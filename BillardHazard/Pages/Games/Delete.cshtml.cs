using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BillardHazard.Models;

namespace BillardHazard.Pages.Games
{
    public class DeleteModel : PageModel
    {
        private readonly BhContext _context;

        public DeleteModel(BhContext context)
        {
            _context = context;
        }

      [BindProperty]
      public Game Game { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null || _context.Games == null)
            {
                return NotFound();
            }

            var game = await _context.Games.FirstOrDefaultAsync(m => m.Id == id);

            if (game == null)
            {
                return NotFound();
            }
            else 
            {
                Game = game;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(Guid? id)
        {
            if (id == null || _context.Games == null)
            {
                return NotFound();
            }

            var game = await _context.Games.FindAsync(id);

            if (game != null)
            {
                List<Team> teamsAssociated = _context.Teams.Where(t => t.GameId == id).ToList();
                
                if (teamsAssociated != null && teamsAssociated.Count > 0)
                {
                    foreach (Team team in teamsAssociated)
                    {
                        _context.Teams.Remove(team);
                    }
                }

                Game = game;
                _context.Games.Remove(Game);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
