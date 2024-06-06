using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MultiplayerQuizGame.Shared.Services.Interfaces;
using MultiplayerQuizGame.Shared.Repositories.Interfaces;
using MultiplayerQuizGame.Shared.Repositories.Client;
using MultiplayerQuizGame.Shared.Services.Client;
using MultiplayerQuizGame.Shared.Services.Server;
var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.Services.AddScoped(http => new HttpClient
{
    BaseAddress = new Uri(builder.HostEnvironment.BaseAddress),
});
builder.Services.AddScoped<IQuizRepository, ClientQuizRepository>();
builder.Services.AddScoped<IUserRepository, ClientUserRepository>();
builder.Services.AddScoped<IQuizService, ClientQuizService>();

var configuration = builder.Configuration;
await builder.Build().RunAsync();
