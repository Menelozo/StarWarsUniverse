using Radzen;
using SharpTrooper.Core;
using StarWarsUniverseApp.Components;
using StarWarsUniverseApp.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddRadzenComponents()
    .AddSingleton<ThemeService>();

builder.Services.AddScoped<SharpTrooperCore>(sp => new SharpTrooperCore("https://swapi.dev/api/"));
builder.Services.AddScoped<StarWarsFilmService>();

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
