using Microsoft.AspNetCore.Http;
using School.Data.Entities;
using School.Services.Dtos.MaterialDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.Services.Services.MaterialService
{
    public interface IMaterialService
    {
        Task<string> UploadMaterial(IFormFile File, MaterialType materialType, MaterialAddDto dto);

    }
}
