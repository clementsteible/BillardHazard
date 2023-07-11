using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BillardHazard.Models;

namespace BillardHazard.Pages.HighScores
{
    public class EditModel : PageModel
    {
        private readonly BillardHazard.BhContext _context;

        public EditModel(BillardHazard.BhContext context)
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

            var highscore =  await _context.HighScores.FirstOrDefaultAsync(m => m.Id == id);
            if (highscore == null)
            {
                return NotFound();
            }
            HighScore = highscore;
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(HighScore).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!HighScoreExists(HighScore.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool HighScoreExists(Guid id)
        {
          return (_context.HighScores?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
