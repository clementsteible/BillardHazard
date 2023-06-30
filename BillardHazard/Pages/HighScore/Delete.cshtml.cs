using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BillardHazard;
using BillardHazard.Models;

namespace BillardHazard.Pages.HighScore
{
    public class DeleteModel : PageModel
    {
        private readonly BillardHazard.BhContext _context;

        public DeleteModel(BillardHazard.BhContext context)
        {
            _context = context;
        }

        [BindProperty]
      public Models.HighScore HighScore { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null || _context.HighScores == null)
            {
                return NotFound();
            }

            var highscore = await _context.HighScores.FirstOrDefaultAsync(m => m.Id == id);

            if (highscore == null)
            {
                return NotFound();
            }
            else 
            {
                HighScore = highscore;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(Guid? id)
        {
            if (id == null || _context.HighScores == null)
            {
                return NotFound();
            }
            var highscore = await _context.HighScores.FindAsync(id);

            if (highscore != null)
            {
                HighScore = highscore;
                _context.HighScores.Remove(HighScore);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
