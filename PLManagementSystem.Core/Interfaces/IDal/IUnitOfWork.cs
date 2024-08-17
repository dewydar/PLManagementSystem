namespace PLManagementSystem.Core.Interfaces.IDal
{
    public interface IUnitOfWork
    {
        Task Commit();
    }
}
