using Microsoft.EntityFrameworkCore;
using PLManagementSystem.Core.Entities;
using PLManagementSystem.Data.Extensions;

namespace PLManagementSystem.Data.Entites
{
    public partial class AppDbContext : DbContext
    {
        public AppDbContext()
        {
        }
        public AppDbContext(DbContextOptions<AppDbContext> options)
                   : base(options)
        {

        }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.EnableSensitiveDataLogging();
            //optionsBuilder.UseSqlServer("Data Source=.;Initial Catalog=PLManagementSystem;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=True;");

        }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Day> Dayes { get; set; }
        public virtual DbSet<Class> Classes { get; set; }
        public virtual DbSet<LessonGroups> LessonGroups { get; set; }
        public virtual DbSet<ClassesUpgradeOrdering> ClassesUpgradeOrdering { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region Query filter
            // add filter to return data where is deleted false ===> IsDeletedQueryFilter
            modelBuilder.IsDeletedQueryFilter();
            modelBuilder.Seed();
            modelBuilder.EntitesKeysConfigration();
            #endregion
        }
        public async override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return await base.SaveChangesAsync(cancellationToken);
        }
    }
}
