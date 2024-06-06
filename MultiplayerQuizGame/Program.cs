using Microsoft.EntityFrameworkCore;
using MultiplayerQuizGame.Components;
using MultiplayerQuizGame.Shared.Data;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;
using MultiplayerQuizGame.Shared.Repositories.Interfaces;
using MultiplayerQuizGame.Shared.Services.Interfaces;
using MultiplayerQuizGame.Shared.Services.Server;
using MultiplayerQuizGame.Shared.Repositories.Server;
using MultiplayerQuizGame.Components.Hubs;
using Microsoft.Extensions.Azure;
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

builder.Services.AddScoped<IQuizRepository,QuizRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IRoomRepository, RoomRepository>();
builder.Services.AddScoped<IQuizService, QuizService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IRoomService,RoomService>();
builder.Services.AddScoped<IFileService,FileService>();

builder.Services.AddSignalR();
builder.Services.AddSwaggerGen();
builder.Services.AddControllersWithViews().AddNewtonsoftJson(x =>
{
    x.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
});
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).
				AddCookie(x => {
                    x.LoginPath = "/login";
                    x.LogoutPath = "/logout";
                    });
builder.Services.AddAuthorization(options =>
{
	options.AddPolicy("LoggedUserOnly", policy => policy.RequireClaim(ClaimTypes.Role, "LoggedUser"));
});
builder.Services.AddCascadingAuthenticationState();
var configuration = builder.Configuration;
builder.Services.AddAzureClients(clientBuilder =>
{
    clientBuilder.AddBlobServiceClient(configuration["Azure:BlobServiceConnectionString"]!, preferMsi: true);
    clientBuilder.AddQueueServiceClient(configuration["Azure:BlobServiceConnectionString"]!, preferMsi: true);
});




var app = builder.Build();
app.UseSwagger();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Blazor API V1");
    });

}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.MapControllers();
app.MapHub<GameHub>("/gamehub");
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseAntiforgery();
app.UseAuthorization();
app.UseAuthorization();




app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode()
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies(typeof(MultiplayerQuizGame.Client._Imports).Assembly);

app.Run();
