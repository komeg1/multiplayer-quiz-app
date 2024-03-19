using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MultiplayerQuizGame.Shared.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.Services.AddScoped(http => new HttpClient
{
    BaseAddress = new Uri(builder.HostEnvironment.BaseAddress),
});
builder.Services.AddScoped<IRoomService, ClientRoomService>();
builder.Services.AddScoped<IQuizService, ClientQuizService>();

await builder.Build().RunAsync();
