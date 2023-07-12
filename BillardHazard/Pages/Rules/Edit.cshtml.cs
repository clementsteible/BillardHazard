using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BillardHazard.Models;

namespace BillardHazard.Pages.Rules
{
    public class EditModel : PageModel
    {
        private readonly BhContext _context;

        public EditModel(BhContext context)
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

            var rule =  await _context.Rules.FirstOrDefaultAsync(m => m.Id == id);
            if (rule == null)
            {
                return NotFound();
            }
            Rule = rule;
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

            _context.Attach(Rule).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RuleExists(Rule.Id))
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

        private bool RuleExists(Guid id)
        {
          return (_context.Rules?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
