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
    public class ParentRepository : IParentRepository
    {
        private readonly SchoolDbContext _context;

        public ParentRepository(SchoolDbContext context) 
        {
            _context = context;
        }
        public async Task<List<Student>> GetStudentsOfParents(int id)
        {
            var students = await _context.Students.Where(x => x.ParentId == id).ToListAsync();
            return students;
        }

    }
}
