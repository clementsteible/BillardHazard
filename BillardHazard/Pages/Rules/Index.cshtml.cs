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
    public class IndexModel : PageModel
    {
        private readonly BillardHazard.BhContext _context;

        public IndexModel(BillardHazard.BhContext context)
        {
            _context = context;
        }

        public IList<Models.Rule> Rule { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.Rules != null)
            {
                Rule = await _context.Rules.ToListAsync();
            }
        }
    }
}
