using School.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.Repository.Interfaces
{
    public interface IUnitOfWork
    {
        IGenericRepository<TEntity> repository<TEntity>() where TEntity : BaseEntity;
        Task<int> CompleteAsync();
    }
}
