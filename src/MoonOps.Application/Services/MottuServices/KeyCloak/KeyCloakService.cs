using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Builder;
using MoonOps.Domain.Models.MottuModels.KeyCloak;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Text.Json;

namespace MoonOps.Application.Services.KeyCloak;

public static class KeyCloakAuth
{
    private static string Token;
    private static int ExpiresIn;
    private static DateTime ExpireDate;
    private static string ClientId;
    private static string ClientSecret;
    private static string Realm;
    private static string SsoHost;
    private static IServiceProvider ServiceProvider;
    private static readonly SemaphoreSlim Semaphore = new(1, 1);

    public static void ConfigureKeyCloak(this IApplicationBuilder app, IConfiguration config)
    {
        ServiceProvider = app.ApplicationServices ?? throw new ArgumentNullException(nameof(app));
        LoadConfiguration(config);
    }

    public static void ConfigureKeyCloak(IServiceProvider provider, IConfiguration config)
    {
        ServiceProvider = provider;
        LoadConfiguration(config);
    }

    private static void LoadConfiguration(IConfiguration config)
    {
        ClientId = config.GetValue<string>("SSO_CLIENT_ID") ?? throw new InvalidOperationException("SSO_CLIENT_ID não configurado");
        ClientSecret = config.GetValue<string>("SSO_CLIENT_SECRET") ?? throw new InvalidOperationException("SSO_CLIENT_SECRET não configurado");
        Realm = config.GetValue<string>("SSO_REALM") ?? "Internal";
        SsoHost = config.GetValue<string>("SSO_URL") ?? "https://sso.mottu.io";
    }

    private static void SetToken(string token, int expiresIn)
    {
        Token = token;
        ExpiresIn = expiresIn;
        ExpireDate = DateTime.UtcNow
            .AddSeconds(expiresIn)
            .AddMinutes(-2); // Força refresh 2 minutos antes de expirar
    }

    private static bool IsTokenValid()
        => !string.IsNullOrEmpty(Token) && ExpireDate > DateTime.UtcNow;

    public static async Task<string> GetToken()
    {
        if (!IsTokenValid())
            await GenerateToken();
        return Token;
    }

    private static async Task GenerateToken()
    {
        await Semaphore.WaitAsync();
        try
        {
            if (IsTokenValid())
                return;

            var logger = ServiceProvider?.GetService<ILogger<object>>();
            logger?.LogInformation("Obtendo token JWT do SSO Keycloak");

            var url = $"{SsoHost}/realms/{Realm}/protocol/openid-connect/token";

            var contentKey = new List<KeyValuePair<string, string>>
            {
                new("client_id", ClientId),
                new("client_secret", ClientSecret),
                new("grant_type", "client_credentials")
            };

            using var client = new HttpClient();
            using var content = new FormUrlEncodedContent(contentKey);

            var response = await client.PostAsync(url, content);
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
            var result = JsonSerializer.Deserialize<KeyCloakAuthResponse>(json, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            if (result == null || string.IsNullOrEmpty(result.AccessToken))
                throw new Exception("Resposta inválida ao obter token do SSO.");

            logger?.LogInformation("Token do SSO obtido com sucesso. Expira em {ExpiresIn} segundos", result.ExpiresIn);

            SetToken(result.AccessToken, result.ExpiresIn);
        }
        catch (Exception ex)
        {
            var logger = ServiceProvider?.GetService<ILogger<object>>();
            logger?.LogError(ex, "Falha ao obter o token do SSO");
            throw;
        }
        finally
        {
            Semaphore.Release();
        }
    }
}