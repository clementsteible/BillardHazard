using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BillardHazard.Models;
using BillardHazard.Tools;

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
        public List<Rule> DefaultRulesList { get; set; }

        public async Task OnGetAsync()
        {
            if (_context.Rules != null)
            {
                Rules = await _context.Rules.ToListAsync();
                NumberOfTotalRulesMessage = $"Il y a {Rules.Count} rules au total en BDD.";

            }

            DefaultRulesList = DefaultRules.DefaultRulesList;
        }

        public IActionResult OnPostResetDefaultRules()
        {
            List<Rule> rules = _context.Rules.ToList();

            foreach (Rule rule in rules)
            {
                _context.Rules.Remove(rule);
            }

            foreach (Rule rule in DefaultRules.DefaultRulesList)
            {
                _context.Rules.Add(rule);
            }

            _context.SaveChanges();

            return RedirectToPage("./Index");
        }

        public IActionResult OnPostEraseAllRules()
        {
            List<Rule> rules = _context.Rules.ToList();

            foreach (Rule rule in rules)
            {
                _context.Remove(rule);
            }

            _context.SaveChanges();

            return RedirectToPage("./Index");
        }
    }
}
