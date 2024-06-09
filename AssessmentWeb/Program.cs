using Assessment.DataAccess.Data;
using Assessment.Models;
using Assessment.Utility;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddIdentity<IdentityUser, IdentityRole>().AddEntityFrameworkStores<ApplicationDbContext>().AddDefaultTokenProviders();
builder.Services.AddRazorPages();
builder.Services.AddScoped<IEmailSender, EmailSender>();
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
app.UseAuthentication();
app.UseAuthorization();
app.MapRazorPages();

//app.MapControllerRoute(
//    name: "default",
//    pattern: "{area=Employee}/{controller=Home}/{action=Index}/{id?}");

app.UseEndpoints(endpoints =>
{
    // Default route for Admin area
    endpoints.MapControllerRoute(
        name: "admin",
        pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

    // Default route for Employee area
    endpoints.MapControllerRoute(
        name: "employee",
        pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

    // Default route for other controllers outside areas
    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");

    endpoints.MapRazorPages();
});


app.MapGet("/", async context =>
{
    if (!context.User.Identity.IsAuthenticated)
    {
        context.Response.Redirect("/Identity/Account/Login");
    }
});


app.Run();
