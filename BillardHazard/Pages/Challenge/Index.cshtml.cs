using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BillardHazard.Models;
using System.Text.Json;
using System.Text.Json.Serialization;
using BillardHazard.Tools;

namespace BillardHazard.Pages.Challenge
{
    public class IndexModel : PageModel
    {
        private readonly BhContext _context;

        public IndexModel(BhContext context)
        {
            _context = context;
        }

        public IList<Models.Rule> Rule { get; set; } = default!;
        public string JsonRules { get; set; } = default!;
        public Team ActualTeam { get; set; } = new Team();
        public Team OpponentTeam { get; set; } = new Team();
        public string CssBallColorClass { get; set; }

        /*
        public async Task OnGetAsync(int teamNumber, Game game)
        {
            if (_context.Rules != null)
            {
                Rule = await _context.Rules.ToListAsync();

                JsonRules = JsonSerializer.Serialize(Rule, new JsonSerializerOptions { WriteIndented = true });

                this.ActualTeam = game.Teams.ToList().First(t => t.Number == teamNumber);
                this.OpponentTeam = game.Teams.ToList().First(t => t.Number != teamNumber);
            }
        }
        */

        //Temporaire TODO Remove when ok
        public async Task OnGetAsync(int teamNumber)
        {
            if (_context.Rules != null)
            {
                Rule = await _context.Rules.ToListAsync();

                JsonRules = JsonSerializer.Serialize(Rule, new JsonSerializerOptions { WriteIndented = true });
            }
        }

        public RedirectToPageResult OpponentTurn()
        {
            return new RedirectToPageResult("/Challenge/");
        }
    }
}
