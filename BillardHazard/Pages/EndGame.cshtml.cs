using BillardHazard.Models;
using BillardHazard.Repositories;
using BillardHazard.Tools;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Drawing.Text;
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
        public Repository<Team> RepoTeam { get; set; }
        public Repository<Game> RepoGame { get; set; }

        public EndGameModel(BhContext context)
        {
            _context = context;

            RepoTeam = new Repository<Team>(_context);
            RepoGame = new Repository<Game>(_context);
        }

        public IActionResult OnPost(Guid gameId)
        {
            SetHighScoresTable();

            List<Team> teams = RepoTeam.GetAll().Where(t => t.GameId == gameId).ToList();

            Team winnerTeam = teams.OrderByDescending(t => t.Score).First();
            Message = $"L'équipe {winnerTeam.Name} ({winnerTeam.Color}) remporte la partie !";

            RedTeam = teams.First(t => t.Number == (int)ColorTeamEnum.Rouge);
            YellowTeam = teams.First(t => t.Number == (int)ColorTeamEnum.Jaune);

            foreach (Team team in teams)
            {
                SetHighScore(team);
                RepoTeam.Delete(team);
            }
            
            Game game = RepoGame.FindById(gameId);

            RepoGame.Delete(game);

            SetHighScoresTable();

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

        private void SetHighScoresTable()
        {
            HighScores = _context.HighScores.ToList();
            HighScores = HighScores.OrderByDescending(hs => hs.Score).ToList();
        }
    }
}
