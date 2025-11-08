using System.Net.Http.Headers;
using MoonOps.Application.Services.KeyCloak;
namespace MoonOps.Application.Handlers;

public class AuthTokenHandler : DelegatingHandler
{
    public AuthTokenHandler()
    {
    }

    protected override async Task<HttpResponseMessage> SendAsync(
        HttpRequestMessage request,
        CancellationToken cancellationToken)
    {
        var token = await KeyCloakAuth.GetToken();

        if (request.Headers.Authorization == null ||
            (request.Headers.Authorization.Scheme == "Bearer" &&
             string.IsNullOrEmpty(request.Headers.Authorization.Parameter)))
        {
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token.Replace("Bearer ", ""));
        }

        return await base.SendAsync(request, cancellationToken);
    }
}