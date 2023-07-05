using BillardHazard.Models;
using BillardHazard.Repositories;
using BillardHazard.Tools;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace BillardHazard.Pages
{
    public class IndexModel : PageModel
    {
        private readonly BhContext _dbContext;

        [Required, StringLength(25), BindProperty]
        public string? RedTeamName {get; set; }

        [Required, StringLength(25), BindProperty]
        public string? YellowTeamName { get; set; }

        [Required, BindProperty]
        public bool IsYellowFirst {get; set; }

        public IndexModel(BhContext context)
        {
            _dbContext = context;
        }

        public IActionResult OnPostAsync()       
        {
            Game newGame = new Game();

            Team yellowTeam = new Team();
            Team redTeam = new Team();

            yellowTeam.Name = YellowTeamName;
            yellowTeam.IsItsTurn = IsYellowFirst;
            yellowTeam.Number = (int)ColorTeamEnum.Jaune;
            yellowTeam.Color = ((ColorTeamEnum)yellowTeam.Number).ToString();

            redTeam.Name = RedTeamName;
            redTeam.IsItsTurn = !IsYellowFirst;
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
