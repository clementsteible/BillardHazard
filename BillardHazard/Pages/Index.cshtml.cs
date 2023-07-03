using BillardHazard.Models;
using BillardHazard.Repositories;
using BillardHazard.Tools;
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

        public IActionResult OnPostAsync()
        {
            Game newGame = new Game();

            Team yellowTeam = new Team();
            Team redTeam = new Team();

            yellowTeam.Name = Request.Form["yellowTeamName"];
            yellowTeam.IsItsTurn = Request.Form["firstTeam"] == YELLOW_TEAM;
            yellowTeam.Number = (int)ColorTeamEnum.Jaune;
            yellowTeam.Color = ((ColorTeamEnum)yellowTeam.Number).ToString();

            redTeam.Name = Request.Form["redTeamName"];
            redTeam.IsItsTurn = Request.Form["firstTeam"] == RED_TEAM;
            redTeam.Number = (int)ColorTeamEnum.Rouge;
            redTeam.Color = ((ColorTeamEnum)redTeam.Number).ToString();

            newGame.Teams.Add(yellowTeam);
            newGame.Teams.Add(redTeam);

            Repository<Game> repoGame = new Repository<Game>(_dbContext);

            repoGame.Create(newGame);

            return RedirectPreserveMethod($"/Challenge/{newGame.Id}");
        }
    }
}
