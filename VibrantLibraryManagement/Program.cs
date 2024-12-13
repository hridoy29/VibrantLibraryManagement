//using Syncfusion.Blazor;
//using VibrantLibraryManagement.Components;
//using VibrantLibraryManagement.ServiceLayer.Services;

//using Microsoft.AspNetCore.Authentication.Cookies;
//using Microsoft.AspNetCore.Authentication.Google;

//var builder = WebApplication.CreateBuilder(args);

//// Add services to the container.
//builder.Services.AddRazorComponents()
//    .AddInteractiveServerComponents();

//var baseUrl = builder.Configuration.GetValue<string>("Api:BaseUrl");

//builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(baseUrl) });

//builder.Services.AddScoped<ILoginService, LoginService>();

//Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("Ngo9BigBOggjHTQxAR8/V1NDaF1cX2hIfEx0Qnxbf1x0ZFREal9RTnZeUiweQnxTdEFiWH1ZcHRWRmNVUk10WA==");

//builder.Services.AddSyncfusionBlazor();

//builder.Services.AddHttpContextAccessor();

//// Add Google authentication
//builder.Services.AddAuthentication(options =>
//{
//    options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
//    options.DefaultChallengeScheme = GoogleDefaults.AuthenticationScheme;
//})
//.AddCookie()
//.AddGoogle(options =>
//{
//    options.ClientId = builder.Configuration.GetValue<string>("Authentication:Google:ClientId");
//    options.ClientSecret = builder.Configuration.GetValue<string>("Authentication:Google:ClientSecret");
//});

//var app = builder.Build();

//// Configure the HTTP request pipeline.
//if (!app.Environment.IsDevelopment())
//{
//    app.UseExceptionHandler("/Error", createScopeForErrors: true);
//    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
//    app.UseHsts();
//}

//// Enable Authentication
//app.UseAuthentication();  
//app.UseAuthorization();

//app.UseHttpsRedirection();

//app.UseStaticFiles();
//app.UseAntiforgery();


//app.MapRazorComponents<App>()
//    .AddInteractiveServerRenderMode();

//app.Run();


using Syncfusion.Blazor;
using VibrantLibraryManagement.Components;
using VibrantLibraryManagement.ServiceLayer.Services;

using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

var baseUrl = builder.Configuration.GetValue<string>("Api:BaseUrl");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(baseUrl) });

builder.Services.AddScoped<ILoginService, LoginService>();

Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("Ngo9BigBOggjHTQxAR8/V1NDaF1cX2hIfEx0Qnxbf1x0ZFREal9RTnZeUiweQnxTdEFiWH1ZcHRWRmNVUk10WA==");

builder.Services.AddSyncfusionBlazor();

builder.Services.AddHttpContextAccessor();

// Add Google authentication
builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = GoogleDefaults.AuthenticationScheme;
})
.AddCookie()
.AddGoogle(options =>
{
    options.ClientId = builder.Configuration.GetValue<string>("Authentication:Google:ClientId");
    options.ClientSecret = builder.Configuration.GetValue<string>("Authentication:Google:ClientSecret");
});

builder.Services.AddControllers(); // **Add this line** to include controllers

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

app.UseRouting(); // **Add this line** to use routing

// Enable Authentication
app.UseAuthentication();
app.UseAuthorization();

app.UseAntiforgery();

// Configure endpoints
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}"); // **Add this line** to map default controller route
    endpoints.MapRazorComponents<App>().AddInteractiveServerRenderMode();
    endpoints.MapControllers(); // **Add this line** to map controllers
});

app.Run();
