using BillardHazard;
using BillardHazard.Tools;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using BillardHazard.Areas.Identity.Data;
using BillardHazard.TimedBackgroundTasks;
using BillardHazard.Models;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("ProdConnection") ?? throw new InvalidOperationException("Connection string 'ProdConnection' not found.");
//var connectionString = builder.Configuration.GetConnectionString("DevConnection") ?? throw new InvalidOperationException("Connection string 'DevConnection' not found.");

// Set default root page path
builder.Services.AddRazorPages().AddRazorPagesOptions(options =>
{
    options.RootDirectory = "/Pages";
});

// Add EF Core classes to link the BDD
builder.Services.AddDbContext<BhContext>(options => options.UseSqlServer(connectionString));
builder.Services.AddDbContext<IdentityDbContext>(options => options.UseSqlServer(connectionString));

// Define required role for limited access pages
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("RequireAdministratorRole",
         policy => policy.RequireRole(Constants.ADMIN));
});

// Set authorizations
builder.Services.AddRazorPages(options =>
{
    options.Conventions.AuthorizePage("/HighScores/Create");
    options.Conventions.AuthorizePage("/HighScores/Delete");
    options.Conventions.AuthorizePage("/HighScores/Edit");
    options.Conventions.AuthorizePage("/HighScores/Details");

    options.Conventions.AuthorizeAreaPage("Identity", "/Account/Register");

    options.Conventions.AuthorizeFolder("/Games");
    options.Conventions.AuthorizeFolder("/Rules");

    options.Conventions.AllowAnonymousToPage("/Index");
    options.Conventions.AllowAnonymousToFolder("/Challenge");
});

builder.Services
    .AddDefaultIdentity<Administrator>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<IdentityDbContext>();

builder.Services.AddHostedService<CleanGamesAndTeams>();

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

app.UseAuthorization();

// Create DB if not exists
using (var scope = app.Services.CreateScope())
{
    var scopedServices = scope.ServiceProvider;
    var db = scopedServices.GetRequiredService<BhContext>();
    var dbIdentity = scopedServices.GetRequiredService<IdentityDbContext>();

    db.Database.EnsureCreated();

    if (db.Rules.Count() < 1)
    {
        foreach (Rule rule in DefaultRules.DefaultRulesList)
        {
            db.Rules.Add(rule);
        }

        db.SaveChanges();
    }
}

// Create Admin role in BDD if not exists
using (var scope = app.Services.CreateScope())
{
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

    var roles = new[] { Constants.ADMIN };

    foreach (var role in roles)
    {
        if (!await roleManager.RoleExistsAsync(role))
        {
            await roleManager.CreateAsync(new IdentityRole(role));
        }
    }
}

// Create an Admin in BDD if not exists
using (var scope = app.Services.CreateScope())
{
    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<Administrator>>();

    string username = Constants.ADMIN;
    string password = "Root123!";

    if (await userManager.FindByNameAsync(username) == null)
    {
        var user = new Administrator();
        user.UserName = username;
        user.Email = username;

        await userManager.CreateAsync(user, password);

        await userManager.AddToRoleAsync(user, Constants.ADMIN);
    }
}

app.MapRazorPages();

app.Run();

