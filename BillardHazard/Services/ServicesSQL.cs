using BillardHazard.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace BillardHazard.Services
{
    /// <summary>
    /// Services to interact with DB
    /// </summary>
    public static class ServicesSQL
    {
        /// <summary>
        /// Delete games passed in params and their associated teams
        /// </summary>
        /// <param name="dbContext">DbContext</param>
        /// <param name="games">List of games</param>
        public static void DeleteGamesAndAssociatedTeams(BhContext dbContext, List<Game> games)
        {
            foreach (Game game in games)
            {
                foreach (Team team in game.Teams)
                {
                    dbContext.Remove(team);
                }

                dbContext.Remove(game);
            }

            dbContext.SaveChanges();
        }

        /// <summary>
        /// Delete all Games and their associated Teams
        /// </summary>
        /// <param name="_context">DbContext</param>
        public static void EraseAllGames(BhContext dbContext)
        {
            List<Game> oldGames = dbContext.Games.Include(g => g.Teams).ToList();

            DeleteGamesAndAssociatedTeams(dbContext, oldGames);
        }

        /// <summary>
        /// Delete all Games old more than 2 days and their associated Teams
        /// </summary>
        /// <param name="_context">DbContext</param>
        public static void DeleteOldGames(BhContext dbContext, int daysInThePast)
        {
            DateTime limitDate = DateTime.Now.AddDays(-daysInThePast);

            List<Game> oldGames = dbContext.Games.Where(g => g.Beginning < limitDate).Include(g => g.Teams).ToList();

            DeleteGamesAndAssociatedTeams(dbContext, oldGames);
        }
    }
}
