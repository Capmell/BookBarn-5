using BookBarn.Models;
using BookBarn.Services;
using BookBarn.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// MVC
builder.Services.AddControllersWithViews();

//Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new() { Title = "BookBarn API", Version = "v1" });

    // Only include endpoints whose route starts with "api/"
    c.DocInclusionPredicate((docName, apiDesc) =>
        apiDesc.RelativePath != null &&
        apiDesc.RelativePath.StartsWith("api/", StringComparison.OrdinalIgnoreCase));
});

// Identity UI uses Razor Pages
builder.Services.AddRazorPages();



// EF Core
builder.Services.AddDbContext<BookBarnContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("BookBarnConnection")));

// Identity + Roles
builder.Services.AddIdentity<IdentityUser, IdentityRole>(options =>
{
    // Course demo: keep confirmation OFF so students can test quickly
    options.SignIn.RequireConfirmedAccount = false;
})
.AddEntityFrameworkStores<BookBarnContext>()
.AddDefaultTokenProviders();

// Demo-only: Register an email sender so Register does not crash
builder.Services.AddTransient<IEmailSender, DevEmailSender>();

builder.Services.AddScoped<IBookService, BookService>();

builder.Services.AddScoped<IAuthorService, AuthorService>();


var app = builder.Build();
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<BookBarnContext>();

    if (!db.Books.Any())
    {
        db.Books.AddRange(
            new Book { Title = "The Pragmatic Programmer", Author = "Hunt & Thomas", Price = 42.00m },
            new Book { Title = "Clean Code", Author = "Robert C. Martin", Price = 38.50m },
            new Book { Title = "Design Patterns", Author = "Gamma et al.", Price = 55.00m }
        );

        db.SaveChanges();
    }
    if (!db.Authors.Any())
    {
        db.Authors.AddRange(
            new Author { FirstName = "Robert", LastName = "Martin", MiddleName = "C."},
            new Author { FirstName = "J.R.R.", LastName = "Tolkein", MiddleName = "" },
            new Author { FirstName = "J.K.", LastName = "Rowling", MiddleName = "" }
        );

        db.SaveChanges();
    }
}
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();
app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

app.MapRazorPages();

using (var scope = app.Services.CreateScope())
{
    // -----------------------------
    // Seed BookBarn data (Books/Authors)
    // -----------------------------
    var db = scope.ServiceProvider.GetRequiredService<BookBarnContext>();

    if (!db.Books.Any())
    {
        db.Books.AddRange(
            new Book { Title = "The Pragmatic Programmer", Author = "Hunt & Thomas", Price = 42.00m },
            new Book { Title = "Clean Code", Author = "Robert C. Martin", Price = 38.50m },
            new Book { Title = "Design Patterns", Author = "Gamma et al.", Price = 55.00m }
        );

        db.SaveChanges();
    }

    if (!db.Authors.Any())
    {
        db.Authors.AddRange(
            new Author { FirstName = "Robert", LastName = "Martin", MiddleName = "C." },
            new Author { FirstName = "J.R.R.", LastName = "Tolkein", MiddleName = "" },
            new Author { FirstName = "J.K.", LastName = "Rowling", MiddleName = "" }
        );

        db.SaveChanges();
    }

    // -----------------------------
    // Seed Identity (Admin role/user)
    // -----------------------------
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();

    if (!await roleManager.RoleExistsAsync("Admin"))
    {
        await roleManager.CreateAsync(new IdentityRole("Admin"));
    }

    var adminEmail = "admin@bookbarn.com";
    var adminUser = await userManager.FindByEmailAsync(adminEmail);

    if (adminUser == null)
    {
        adminUser = new IdentityUser
        {
            UserName = adminEmail,
            Email = adminEmail,
            EmailConfirmed = true
        };

        await userManager.CreateAsync(adminUser, "P@ssw0rd!");
    }

    if (!await userManager.IsInRoleAsync(adminUser, "Admin"))
    {
        await userManager.AddToRoleAsync(adminUser, "Admin");
    }
}

app.Run();
