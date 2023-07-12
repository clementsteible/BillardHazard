using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BillardHazard.Models;

namespace BillardHazard.Pages.Rules
{
    public class IndexModel : PageModel
    {
        private readonly BhContext _context;
        public string NumberOfTotalRulesMessage { get; set; }

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
                NumberOfTotalRulesMessage = $"Il y a {Rules.Count} rules au total en BDD.";
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
