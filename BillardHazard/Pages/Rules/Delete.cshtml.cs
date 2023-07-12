using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BillardHazard.Models;

namespace BillardHazard.Pages.Rules
{
    public class DeleteModel : PageModel
    {
        private readonly BhContext _context;

        public DeleteModel(BhContext context)
        {
            _context = context;
        }

        [BindProperty]
      public Rule Rule { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null || _context.Rules == null)
            {
                return NotFound();
            }

            var rule = await _context.Rules.FirstOrDefaultAsync(m => m.Id == id);

            if (rule == null)
            {
                return NotFound();
            }
            else 
            {
                Rule = rule;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(Guid? id)
        {
            if (id == null || _context.Rules == null)
            {
                return NotFound();
            }
            var rule = await _context.Rules.FindAsync(id);

            if (rule != null)
            {
                Rule = rule;
                _context.Rules.Remove(Rule);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
