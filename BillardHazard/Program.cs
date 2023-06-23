using BillardHazard;
using BillardHazard.Models;
using Microsoft.EntityFrameworkCore;
using System.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

builder.Services.AddDbContext<BhContext>(options => options.UseMySQL("Server=localhost;Database=billard_hazard;Uid=root;Pwd=root;Port=3306;"));

var app = builder.Build();

//Scope permettant l'usage de la BDD
using (var scope = app.Services.CreateScope())
{
    var scopedServices = scope.ServiceProvider;
    var db = scopedServices.GetRequiredService<BhContext>();

    db.Database.EnsureCreated();

    //Create
    db.Add(new Rule { Explanation = "+1 coup" });
    db.SaveChanges();

    //Read
    Rule rule = db.Rules.First(e => e.Explanation == "+1 coup");

    //Update
    rule.Explanation = "+30 coups";
    db.SaveChanges();

    //Delete
    db.Remove(rule);
    db.SaveChanges();
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

