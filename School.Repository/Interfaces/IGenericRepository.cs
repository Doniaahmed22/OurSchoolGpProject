using School.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.Repository.Interfaces
{
    public interface IGenericRepository<TEntity> where TEntity : BaseEntity
    {
        Task Update(TEntity entity);
        Task<IEnumerable<TEntity>> GetAll();
        Task<TEntity> GetById(int id);
        Task Add(TEntity entity);
        Task Delete(int id);
    }
}
