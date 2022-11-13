using Entity.Base;
using System.Threading.Tasks;

namespace Dal.Abstractions
{
    public interface IUnitOfWork
    {
        IRepository<T> GetRepository<T>() where T : EntityBase;
        Task<int> SaveChangesAsync();
    }
}
