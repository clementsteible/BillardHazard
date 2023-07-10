using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BillardHazard;
using BillardHazard.Models;

namespace BillardHazard.Pages.Rules
{
    public class IndexModel : PageModel
    {
        private readonly BhContext _context;

        public IndexModel(BhContext context)
        {
            _context = context;
        }

        public IList<Rule> Rules { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.Rules != null)
            {
                Rules = await _context.Rules.ToListAsync();
            }
        }

        public IActionResult OnPostEraseAllRules()
        {
            List<Rule> rules = _context.Rules.ToList();

            foreach (Rule rule in rules)
            {
                _context.Remove(rule);
            }

            _context.SaveChanges();

            return Page();
        }
    }
}
