using BillardHazard.Models;
using BillardHazard.Repositories;
using BillardHazard.Tools;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Text;

namespace BillardHazard.Pages
{
    public class EndGameModel : PageModel
    {
        private readonly BhContext _context;
        public string Message { get; set; }
        public Team RedTeam { get; set; }
        public Team YellowTeam { get; set; }

        public IList<HighScore> HighScores { get; set; } = default!;

        public EndGameModel(BhContext context)
        {
            _context = context;
        }

        public IActionResult OnPost(Guid gameId)
        {

            HighScores = _context.HighScores.ToList();
            HighScores = HighScores.OrderByDescending(hs => hs.Score).ToList();

            Repository<Team> repoTeam = new Repository<Team>(_context);
            Repository<Game> repoGame = new Repository<Game>(_context);

            List<Team> teams = repoTeam.GetAll().Where(t => t.GameId == gameId).ToList();

            Team winnerTeam = teams.OrderByDescending(t => t.Score).First();
            Message = $"L'équipe {winnerTeam.Name} ({winnerTeam.Color}) remporte la partie !";

            RedTeam = teams.First(t => t.Number == (int)ColorTeamEnum.Rouge);
            YellowTeam = teams.First(t => t.Number == (int)ColorTeamEnum.Jaune);

            foreach (Team team in teams)
            {
                SetHighScore(team);
                repoTeam.Delete(team);
            }
            
            Game game = repoGame.FindById(gameId);

            repoGame.Delete(game);

            return Page();
        }

        private void SetHighScore(Team team)
        {
            Repository<HighScore> repoHighScore = new Repository<HighScore>(_context);
            HighScore? worstHighScore = repoHighScore.GetAll().OrderBy(hs => hs.Score).FirstOrDefault();

            if (worstHighScore == null 
                || repoHighScore.GetAll().Count < 25
                || team.Score > worstHighScore.Score)
            {
                HighScore newHighScore = new HighScore();
                newHighScore.Score = team.Score;
                newHighScore.TeamName = team.Name;

                repoHighScore.Create(newHighScore);

                if (worstHighScore != null
                    && repoHighScore.GetAll().Count > 25)
                {
                    repoHighScore.Delete(worstHighScore);
                }
            }
        }
    }
}
