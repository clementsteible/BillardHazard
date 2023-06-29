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

        public async Task OnGetAsync(int teamNumber)
        {
            if (_context.Rules != null)
            {
                Rule = await _context.Rules.ToListAsync();

                JsonRules = JsonSerializer.Serialize(Rule, new JsonSerializerOptions { WriteIndented = true });

                var ballColorStatut = (CssClassBallEnum)teamNumber;
                CssBallColorClass = ballColorStatut.ToString();

                var actualTeamColorStatut = (TeamEnum)teamNumber;
                ActualTeam.Color = actualTeamColorStatut.ToString();
                ActualTeam.Number = teamNumber;

                OpponentTeam.Number = teamNumber == 1 ? 2 : 1;
                var oponentTeamColorStatut = (TeamEnum) OpponentTeam.Number;
                OpponentTeam.Color = oponentTeamColorStatut.ToString();
            }
        }

        public RedirectToPageResult OpponentTurn()
        {
            return new RedirectToPageResult("/Challenge/");
        }
    }
}
