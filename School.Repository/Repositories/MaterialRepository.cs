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
        public async Task< List<Material> >GetMaterialWithClassesAsync( MaterialType materialType, int teacherid, int levelid , int subjectid )
        {
            return await _context.Materials.Include(m=>m.ClassMaterials).ThenInclude(cm=>cm.Class)
                .Where(m=>m.Type == materialType&&m.TeacherId==teacherid && m.SubjectId == subjectid&&m.Levelid == levelid).ToListAsync();
        }
    }
}
