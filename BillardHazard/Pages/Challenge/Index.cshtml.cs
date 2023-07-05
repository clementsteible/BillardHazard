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
using Microsoft.AspNetCore.Mvc.ViewFeatures;

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
        public Guid CurrentGameId { get; set; }
        public Game CurrentGame { get; set; }
        public Team ActualTeam { get; set; } = new Team();
        public Team OpponentTeam { get; set; } = new Team();
        public string CssBallCurrentColorClass { get; set; }
        public string CssBallOpponentColorClass { get; set; }
        [BindProperty]
        public bool IsChallengeValidate { get; set; }

        //TODO Remove when ok
        public IActionResult OnPost(Guid gameId)
        {
            if (_context.Rules != null)
            {
                Rule = _context.Rules.ToList();

                TempData["CurrentGameId"] = gameId;
                TempData.Keep("CurrentGameId");

                JsonRules = JsonSerializer.Serialize(Rule, new JsonSerializerOptions { WriteIndented = true });

                Repository<Game> repoGame = new Repository<Game>(_context);
                Repository<Team> repoTeam = new Repository<Team>(_context);

                CurrentGame = repoGame.FindById(gameId) ?? new Game();
                List<Team> teams = repoTeam.GetAll().Where(t => t.GameId == gameId).ToList();

                ActualTeam = teams.First(t => t.IsItsTurn);
                OpponentTeam = teams.First(t => !t.IsItsTurn);

                CssBallCurrentColorClass = ((CssClassBallEnum)ActualTeam.Number).ToString();
                CssBallOpponentColorClass = ((CssClassBallEnum)OpponentTeam.Number).ToString();

                IsChallengeValidate = false;
            }

            return Page();
        }

        //Faire des Radios input pour IsChallengeValidate
        public IActionResult OnPostOpponentTurn()
        {
            Repository<Team> repoTeam = new Repository<Team>(_context);

            CurrentGameId = (Guid)TempData.Peek("CurrentGameId");

            ActualTeam = repoTeam.GetAll().First(t => t.GameId.Equals(CurrentGameId) && t.IsItsTurn);
            OpponentTeam = repoTeam.GetAll().First(t => t.GameId.Equals(CurrentGameId) && !t.IsItsTurn);

            if (IsChallengeValidate)
            {
                ActualTeam.Score++;
            }

            ActualTeam.IsItsTurn = !ActualTeam.IsItsTurn;
            OpponentTeam.IsItsTurn = !OpponentTeam.IsItsTurn;

            repoTeam.Update(ActualTeam);
            repoTeam.Update(OpponentTeam);

            return RedirectPreserveMethod($"/Challenge/{CurrentGameId}");
        }

        /// <summary>
        /// Tour Supplémentaire pour l'équipe actuelle
        /// </summary>
        /// <returns></returns>
        public IActionResult OnPostAnotherTurn()
        {
            CurrentGameId = (Guid)TempData.Peek("CurrentGameId");

            if (IsChallengeValidate)
            {
                Repository<Team> repoTeam = new Repository<Team>(_context);

                ActualTeam = repoTeam.GetAll().First(t => t.GameId.Equals(CurrentGameId) && t.IsItsTurn);

                ActualTeam.Score++;
                repoTeam.Update(ActualTeam);
            }

            return RedirectPreserveMethod($"/Challenge/{CurrentGameId}");
        }
    }
}
