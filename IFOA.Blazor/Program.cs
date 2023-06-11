using System.Diagnostics;
using IFOA.DB;
using IFOA.Exceptions;
using IFOA.Shared;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Hosting.StaticWebAssets;
using IfoaIt.Data;
using Microsoft.EntityFrameworkCore;
using MudBlazor.Services;
using MudExtensions.Services;
using Syncfusion.Blazor;

var builder = WebApplication.CreateBuilder(args);

StaticWebAssetsLoader.UseStaticWebAssets(builder.Environment, builder.Configuration);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddSingleton<WeatherForecastService>();
builder.Services.AddMudServices();
builder.Services.AddMudExtensions();

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

//Syncflow
builder.Services.AddSyncfusionBlazor();

var app = builder.Build();

//SyncFlow
Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("Mgo+DSMBaFt+QHJqVk1hXk5Hd0BLVGpAblJ3T2ZQdVt5ZDU7a15RRnVfRFxiSXlRf0diXXleeA==;Mgo+DSMBPh8sVXJ1S0R+X1pFdEBBXHxAd1p/VWJYdVt5flBPcDwsT3RfQF5jT39SdkJhUH1fdHNQTg==;ORg4AjUWIQA/Gnt2VFhiQlJPd11dXmJWd1p/THNYflR1fV9DaUwxOX1dQl9gSXhSd0VkW3ZccXBcRmg=;MjM4MTE5OEAzMjMxMmUzMDJlMzBsSmt0WVJ6NVFDS3hBV3l0aVQwVlJ5WW54emg2SE5IbVhpci9COSsyMjlnPQ==;MjM4MTE5OUAzMjMxMmUzMDJlMzBpdU9ocE00RUo4eDE5ZmlsUzVxaVlMc1hocWZNRXRxckdTUjhQcmU3MDRBPQ==;NRAiBiAaIQQuGjN/V0d+Xk9HfV5AQmBIYVp/TGpJfl96cVxMZVVBJAtUQF1hSn5Vd0RjXn1XcnVQQGVU;MjM4MTIwMUAzMjMxMmUzMDJlMzBhd2UyZnNkSjdiNWloaU5TTC9YTjZqdE00RzdaWnBteUQxeFVad1hEdTZNPQ==;MjM4MTIwMkAzMjMxMmUzMDJlMzBWdnYrTjluOHhkYnNadUIwQzlaV05uRG9GYm1sTkdaQTU1NDhaMjNWY3ZnPQ==;Mgo+DSMBMAY9C3t2VFhiQlJPd11dXmJWd1p/THNYflR1fV9DaUwxOX1dQl9gSXhSd0VkW3ZccXJWQ2g=;MjM4MTIwNEAzMjMxMmUzMDJlMzBTcE5CaFRiZG9oL1lwSjNkRHdIVmdKci9abnR2aVdMbnpCdnhVd0VybDNVPQ==;MjM4MTIwNUAzMjMxMmUzMDJlMzBuMS9hcnJ1ZWU1akY2ZUJLcm5CZzJvOURoVVJaSEdRTEtDaUlMN01vaTl3PQ==;MjM4MTIwNkAzMjMxMmUzMDJlMzBhd2UyZnNkSjdiNWloaU5TTC9YTjZqdE00RzdaWnBteUQxeFVad1hEdTZNPQ==");

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

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();