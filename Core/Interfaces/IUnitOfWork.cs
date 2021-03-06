using System;
using System.Threading.Tasks;
using Core.Entities;

namespace Core.Intefraces
{
    public interface IUnitOfWork: IDisposable 
    {
        IGenericRepository<TEntity> Repository<TEntity>() where TEntity : BaseEntity;
        Task<int> Complete();
    }
}
