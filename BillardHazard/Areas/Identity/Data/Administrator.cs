using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace BillardHazard.Areas.Identity.Data;

// Add profile data for application users by adding properties to the Administrator class
public class Administrator : IdentityUser
{
}

