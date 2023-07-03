using BillardHazard.Models;
using BillardHazard.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace BillardHazard.Pages
{
    public class IndexModel : PageModel
    {
        public readonly string YELLOW_TEAM = "yellowTeam";
        public readonly string RED_TEAM = "redTeam";

        private readonly BhContext _dbContext;

        public IndexModel(BhContext context)
        {
            _dbContext = context;
        }

        public RedirectToPageResult OnPostAsync()
        {
            Game newGame = new Game();

            Team yellowTeam = new Team();
            Team redTeam = new Team();

            yellowTeam.Name = Request.Form["yellowTeamName"];
            redTeam.Name = Request.Form["redTeamName"];

            yellowTeam.IsItsTurn = Request.Form["firstTeam"] == YELLOW_TEAM;
            redTeam.IsItsTurn = Request.Form["firstTeam"] == RED_TEAM;

            newGame.Teams.Add(yellowTeam);
            newGame.Teams.Add(redTeam);

            Repository<Game> repoGame = new Repository<Game>(_dbContext);
            Repository<Team> repoTeam = new Repository<Team>(_dbContext);

            repoTeam.Create(yellowTeam);
            repoTeam.Create(redTeam);

            repoGame.Create(newGame);

            return RedirectToPage("/Challenge");
        }
    }
}
