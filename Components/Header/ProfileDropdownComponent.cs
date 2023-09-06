using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;

namespace Kwiz.Dashboard.Components.Header;

public partial class ProfileDropdownComponent : ComponentBase
{
    private AuthenticationState authState;
    private string ProfileUrl() 
        => $"{Configuration.GetValue<string>("Oidc:Keycloak:RealmUrl")}/account/#/personal-info";
    [Inject] private AuthenticationStateProvider AuthStateProvider { get; set; }
    [Inject] private IConfiguration Configuration { get; set; }
    [Inject] private NavigationManager Navigation { get; set; }
    protected override async Task OnInitializedAsync()
    {
        authState = await AuthStateProvider.GetAuthenticationStateAsync();
        await base.OnInitializedAsync();
    }

    private void BeginSignOut(MouseEventArgs args)
    {
        Navigation.NavigateToLogout(Configuration.GetValue<string>("Oidc:Keycloak:LogoutUrl"));
    }
}