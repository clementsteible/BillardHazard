using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BillardHazard;
using BillardHazard.Models;

namespace BillardHazard.Pages.Rule
{
    public class EditModel : PageModel
    {
        private readonly BillardHazard.BhContext _context;

        public EditModel(BillardHazard.BhContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Models.Rule Rule { get; set; } = default!;

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
