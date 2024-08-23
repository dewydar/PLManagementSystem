using Microsoft.EntityFrameworkCore;
using PLManagementSystem.Core.Entities;

namespace PLManagementSystem.Data.Extensions
{
    public static class IsDeletedQueryFilterExtensions
    {
        public static void IsDeletedQueryFilter(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasQueryFilter(x => !x.IsDeleted);
            modelBuilder.Entity<Day>().HasQueryFilter(x => !x.IsDeleted);
            modelBuilder.Entity<Class>().HasQueryFilter(x => !x.IsDeleted);
            modelBuilder.Entity<LessonGroups>().HasQueryFilter(x => !x.IsDeleted);
        }
    }
}
