using MoonOps.Application.Services.KeyCloak;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Http;

namespace MoonOps.Application.Handlers;

public class AuthTokenKeeperHandler : DelegatingHandler
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public AuthTokenKeeperHandler(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    protected override async Task<HttpResponseMessage> SendAsync(
        HttpRequestMessage request,
        CancellationToken cancellationToken)
    {
        string token = string.Empty;

        if (_httpContextAccessor.HttpContext != null)
        {
            token = _httpContextAccessor.HttpContext.Request.Headers["Authorization"].ToString();
        }

        bool forceAppToken = request.Headers.TryGetValues("ForcarTokenAplicacao", out var forceValues) &&
                             forceValues.FirstOrDefault()?.ToLower() == "true";

        if (string.IsNullOrWhiteSpace(token) || forceAppToken)
        {
            token = await KeyCloakAuth.GetToken();
        }

        if (request.Headers.Authorization?.ToString() == "Bearer")
        {
            request.Headers.Authorization = null;
        }

        request.Headers.Authorization ??= new AuthenticationHeaderValue("Bearer",
            token.Replace("Bearer ", "", StringComparison.OrdinalIgnoreCase));

        return await base.SendAsync(request, cancellationToken).ConfigureAwait(false);
    }
}