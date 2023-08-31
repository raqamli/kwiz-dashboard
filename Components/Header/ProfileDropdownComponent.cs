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
    [Inject] SignOutSessionStateManager SignoutManager { get; set; }

    protected override async Task OnInitializedAsync()
    {
        authState = await AuthStateProvider.GetAuthenticationStateAsync();
        await base.OnInitializedAsync();
    }

    private async Task Logout(MouseEventArgs args)
    {
        await SignoutManager.SetSignOutState();
        Navigation.NavigateTo("authentication/logout");
    }
}