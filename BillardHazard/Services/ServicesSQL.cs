using BillardHazard.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace BillardHazard.Services
{
    public static class ServicesSQL
    {
        public static void DeleteGamesAndAssociatedTeams(BhContext _context)
        {
            IList<Team> teams = _context.Teams.ToList();
            IList<Game> games = _context.Games.ToList();

            foreach (Team team in teams)
            {
                _context.Remove(team);
            }

            foreach (Game game in games)
            {
                _context.Remove(game);
            }

            _context.SaveChanges();
        }

        /// <summary>
        /// Delete all Games old more than 2 days and their associated Teams are deleted
        /// </summary>
        /// <param name="_context"></param>
        public static void DeleteOldGames(BhContext _context)
        {
            DateTime limitDate = DateTime.Now.AddDays(-2);

            IList<Game> oldGames = _context.Games.Where(g => g.Beginning < limitDate).ToList();
            IList<Team> oldTeams = new List<Team>();

            foreach (Game game in oldGames)
            {
                //TODO oldTeams
            }

            foreach (Team team in oldTeams)
            {
                _context.Remove(team);
            }

            foreach (Game game in oldGames)
            {
                _context.Remove(game);
            }

            _context.SaveChanges();
        }
    }
}
