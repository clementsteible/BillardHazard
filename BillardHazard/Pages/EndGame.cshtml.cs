using BillardHazard.Models;
using BillardHazard.Repositories;
using BillardHazard.Tools;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
namespace BillardHazard.Pages
{
    public class EndGameModel : PageModel
    {
        private readonly BhContext _context;
        private readonly int MAXIMUM_NUMBER_OF_HIGHSCORES = 25;
        /// <summary>
        /// Message display on page
        /// </summary>
        public string Message { get; set; }
        public Team RedTeam { get; set; }
        public Team YellowTeam { get; set; }

        public List<HighScore> HighScores { get; set; }
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

            // Set highscores and clean the DB
            foreach (Team team in teams)
            {
                SetHighScore(team);
                RepoTeam.Delete(team);
            }
            
            Game game = RepoGame.FindById(gameId);

            // Set highscores and clean the DB
            RepoGame.Delete(game);

            // Update Highscores
            SetHighScoresTable();

            return Page();
        }

        /// <summary>
        /// Set team score in the Highscores table if team is in the top teams
        /// </summary>
        /// <param name="team"></param>
        private void SetHighScore(Team team)
        {
            Repository<HighScore> repoHighScore = new Repository<HighScore>(_context);
            // Retrieve the worst highscore among all highscores
            HighScore? worstHighScore = repoHighScore.GetAll().OrderBy(hs => hs.Score).FirstOrDefault();

            // If the actual team score is better than the worst score or if there is less than the maximum number of highscores in total...
            if (worstHighScore == null 
                || repoHighScore.GetAll().Count < MAXIMUM_NUMBER_OF_HIGHSCORES
                || team.Score > worstHighScore.Score)
            {
                HighScore newHighScore = new HighScore();
                newHighScore.Score = team.Score;
                newHighScore.TeamName = team.Name;

                // ... create new entry in the Hisghscore's table
                repoHighScore.Create(newHighScore);

                // If there is more than maximum number of highscores, delete the worse
                if (worstHighScore != null
                    && repoHighScore.GetAll().Count > MAXIMUM_NUMBER_OF_HIGHSCORES)
                {
                    repoHighScore.Delete(worstHighScore);
                }
            }
        }

        /// <summary>
        /// Retrieve and class highscores for table display
        /// </summary>
        private void SetHighScoresTable()
        {
            HighScores = _context.HighScores.ToList();
            HighScores = HighScores.OrderByDescending(hs => hs.Score).ToList();
        }
    }
}
