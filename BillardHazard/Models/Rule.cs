using System;
using System.Collections.Generic;

namespace BillardHazard.Models;

public partial class Rule
{
    public Guid Id { get; set; }

    public string? Explanation { get; set; }
}
