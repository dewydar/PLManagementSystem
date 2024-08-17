using PLManagementSystem.Core.Interfaces.IDal;
using PLManagementSystem.Data.Entites;

namespace PLManagementSystem.Data.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private AppDbContext _dbContext;
        public UnitOfWork(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task Commit()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}
