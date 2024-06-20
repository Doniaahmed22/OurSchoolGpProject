using School.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.Repository.Interfaces
{
    public interface IMaterialRepository:IGenericRepository<Material>
    {
        Task<List<Material>> GetMaterialWithClassesAsync(MaterialType materialType, int teacherid, int levelid, int subjectid);

    }
}
