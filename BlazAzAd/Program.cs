using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using BlazAzAd;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped<APIAuthorizationMessageHandler>();
builder.Services.AddHttpClient("API",
        client => client.BaseAddress = new Uri("https://localhost:5011"))
    .AddHttpMessageHandler<APIAuthorizationMessageHandler>();
builder.Services.AddScoped(sp => sp.GetService<IHttpClientFactory>().CreateClient("API"));

builder.Services.AddMsalAuthentication(options =>
{
    builder.Configuration.Bind("AzureAd", options.ProviderOptions.Authentication);
    options.ProviderOptions.LoginMode = "redirect";
    options.ProviderOptions.DefaultAccessTokenScopes
       .Add("https://you.onmicrosoft.com/webapi/web-api");
});

await builder.Build().RunAsync();
