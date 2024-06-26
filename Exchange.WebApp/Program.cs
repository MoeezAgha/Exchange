using Blazored.LocalStorage;
using Exchange.BAL.Services;

using Exchange.Library.Helper;
using Exchange.UI.Library.Helper.NavigationMenu;
using Exchange.UI.Library.Helper.StateProviderHelper;
using Exchange.WebApp;
using Exchange.WebApp.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Extensions.DependencyInjection;
using MudBlazor.Services;
using Radzen;
using System.Text.Json;
using System.Text.Json.Serialization;
using Exchange.Library.Services;
using Exchange.UI.Library;


var builder = WebApplication.CreateBuilder(args);
//builder.Services.AddRegisterBusinessServices();
// Add services to the container.
builder.Services.AddRazorComponents()

    .AddInteractiveServerComponents();

builder.Services.AddMudServices();
//builder.Services.AddAutoMapper(typeof(AutoMapperProfiles));

//JSON serialization options for System.Text.Json
var jsonSerializerOptions = new JsonSerializerOptions
{
    //PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
    PropertyNameCaseInsensitive = true

};

builder.Services.AddRadzenComponents();



builder.Services.AddBlazoredLocalStorage(config =>
{
    config.JsonSerializerOptions.DictionaryKeyPolicy = JsonNamingPolicy.CamelCase;
    config.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
    config.JsonSerializerOptions.IgnoreReadOnlyProperties = true;
    config.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
    config.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
    config.JsonSerializerOptions.ReadCommentHandling = JsonCommentHandling.Skip;
    config.JsonSerializerOptions.WriteIndented = false;
});


// Correct way to add named HttpClient
//builder.Services.AddHttpClient("ApplicationAPI", client =>
//{
//    client.BaseAddress = new Uri("https://localhost:7271/");
//});

builder.Services.AddHttpClient<ApplicationHttpClient>(client =>
{
    client.BaseAddress = new Uri("https://localhost:7271/api/");
});


// Add CORS policy
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "AllowSpecificOrigin1",
                      builder =>
                      {
                          builder.WithOrigins("https://localhost:7271") // Specify the origin of your Blazor app
                                 .AllowAnyHeader()
                                 .AllowAnyMethod();
                      });
});

// Add CORS policy
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "AllowSpecificOrigin",
                      builder =>
                      {
                          builder.WithOrigins("https://localhost:7032") // Specify the origin of your Blazor app
                                 .AllowAnyHeader()
                                 .AllowAnyMethod();
                      });
});
builder.Services.AddScoped<CustomAuthenticationStateProvider>();
builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthenticationStateProvider>();
//builder.Services.AddAutoMapper();
builder.Services.AddScoped<NavigationMenuService>();
builder.Services.AddScoped<NavigationMenuSeriveEvent>();
builder.Services.AddAutoMapper(typeof(AutoMapperProfiles));

#region Register Lib services
builder.Services.AddRegisterServicesUIExtensions();
#endregion
var app = builder.Build();

app.UseCors("AllowSpecificOrigin");

app.UseCors("AllowSpecificOrigin1");
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}




app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();


app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode()

    .AddAdditionalAssemblies(typeof(TagForm).Assembly);


app.Run();
