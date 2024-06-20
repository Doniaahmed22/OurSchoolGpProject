using School.Services.Dtos.SharedDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.Services.Services.TermService
{
    public interface ITermService
    {
        Task<IEnumerable<NameIdDto>> GetAllTermsForList();

    }
}
