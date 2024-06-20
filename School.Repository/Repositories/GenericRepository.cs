using Microsoft.EntityFrameworkCore;
using School.Data.Context;
using School.Data.Entities;
using School.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.Repository.Repositories
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : BaseEntity
    {
        protected readonly SchoolDbContext _context;

        public GenericRepository(SchoolDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<TEntity>> GetAll()
        {
            return await _context.Set<TEntity>().ToListAsync();
        }
        public async Task<TEntity> GetById(int id)
        {
            return await _context.Set<TEntity>().FindAsync(id);
        }

        public async Task Add(TEntity entity)
        {
            _context.Set<TEntity>().Add(entity);
            await _context.SaveChangesAsync();
        }
        public void AddWithoutSave(TEntity entity)
        {
            _context.Set<TEntity>().Add(entity);
        }
        public async Task<int> Add2(TEntity entity)
        {
            _context.Set<TEntity>().Add(entity);
            await _context.SaveChangesAsync();
            return 0;
        }
        public async Task Update( TEntity new_entity)
        {
            _context.Set<TEntity>().Update(new_entity);
            await _context.SaveChangesAsync();

        }

        public async Task<TEntity> Delete(int id)
        {
            var entity = await GetById(id);
            if (entity != null)
            {
                _context.Set<TEntity>().Remove(entity);
                await _context.SaveChangesAsync();
                return entity;

            }
            return null;            
        }
    }
}
