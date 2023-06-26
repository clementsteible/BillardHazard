using BillardHazard;
using BillardHazard.Models;
using BillardHazard.Repositories;
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

    Repository<Rule> ruleRepo = new Repository<Rule>(db);
    Repository<Bonus> bonusRepo = new Repository<Bonus>(db);

    db.Database.EnsureCreated();

    //Test repo Rule : Création, GetAll, Affichage, Modification, Suppression
    Rule rule = new Rule();
    rule.Explanation = "Règle parmis d'autres";
    ruleRepo.Create(rule);

    List<Rule> rules = ruleRepo.GetAll();

    int id = rules.First(r => r.Explanation == "Règle parmis d'autres").Id;
    Rule? readRule = ruleRepo.FindById(id);

    readRule.Explanation = "UPDATE Règle parmis d'autres";
    ruleRepo.Update(readRule);

    ruleRepo.Delete(readRule);

    //Test repo Bonus : GetAll, Création, Affichage, Modification, Suppression
    Bonus bonus = new Bonus();
    bonus.Explanation = "Bonus parmis d'autres";
    bonusRepo.Create(bonus);

    List<Bonus> bonuses = bonusRepo.GetAll();

    int bonusId = bonuses.First(b => b.Explanation == "Bonus parmis d'autres").Id;
    Bonus? readBonus= bonusRepo.FindById(bonusId);

    readBonus.Explanation = "UPDATE Règle parmis d'autres";
    bonusRepo.Update(readBonus);

    bonusRepo.Delete(readBonus);
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

