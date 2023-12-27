using Exchange.BAL.Services;
using Exchange.Library.Helper;
using Exchange.WebApp.Components;
using Exchnage.Library.ClinetHttpServices;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);
//builder.Services.AddRegisterBusinessServices();
// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

//JSON serialization options for System.Text.Json
var jsonSerializerOptions = new JsonSerializerOptions
{
    //PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
    PropertyNameCaseInsensitive = true

};

// Correct way to add named HttpClient
//builder.Services.AddHttpClient("ApplicationAPI", client =>
//{
//    client.BaseAddress = new Uri("https://localhost:7271/");
//});

builder.Services.AddHttpClient<ApplicationHttpClient>(client =>
{
    client.BaseAddress = new Uri("https://localhost:7271/api/");
});

var app = builder.Build();



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
    .AddInteractiveServerRenderMode();

app.Run();
