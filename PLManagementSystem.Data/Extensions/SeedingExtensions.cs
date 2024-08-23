using Microsoft.EntityFrameworkCore;
using PLManagementSystem.Core.Entities;

namespace PLManagementSystem.Data.Extensions
{
    public static class SeedingExtensions
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Dayes();
            modelBuilder.ClassesSeeding();
            modelBuilder.ClassesUpgradeOrdering();
        }
        #region Private Seeding
        private static void Dayes(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Day>().HasData(
                new Day
                {
                    Id = 1,
                    Name = "Saturday",
                    IsDeleted = false
                }, new Day
                {
                    Id = 2,
                    Name = "Sunday",
                    IsDeleted = false
                }, new Day
                {
                    Id = 3,
                    Name = "Monday",
                    IsDeleted = false
                }, new Day
                {
                    Id = 4,
                    Name = "Tuesday",
                    IsDeleted = false
                }, new Day
                {
                    Id = 5,
                    Name = "Wednesday",
                    IsDeleted = false
                }, new Day
                {
                    Id = 6,
                    Name = "Thursday",
                    IsDeleted = false
                }, new Day
                {
                    Id = 7,
                    Name = "Friday",
                    IsDeleted = false
                }
            );
        }
        private static void ClassesSeeding(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Class>().HasData(
                new Class
                {
                    Id = 1,
                    Name = "First Grade Primary",
                    ShortName = "First Primary",
                    OrderNo = 1,
                    IsDeleted = false
                }, new Class
                {
                    Id = 2,
                    Name = "Second Grade Primary",
                    ShortName = "Second Primary",
                    OrderNo = 2,
                    IsDeleted = false
                }, new Class
                {
                    Id = 3,
                    Name = "Third Grade Primary",
                    ShortName = "Third Primary",
                    OrderNo = 3,
                    IsDeleted = false
                }, new Class
                {
                    Id = 4,
                    Name = "Fourth Grade Primary",
                    ShortName = "Fourth Primary",
                    OrderNo = 4,
                    IsDeleted = false
                }, new Class
                {
                    Id = 5,
                    Name = "Fifth Grade Primary",
                    ShortName = "Fifth Primary",
                    OrderNo = 5,
                    IsDeleted = false
                }, new Class
                {
                    Id = 6,
                    Name = "Sixth Grade Primary",
                    ShortName = "Sixth Primary",
                    OrderNo = 6,
                    IsDeleted = false
                }, new Class
                {
                    Id = 7,
                    Name = "First Grade Preparatory",
                    ShortName = "First Prep",
                    OrderNo = 7,
                    IsDeleted = false
                }, new Class
                {
                    Id = 8,
                    Name = "Second Grade Preparatory",
                    ShortName = "Second Prep",
                    OrderNo = 8,
                    IsDeleted = false
                }, new Class
                {
                    Id = 9,
                    Name = "Third Grade Preparatory",
                    ShortName = "Third Prep",
                    OrderNo = 9,
                    IsDeleted = false
                }, new Class
                {
                    Id = 10,
                    Name = "First Grade Secondary",
                    ShortName = "First Secondary",
                    OrderNo = 10,
                    IsDeleted = false
                }, new Class
                {
                    Id = 11,
                    Name = "Second Grade Secondary",
                    ShortName = "Second Secondary",
                    OrderNo = 11,
                    IsDeleted = false
                }, new Class
                {
                    Id = 12,
                    Name = "Third Grade Secondary",
                    ShortName = "Third Secondary",
                    OrderNo = 12,
                    IsDeleted = false
                }
            );
        }
        private static void ClassesUpgradeOrdering(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ClassesUpgradeOrdering>().HasData(
                new ClassesUpgradeOrdering
                {
                    LowerClassId = 1,
                    UpperClassId = 2,
                    IsDeleted = false
                }, new ClassesUpgradeOrdering
                {
                    LowerClassId = 2,
                    UpperClassId = 3,
                    IsDeleted = false
                }, new ClassesUpgradeOrdering
                {
                    LowerClassId = 3,
                    UpperClassId = 4,
                    IsDeleted = false
                }, new ClassesUpgradeOrdering
                {
                    LowerClassId = 4,
                    UpperClassId = 5,
                    IsDeleted = false
                }, new ClassesUpgradeOrdering
                {
                    LowerClassId = 5,
                    UpperClassId = 6,
                    IsDeleted = false
                }, new ClassesUpgradeOrdering
                {
                    LowerClassId = 6,
                    UpperClassId = 7,
                    IsDeleted = false
                }, new ClassesUpgradeOrdering
                {
                    LowerClassId = 7,
                    UpperClassId = 8,
                    IsDeleted = false
                }, new ClassesUpgradeOrdering
                {
                    LowerClassId = 8,
                    UpperClassId = 9,
                    IsDeleted = false
                }, new ClassesUpgradeOrdering
                {
                    LowerClassId = 9,
                    UpperClassId = 10,
                    IsDeleted = false
                }, new ClassesUpgradeOrdering
                {
                    LowerClassId = 10,
                    UpperClassId = 11,
                    IsDeleted = false
                }, new ClassesUpgradeOrdering
                {
                    LowerClassId = 11,
                    UpperClassId = 12,
                    IsDeleted = false
                }
            );
        }
        #endregion
    }
}
