using Microsoft.AspNet.SignalR;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.EntityFrameworkCore;
using MultiplayerQuizGame.Client.Pages;
using MultiplayerQuizGame.Components;
using MultiplayerQuizGame.Shared.Data;
using MultiplayerQuizGame.Shared.Repositories;
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
                        .GetConnectionString("DefaultDbConnection"),
                        b=>b.MigrationsAssembly("MultiplayerQuizGame")));

builder.Services.AddSignalR();
builder.Services.AddScoped<IRoomRepository, RoomRepository>();
//builder.Services.AddScoped<IRoomService, RoomService>();
builder.Services.AddScoped<IQuizService, QuizService>();
builder.Services.AddSwaggerGen();
builder.Services.AddControllersWithViews().AddNewtonsoftJson(x =>
{
    x.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
});


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
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Blazor API V1");
});
app.MapHub<RoomHub>("/roomhub");

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode()
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies(typeof(MultiplayerQuizGame.Client._Imports).Assembly);

app.Run();
