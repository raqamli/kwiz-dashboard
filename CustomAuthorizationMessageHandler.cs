using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;

public class CustomAuthorizationMessageHandler : AuthorizationMessageHandler
{
    public CustomAuthorizationMessageHandler(
        IAccessTokenProvider provider,
        NavigationManager navigationManager,
        IConfiguration configuration)
        : base(provider, navigationManager)
    {
        ConfigureHandler(
            authorizedUrls: new[] { configuration.GetValue<string>("KwizApi:BaseUrl") });

    }
}