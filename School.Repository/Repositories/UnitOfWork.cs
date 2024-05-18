using Microsoft.EntityFrameworkCore;
using School.Data.Context;
using School.Data.Entities;
using School.Repository.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.Repository.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly SchoolDbContext _context;
        private Hashtable _repositores;

        public UnitOfWork(SchoolDbContext context) 
        {
            _context = context;
        }
        public async Task<int> CompleteAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public IGenericRepository<TEntity> repository<TEntity>() where TEntity : BaseEntity
        {
            if (_repositores is null)
            {
                _repositores = new Hashtable();
            }
            var entityKey = typeof(TEntity).Name;
            if (!_repositores.ContainsKey(entityKey))
            {
                

                var repositoryType = typeof(GenericRepository<>);
                var repositoryInstance = Activator.CreateInstance(repositoryType.MakeGenericType(typeof(TEntity)), _context);
                //_repositores.Add(entityKey, repositoryInstance);
               // 
           
                 if (typeof(TEntity) == typeof(SubjectLevelDepartmentTerm))
                    _repositores.Add(entityKey, new SubjectRecordRepository(_context));
                else if (typeof(TEntity) == typeof(Class))
                    _repositores.Add(entityKey, new ClassRepository(_context));
                else if (typeof(TEntity) == typeof(Student))
                    _repositores.Add(entityKey, new StudentRepository(_context));
                else if (typeof(TEntity) == typeof(StudentSubject))
                    _repositores.Add(entityKey, new GradeRepository(_context));
                else if (typeof(TEntity) == typeof(Teacher))
                    _repositores.Add(entityKey, new TeacherRepository(_context));
                else if (typeof(TEntity) == typeof(SchoolInfo))
                    _repositores.Add(entityKey, new SchoolRepository(_context));
                else
                    _repositores.Add(entityKey, repositoryInstance);
                 
            }
            return (IGenericRepository<TEntity>)_repositores[entityKey];
        }
    }
}
