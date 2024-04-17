using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MultiplayerQuizGame.Shared.Services;
using MultiplayerQuizGame.Shared.Services.Interfaces;
using MultiplayerQuizGame.Shared.Repositories.Interfaces;
using MultiplayerQuizGame.Shared.Repositories.Client;
using MultiplayerQuizGame.Shared.Services.Client;
var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.Services.AddScoped(http => new HttpClient
{
    BaseAddress = new Uri(builder.HostEnvironment.BaseAddress),
});
builder.Services.AddScoped<IRoomService, ClientRoomService>();
builder.Services.AddScoped<IQuizRepository, ClientQuizRepository>();
builder.Services.AddScoped<IQuizService, ClientQuizService>();

await builder.Build().RunAsync();
