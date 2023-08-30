using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using kwiz_dashboard;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped<CustomAuthorizationMessageHandler>();
builder.Services.AddHttpClient("ServerAPI", client => client.BaseAddress = new Uri("http://localhost:5204"))
    .AddHttpMessageHandler<CustomAuthorizationMessageHandler>();
builder.Services.AddTransient(sp => 
   sp.GetRequiredService<IHttpClientFactory>().CreateClient("ServerAPI"));

builder.Services.AddOidcAuthentication(options =>
{
    options.ProviderOptions.MetadataUrl = "https://auth.xloyiha.tech/realms/xloyiha/.well-known/openid-configuration";
    options.ProviderOptions.Authority = "https://auth.xloyiha.tech/realms/xloyiha";
    options.ProviderOptions.ClientId = "dashboard";
    options.ProviderOptions.ResponseType = "id_token token";

    options.UserOptions.NameClaim = "preferred_username";
    options.UserOptions.RoleClaim = "roles";
    options.UserOptions.ScopeClaim = "scope";
});

await builder.Build().RunAsync();
