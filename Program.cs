using CarCleanz.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// MVC
builder.Services.AddControllersWithViews();

// Database
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

// Error handling
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}

app.UseStaticFiles();

app.UseRouting();

// ?? DISABLE ANTIFORGERY COMPLETELY FOR RENDER
app.Use((context, next) =>
{
    context.Features.Set<Microsoft.AspNetCore.Antiforgery.IAntiforgeryFeature>(null);
    context.Request.Headers.Remove("RequestVerificationToken");
    return next();
});

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();