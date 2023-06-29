using Microsoft.EntityFrameworkCore;

namespace BillardHazard.Tools
{
    public static class DbContextExtensions
    {
        public static string[] GetEntityNames(this DbContext context)
        {
            return context.GetType().GetProperties()
                .Where(p => p.PropertyType.IsGenericType && p.PropertyType.GetGenericTypeDefinition() == typeof(DbSet<>))
                .Select(p => p.PropertyType.GetGenericArguments().First().Name)
                .ToArray();
        }
    }
}
