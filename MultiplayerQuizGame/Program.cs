using Microsoft.AspNet.SignalR;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.EntityFrameworkCore;
using MultiplayerQuizGame.Client.Pages;
using MultiplayerQuizGame.Components;
using MultiplayerQuizGame.Shared.Data;
using MultiplayerQuizGame.Components.Repositories;
using MultiplayerQuizGame.Shared.Services;
using MultiplayerQuizGame.Components.Hubs;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents()
    .AddInteractiveWebAssemblyComponents();

builder.Services.AddControllers();

builder.Services.AddScoped(http => new HttpClient
{
    BaseAddress = new Uri(builder.Configuration.GetSection("BaseUri").Value!),
});


builder.Services.AddDbContext<DataContext>(options =>
options.UseSqlServer(builder
                        .Configuration
                        .GetConnectionString("DefaultDbConnection")));

builder.Services.AddSignalR();
builder.Services.AddScoped<IRoomRepository, RoomRepository>();
builder.Services.AddScoped<IRoomService, RoomService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.MapControllers();
app.UseStaticFiles();
app.UseAntiforgery();

app.MapHub<RoomHub>("/roomhub");

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode()
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies(typeof(MultiplayerQuizGame.Client._Imports).Assembly);

app.Run();
