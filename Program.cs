using CarCleanz.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.DataProtection;

var builder = WebApplication.CreateBuilder(args);

// ? Data Protection fix for Render (persistent antiforgery keys)
builder.Services.AddDataProtection()
    .PersistKeysToFileSystem(new DirectoryInfo("/tmp/keys"))
    .SetApplicationName("CarCleanzApp");

var port = Environment.GetEnvironmentVariable("PORT") ?? "8080";

// ? SQLite database in /tmp
var connectionString = "Data Source=Data/CarCleanz_v3.db";

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite(connectionString));

builder.Services.AddControllersWithViews();
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession();

var app = builder.Build();

// ? Ensure database is created
using (var scope = app.Services.CreateScope())
{
    var dataPath = Path.Combine(Directory.GetCurrentDirectory(), "Data");
    if (!Directory.Exists(dataPath))
    {
        Directory.CreateDirectory(dataPath);
    }

    var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    db.Database.EnsureCreated();
}
// ? Configure pipeline
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

// app.UseHttpsRedirection(); // handled by Render

app.UseStaticFiles();
app.UseRouting();
app.UseSession();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

// ? Bind to Render port
app.Run($"http://0.0.0.0:{port}");
