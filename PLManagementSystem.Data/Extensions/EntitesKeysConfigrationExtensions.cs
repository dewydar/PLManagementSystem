using Microsoft.EntityFrameworkCore;
using PLManagementSystem.Core.Entities;

namespace PLManagementSystem.Data.Extensions
{
    public static class EntitesKeysConfigrationExtensions
    {
        public static void EntitesKeysConfigration(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ClassesUpgradeOrdering>().HasKey(a => new { a.LowerClassId, a.UpperClassId });
        }
    }
}
