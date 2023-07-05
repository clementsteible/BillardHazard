using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using BillardHazard;
using BillardHazard.Models;

namespace BillardHazard.Pages.HighScores
{
    public class CreateModel : PageModel
    {
        private readonly BillardHazard.BhContext _context;

        public CreateModel(BillardHazard.BhContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Models.HighScore HighScore { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid || _context.HighScores == null || HighScore == null)
            {
                return Page();
            }

            _context.HighScores.Add(HighScore);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
