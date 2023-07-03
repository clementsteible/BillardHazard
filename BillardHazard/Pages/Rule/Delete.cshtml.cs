﻿using System;
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
    public class DeleteModel : PageModel
    {
        private readonly BillardHazard.BhContext _context;

        public DeleteModel(BillardHazard.BhContext context)
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
