using easy_farmers.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // 🔹 Define Connection Strings
        string connectionString = builder.Configuration.GetConnectionString("easy_farmersContextConnection")
            ?? throw new InvalidOperationException("Connection string 'easy_farmersContextConnection' not found.");

        // 🔹 Add Database Contexts
        builder.Services.AddDbContext<easy_farmersContext>(options =>
            options.UseSqlServer(connectionString)
        );

        builder.Services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
        );

        // ✅ Add Identity (Fix)
        builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = false)
            .AddEntityFrameworkStores<ApplicationDbContext>();

        // ✅ Enable Authentication & Authorization
        builder.Services.AddAuthentication();
        builder.Services.AddAuthorization();

        // ✅ Enable Session (If Needed)
        builder.Services.AddDistributedMemoryCache();
        builder.Services.AddSession();

        // ✅ Add MVC & Razor Pages
        builder.Services.AddControllersWithViews();
        builder.Services.AddRazorPages();

        var app = builder.Build();

        // ✅ Ensure Middleware Is Correctly Configured
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Home/Error");
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();
        app.UseRouting();

        // ✅ Enable Session & Authentication
        app.UseAuthentication();
        app.UseAuthorization();
        app.UseSession();

        // ✅ Define Routes  
        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}");
        app.MapRazorPages();

        app.Run();
    }
}
