using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;

public class APIAuthorizationMessageHandler : AuthorizationMessageHandler
{
    public APIAuthorizationMessageHandler(IAccessTokenProvider provider,
        NavigationManager navigationManager)
        : base(provider, navigationManager)
    {
        ConfigureHandler(
            authorizedUrls: new[] { "https://localhost:5011/weatherforecast" },
            scopes: new[] { "https://alexandergolubevarcadiaspb.onmicrosoft.com/webapi/web-api" });
    }
}
