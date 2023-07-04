using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BillardHazard.Models;
using System.Text.Json;
using System.Text.Json.Serialization;
using BillardHazard.Tools;
using BillardHazard.Repositories;

namespace BillardHazard.Pages.Challenge
{
    public class IndexModel : PageModel
    {
        private readonly BhContext _context;

        public IndexModel(BhContext context)
        {
            _context = context;
        }

        public IList<Models.Rule> Rule { get; set; } = default!;
        public string JsonRules { get; set; } = default!;
        public Game CurrentGame { get; set; }
        public Team ActualTeam { get; set; } = new Team();
        public Team OpponentTeam { get; set; } = new Team();
        public string CssBallColorClass { get; set; }
        [BindProperty]
        public bool IsChallengeValidate { get; set; }

        //Temporaire TODO Remove when ok
        public async Task OnPostAsync(Guid gameId)
        {
            if (_context.Rules != null)
            {
                Rule = await _context.Rules.ToListAsync();

                JsonRules = JsonSerializer.Serialize(Rule, new JsonSerializerOptions { WriteIndented = true });

                Repository<Game> repoGame = new Repository<Game>(_context);
                Repository<Team> repoTeam = new Repository<Team>(_context);

                CurrentGame = repoGame.FindById(gameId) ?? new Game();
                List<Team> teams = repoTeam.GetAll().Where(t => t.GameId == gameId).ToList();

                ActualTeam = teams.First(t => t.IsItsTurn);
                OpponentTeam = teams.First(t => !t.IsItsTurn);

                CssBallColorClass = ((CssClassBallEnum)ActualTeam.Number).ToString();
            }
        }

        public void OnGet()
        {

        }

        public IActionResult OnPostOpponentTurn()
        {
            if (IsChallengeValidate)
            {
                ActualTeam.Score++;
            }
            
            ActualTeam.IsItsTurn = !ActualTeam.IsItsTurn;
            OpponentTeam.IsItsTurn = !OpponentTeam.IsItsTurn;

            Repository<Team> repoTeam = new Repository<Team>(_context);
            repoTeam.Update(ActualTeam);
            repoTeam.Update(OpponentTeam);

            return RedirectPreserveMethod($"/Challenge/{CurrentGame.Id}");
        }

        public IActionResult OnPostAnotherTurn()
        {
            if (IsChallengeValidate)
            {
                ActualTeam.Score++;

                Repository<Team> repoTeam = new Repository<Team>(_context);
                repoTeam.Update(ActualTeam);
            }

            return RedirectPreserveMethod($"/Challenge/{CurrentGame.Id}");
        }
    }
}
