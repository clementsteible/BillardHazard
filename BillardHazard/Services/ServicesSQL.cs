using BillardHazard.Models;
using Microsoft.EntityFrameworkCore;

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
    }
}
