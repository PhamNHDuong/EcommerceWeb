using EcommerceWeb.CustomerSite.Utilities;
using Microsoft.AspNetCore.Authentication.Cookies;
using Refit;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

builder.Services
    .AddRefitClient<IData>()
    .ConfigureHttpClient(op => op.BaseAddress = new Uri(builder.Configuration["APIUri"]));
   

builder.Services.AddSession(op =>
{
    op.IdleTimeout = TimeSpan.FromMinutes(10);
});

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(opt =>
    {
        opt.LoginPath = "/Auth/Login";
    });


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();


app.MapRazorPages();

app.UseAuthentication();
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name: "Default",
        pattern: "{controller=Products}/{action=Index}/{id?}");
});


app.Run();
