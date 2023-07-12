using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using BillardHazard.Models;

namespace BillardHazard.Pages.Rules
{
    public class CreateModel : PageModel
    {
        private readonly BhContext _context;

        public CreateModel(BhContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Rule Rule { get; set; } = default!;
        
        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid || _context.Rules == null || Rule == null)
            {
                return Page();
            }

            _context.Rules.Add(Rule);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
