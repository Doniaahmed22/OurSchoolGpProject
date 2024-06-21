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
    public class MaterialRepository: GenericRepository<Material>,IMaterialRepository
    {
        public MaterialRepository(SchoolDbContext context) : base(context)
        {

        }
        public async Task< List<Material> >GetMaterialWithClassesAsync( MaterialType materialType, int teacherid, int levelid , int subjectid, int ?classid=null)
        {
            var query =  _context.Materials.Include(m => m.ClassMaterials).ThenInclude(cm => cm.Class)
                .Where(m => m.Type == materialType && m.TeacherId == teacherid && m.SubjectId == subjectid && m.Levelid == levelid);

            if (classid != null)
                query = query.Where(m => m.ClassMaterials.Any(cm => cm.ClassId == classid.Value));

           return  await query.ToListAsync();
        }/*
        public async Task<List<Material>> GetMaterialWithClassesAsync(MaterialType materialType, int teacherid, int levelid, int subjectid, int classid )
        {
            return await _context.Materials.Include(m => m.ClassMaterials).ThenInclude(cm => cm.Class)
                .Where(m => m.Type == materialType && m.TeacherId == teacherid && m.SubjectId == subjectid && m.Levelid == levelid 
                && m.ClassMaterials.Any(cm=>cm.ClassId==classid)).ToListAsync();
        }*/
        public async Task<Material> GetMaterialOfNameAsync(string matrialname,MaterialType materialType, int teacherid, int levelid, int subjectid)
        {
            return await _context.Materials.FirstOrDefaultAsync(m =>m.MaterialName==matrialname && m.Type == materialType && m.TeacherId == teacherid && m.SubjectId == subjectid && m.Levelid == levelid);
        }
    }
}
