using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BillardHazard.Models;
using BillardHazard.Services;

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

        public IActionResult OnPostEraseAllGames()
        {
            ServicesSQL.DeleteGamesAndAssociatedTeams(_context);

            return Page();
        }
    }
}
