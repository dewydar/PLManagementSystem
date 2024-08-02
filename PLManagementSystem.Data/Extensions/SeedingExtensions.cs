using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic.FileIO;
using PLManagementSystem.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace PLManagementSystem.Data.Extensions
{
    public static class SeedingExtensions
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Dayes();
            modelBuilder.ClassesSeeding();
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
                    Name = "Third Grade Primary",
                    ShortName = "Third Primary",
                    OrderNo=1,
                    IsDeleted = false
                }, new Class
                {
                    Id = 2,
                    Name = "Fourth Grade Primary",
                    ShortName = "Fourth Primary",
                    OrderNo = 2,
                    IsDeleted = false
                }, new Class
                {
                    Id = 3,
                    Name = "Fifth Grade Primary",
                    ShortName = "Fifth Primary",
                    OrderNo = 3,
                    IsDeleted = false
                }, new Class
                {
                    Id = 4,
                    Name = "Sixth Grade Primary",
                    ShortName = "Sixth Primary",
                    OrderNo = 4,
                    IsDeleted = false
                }, new Class
                {
                    Id = 5,
                    Name = "First Grade Preparatory",
                    ShortName = "First Prep",
                    OrderNo = 5,
                    IsDeleted = false
                }, new Class
                {
                    Id = 6,
                    Name = "Second Grade Preparatory",
                    ShortName = "Second Prep",
                    OrderNo = 6,
                    IsDeleted = false
                }, new Class
                {
                    Id = 7,
                    Name = "Third Grade Preparatory",
                    ShortName = "Third Prep",
                    OrderNo = 7,
                    IsDeleted = false
                }, new Class
                {
                    Id = 8,
                    Name = "First Grade Secondary",
                    ShortName = "First Secondary",
                    OrderNo = 8,
                    IsDeleted = false
                }, new Class
                {
                    Id = 9,
                    Name = "Second Grade Secondary",
                    ShortName = "Second Secondary",
                    OrderNo = 9,
                    IsDeleted = false
                }, new Class
                {
                    Id = 10,
                    Name = "Third Grade Secondary",
                    ShortName = "Third Secondary",
                    OrderNo = 10,
                    IsDeleted = false
                }, new Class
                {
                    Id = 11,
                    Name = "First Grade Primary",
                    ShortName = "First Primary",
                    OrderNo = 11,
                    IsDeleted = false
                }, new Class
                {
                    Id = 12,
                    Name = "Second Grade Primary",
                    ShortName = "Second Primary",
                    OrderNo = 12,
                    IsDeleted = false
                }
            );
        }
        #endregion
    }
}
