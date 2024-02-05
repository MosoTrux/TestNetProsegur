using TestNetProsegur.Core.Entities;

namespace TestNetProsegur.Core.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<Order> OrderRepository { get; }
        IRepository<Product> ProductRepository { get; }

        void SaveChanges();
        Task SaveChangesAsync();

        void BeginTransaction();
        void CommitTransaction();
        void RollbackTransaction();
    }
}
