using Microsoft.EntityFrameworkCore;
using PLManagementSystem.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PLManagementSystem.Data.Extensions
{
    public static class IsDeletedQueryFilterExtensions
    {
        public static void IsDeletedQueryFilter(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasQueryFilter(x => !x.IsDeleted);
        }
    }
}
