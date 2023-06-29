using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BillardHazard;
using BillardHazard.Models;

namespace BillardHazard.Pages.Rule
{
    public class DetailsModel : PageModel
    {
        private readonly BillardHazard.BhContext _context;

        public DetailsModel(BillardHazard.BhContext context)
        {
            _context = context;
        }

      public Models.Rule Rule { get; set; } = default!; 

        public async Task<IActionResult> OnGetAsync(int? id)
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
    }
}
