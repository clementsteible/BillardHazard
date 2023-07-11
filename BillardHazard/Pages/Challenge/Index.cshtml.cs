using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using BillardHazard.Models;
using System.Text.Json;
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
            RepoTeam = new Repository<Team>(_context);
            RepoGame = new Repository<Game>(_context);
        }

        Repository<Team> RepoTeam { get; set; }
        Repository<Game> RepoGame { get; set; }

        public IList<Rule> Rule { get; set; } = default!;
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

                CurrentGame = RepoGame.FindById(gameId) ?? new Game();
                List<Team> teams = RepoTeam.GetAll().Where(t => t.GameId == gameId).ToList();

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
            CurrentGameId = (Guid)TempData.Peek("CurrentGameId");

            ActualTeam = RepoTeam.GetAll().First(t => t.GameId.Equals(CurrentGameId) && t.IsItsTurn);
            OpponentTeam = RepoTeam.GetAll().First(t => t.GameId.Equals(CurrentGameId) && !t.IsItsTurn);

            if (IsChallengeValidate)
            {
                ActualTeam.Score++;
            }

            ActualTeam.IsItsTurn = !ActualTeam.IsItsTurn;
            OpponentTeam.IsItsTurn = !OpponentTeam.IsItsTurn;

            RepoTeam.Update(ActualTeam);
            RepoTeam.Update(OpponentTeam);

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
                ActualTeam = RepoTeam.GetAll().First(t => t.GameId.Equals(CurrentGameId) && t.IsItsTurn);

                ActualTeam.Score++;
                RepoTeam.Update(ActualTeam);
            }

            return RedirectPreserveMethod($"/Challenge/{CurrentGameId}");
        }

        /// <summary>
        /// Déclenche la fin de partie
        /// </summary>
        /// <returns></returns>
        public IActionResult OnPostEndGame()
        {
            CurrentGameId = (Guid)TempData.Peek("CurrentGameId");
            TempData.Remove("CurrentGameId");

            if (IsChallengeValidate)
            {
                ActualTeam = RepoTeam.GetAll().First(t => t.GameId.Equals(CurrentGameId) && t.IsItsTurn);

                ActualTeam.Score++;
                RepoTeam.Update(ActualTeam);
            }

            return RedirectPreserveMethod($"/EndGame/{CurrentGameId}");
        }
    }
}
