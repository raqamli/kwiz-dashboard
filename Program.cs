using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Kwiz.Dashboard;                                                                                    

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped<CustomAuthorizationMessageHandler>();
builder.Services.AddHttpClient<IKwizApiHttpClient, KwizApiHttpClient>(client => 
    client.BaseAddress = new Uri(builder.Configuration.GetValue("KwizApiBaseUrl", string.Empty)))
    .AddHttpMessageHandler<CustomAuthorizationMessageHandler>();

// builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://http://localhost:5115/") });

// builder.Services.AddHttpClient<IKwizApiHttpClient, KwizApiHttpClient>(client => 
//     client.BaseAddress = new Uri(builder.Configuration["KwizApiBaseUrl"]))
//     .AddHttpMessageHandler<CustomAuthorizationMessageHandler>();


// builder.Services.AddHttpClient("KwizAPI", client => client.BaseAddress = new Uri(builder.Configuration.GetValue<string>("KwizApi:BaseUrl")))
//     .AddHttpMessageHandler<CustomAuthorizationMessageHandler>();


// builder.Services.AddScoped(x => {
//     var apiUrl = new Uri("http://localhost:5115");
//     return new HttpClient() {BaseAddress = apiUrl};
// });

// builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

builder.Services.AddOidcAuthentication(options =>
{
    var keycloakSection = builder.Configuration.GetSection("Oidc:Keycloak");
    options.ProviderOptions.MetadataUrl = keycloakSection.GetValue<string>("OidcConfigUrl");
    options.ProviderOptions.Authority = keycloakSection.GetValue<string>("RealmUrl");
    options.ProviderOptions.ClientId = keycloakSection.GetValue<string>("ClientId");
    options.ProviderOptions.ResponseType = keycloakSection.GetValue<string>("ResponseType");

    options.UserOptions.NameClaim = keycloakSection.GetValue<string>("NameClaim");
    options.UserOptions.RoleClaim = keycloakSection.GetValue<string>("RoleClaim");
    options.UserOptions.ScopeClaim = keycloakSection.GetValue<string>("ScopeCliam");
});

await builder.Build().RunAsync();
