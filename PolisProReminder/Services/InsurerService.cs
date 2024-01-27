using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PolisProReminder.Entities;
using PolisProReminder.Models.Insurer;

namespace PolisProReminder.Services
{
    public interface IInsurerService
    {
        IEnumerable<InsurerDto> GetAll();
    }

    public class InsurerService : IInsurerService
    {
        private readonly InsuranceDbContext _dbContext;
        private readonly IMapper _mapper;
        public InsurerService(InsuranceDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public IEnumerable<InsurerDto> GetAll()
        {
            var insurers = _dbContext
                .Insurers
                .Include(i => i.Policies.OrderBy(p => p.EndDate))
                .ThenInclude(p => p.InsuranceCompany)
                .Include(i => i.Policies)
                .ThenInclude(p => p.InsuranceTypes)
                .ToList();

            return _mapper.Map<List<InsurerDto>>(insurers);
        }
    }
}
