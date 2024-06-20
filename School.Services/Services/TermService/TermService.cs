using School.Data.Entities;
using School.Repository.Interfaces;
using School.Services.Dtos.SharedDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.Services.Services.TermService
{
    public class TermService: ITermService
    {
        private readonly IUnitOfWork _unitOfWork;

        public TermService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<NameIdDto>> GetAllTermsForList()
        {
            List<NameIdDto> TermsDto = new List<NameIdDto>();
            var Terms = await _unitOfWork.repository<Term>().GetAll();
            foreach (var Term in Terms)
            {
                TermsDto.Add(new NameIdDto() { Id = Term.Id, Name = Term.Name });
            }
            return TermsDto;
        }
    }
}
