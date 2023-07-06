using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BillardHazard;
using BillardHazard.Models;
using BillardHazard.Repositories;

namespace BillardHazard.Pages.Games
{
    public class IndexModel : PageModel
    {
        private readonly BhContext _context;

        public IndexModel(BhContext context)
        {
            _context = context;
        }

        public IList<Game> Games { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.Games != null)
            {
                Games = await _context.Games.ToListAsync();
            }
        }

        public IActionResult OnGetEraseAllGames()
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

            return Page();
        }
    }
}
