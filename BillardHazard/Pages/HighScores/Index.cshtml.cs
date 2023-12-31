﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BillardHazard.Models;

namespace BillardHazard.Pages.HighScores
{
    public class IndexModel : PageModel
    {
        private readonly BhContext _context;

        public IndexModel(BhContext context)
        {
            _context = context;
        }

        public IList<HighScore> HighScores { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.HighScores != null)
            {
                HighScores = await _context.HighScores.ToListAsync();
                HighScores = HighScores.OrderByDescending(hs => hs.Score).ToList();
            }
        }

        public IActionResult OnPostEraseAllHighScores()
        {
            IList<HighScore> highScores = _context.HighScores.ToList();

            foreach (var highScore in highScores) {
                _context.Remove(highScore);
            }

            _context.SaveChanges();

            return Page();
        }
    }
}
