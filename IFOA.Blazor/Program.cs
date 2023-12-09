using System.Diagnostics;
using IFOA.DB;
using IFOA.Exceptions;
using IFOA.Shared;
using IFOA.Shared.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Hosting.StaticWebAssets;
using IfoaIt.Data;
using Microsoft.EntityFrameworkCore;
using MudBlazor.Services;
using MudExtensions.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Identity.Web;
using Microsoft.Identity.Web.UI;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;

var builder = WebApplication.CreateBuilder(args);

StaticWebAssetsLoader.UseStaticWebAssets(builder.Environment, builder.Configuration);

builder.Services.AddAuthentication(OpenIdConnectDefaults.AuthenticationScheme)
.AddMicrosoftIdentityWebApp(builder.Configuration.GetSection("AzureAd"));

builder.Services.AddControllersWithViews()
.AddMicrosoftIdentityUI();

builder.Services.AddAuthorization(options =>

{
    options.FallbackPolicy = options.DefaultPolicy;
});

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor()
    .AddMicrosoftIdentityConsentHandler();
builder.Services.AddSingleton<WeatherForecastService>();
builder.Services.AddMudServices();
builder.Services.AddMudExtensions();
builder.Services.AddApplicationInsightsTelemetry();
builder.Services.AddSingleton<PictureBlobStorageService>();

builder.Services.AddDbContextFactory<IfoaContext>(o =>
{
    o.UseSqlServer("Server=localhost;Database=ifoa;User Id=sa;Password=Password!!!!!!;Trusted_Connection=False;Encrypt=False");
});

//
// builder.Services.AddSignalR().AddAzureSignalR(options =>
// {
//     options.ServerStickyMode = 
//         Microsoft.Azure.SignalR.ServerStickyMode.Required;
//     
//     var signalRConnectionString = builder.Configuration[IfoaConfigurationKeys.SignalRConnectionString];
//
//     //if (Debugger.IsAttached)
//     {
//         signalRConnectionString = "Endpoint=https://ifoa.service.signalr.net;AccessKey=qfhVwKPcWtcKQJf6qHdHKJK7jKmRNOoNxModjjEFK2g=;Version=1.0;";
//     }
//     
//     if (string.IsNullOrEmpty(signalRConnectionString))
//         throw new MissingConfigurationException(IfoaConfigurationKeys.SignalRConnectionString);
//
//     options.ConnectionString = signalRConnectionString;
// });

// builder.WebHost.UseStaticWebAssets();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();