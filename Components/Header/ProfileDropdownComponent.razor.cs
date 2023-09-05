using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;

namespace Kwiz.Dashboard.Components.Header;

public partial class ProfileDropdownComponent : ComponentBase
{
    private AuthenticationState authState;
    private string ProfileUrl() 
    => $"{Configuration.GetValue<string>("Oidc:Keycloak:RealmUrl")}/account/#/personal-profile";
    [Inject] private AuthenticationStateProvider AuthStateProvider { get; set; }
    [Inject] private IConfiguration Configuration { get; set; }
    [Inject] private NavigationManager Navigation { get; set; }
    [Inject] SignOutSessionStateManager SignOutManager { get; set; }

    protected override async Task OnInitializedAsync()
    {
        var authState = await AuthStateProvider.GetAuthenticationStateAsync();
        await base.OnInitializedAsync();
    }

    private void BeginSignOut(MouseEventArgs args)
    {
        Navigation.NavigateTo("authentication/logout");
        Navigation.NavigateToLogout("authentication/logout");
    }
}