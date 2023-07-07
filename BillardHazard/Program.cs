using BillardHazard;
using BillardHazard.Models;
using BillardHazard.Repositories;
using BillardHazard.Tools;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using System.Configuration;
using Microsoft.AspNetCore.Identity;
using BillardHazard.Areas.Identity.Data;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("IdentityDbContextConnection") ?? throw new InvalidOperationException("Connection string 'IdentityDbContextConnection' not found.");

// Add services to the container + Mettre les authorisations sur les pages
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

builder.Services.AddRazorPages().AddRazorPagesOptions(options =>
{
    options.RootDirectory = "/Pages";
});

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("RequireAdministratorRole",
         policy => policy.RequireRole("Administrator"));
});

builder.Services.AddDbContext<BhContext>(options => options.UseMySQL("Server=localhost;Database=billard_hazard;Uid=root;Pwd=root;Port=3306;"));

builder.Services.AddDbContext<IdentityDbContext>(options => options.UseMySQL("Server=localhost;Database=billard_hazard;Uid=root;Pwd=root;Port=3306;"));

builder.Services.AddDefaultIdentity<Administrator>(options => options.SignIn.RequireConfirmedAccount = false).AddEntityFrameworkStores<IdentityDbContext>();

var app = builder.Build();

//Scope permettant l'usage de la BDD + création de la Database s'il elle n'existe pas encore
using (var scope = app.Services.CreateScope())
{
    var scopedServices = scope.ServiceProvider;
    var db = scopedServices.GetRequiredService<BhContext>();

    db.Database.EnsureCreated();
}

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

app.MapRazorPages();

app.Run();

