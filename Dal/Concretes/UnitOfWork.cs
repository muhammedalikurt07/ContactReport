using Dal.Abstractions;
using Dal.Contexts;
using Entity.Base;
using System.Threading.Tasks;

namespace Dal.Concretes
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ContactDbContext _Context;

        public UnitOfWork(ContactDbContext _context)
        {
            _Context = _context;
        }
        public IRepository<T> GetRepository<T>() where T : EntityBase =>
             new Repository<T>(_Context);

        public Task<int> SaveChangesAsync() => _Context.SaveChangesAsync();
    }
}
