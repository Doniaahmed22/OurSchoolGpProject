using Microsoft.EntityFrameworkCore;
using School.Data.Context;
using School.Data.Entities;
using School.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace School.Repository.Repositories
{
    public class SchoolRepository:GenericRepository<SchoolInfo>,ISchoolRepository
    {
        public SchoolRepository(SchoolDbContext context) : base(context) { }
        public async Task<Term>GetCurrentTerm()
        {
            
           SchoolInfo s = await _context.SchoolInfo.Include(s=>s.Term).FirstOrDefaultAsync();
            if (s == null)
                return null;
            return s.Term;
        }
        
        public int GetLimitAbsentDays()
        {
             return _context.SchoolInfo.Select(s=>s.LimitAbsentDays).First();
        }
        public async Task SetLimitAbsentDays(int LimitAbsentDays )
        {
            var schoool=await _context.SchoolInfo.FirstOrDefaultAsync();
            if(schoool != null)
            {
                schoool.LimitAbsentDays = LimitAbsentDays;
                await _context.SaveChangesAsync();
            }
        }
    }
}
