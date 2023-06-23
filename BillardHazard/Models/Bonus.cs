using System;
using System.Collections.Generic;

namespace BillardHazard.Models;

public partial class Bonus
{
    public int Id { get; set; }

    public int? Points { get; set; }

    public string? Explanation { get; set; }
}
