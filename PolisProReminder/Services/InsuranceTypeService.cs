using AutoMapper;
using PolisProReminder.Entities;
using PolisProReminder.Exceptions;
using PolisProReminder.Models.InsuranceType;

namespace PolisProReminder.Services
{
    public interface IInsuranceTypeService
    {
        int CreateInsuranceType(CreateInsuranceTypeDto dto);
        void DeleteInsuranceType(int id);
        IEnumerable<InsuranceTypeDto> GetAll();
        InsuranceTypeDto GetById(int id);
        void Update(int id, CreateInsuranceTypeDto dto);
    }

    public class InsuranceTypeService : IInsuranceTypeService
    {
        private readonly InsuranceDbContext _dbContext;
        private readonly IMapper _mapper;
        public InsuranceTypeService(InsuranceDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public void Update(int id, CreateInsuranceTypeDto dto)
        {
            var type = _dbContext
               .InsuranceTypes
               .FirstOrDefault(t => t.Id == id);

            if (type == null)
                throw new NotFoundException("Insurance Type does not exist");

            type.Name = dto.Name;

            _dbContext.SaveChanges();
        }
        public void DeleteInsuranceType(int id)
        {
            var type = _dbContext
               .InsuranceTypes
               .FirstOrDefault(t => t.Id == id);

            if (type == null)
                throw new NotFoundException("Insurance Type does not exist");

            _dbContext.InsuranceTypes.Remove(type);
            _dbContext.SaveChanges();
        }

        public int CreateInsuranceType(CreateInsuranceTypeDto dto)
        {
            var type = _dbContext
                .InsuranceTypes
                .FirstOrDefault(t => t.Name == dto.Name);

            if (type != null)
                throw new AlreadyExistsException("Insurance Type already exists");

            var createType = _mapper.Map<InsuranceType>(dto);
            _dbContext.InsuranceTypes.Add(createType);
            _dbContext.SaveChanges();

            return createType.Id;
        }

        public IEnumerable<InsuranceTypeDto> GetAll()
        {
            var types = _dbContext
                .InsuranceTypes
                .ToList();

            return _mapper.Map<List<InsuranceTypeDto>>(types);
        }

        public InsuranceTypeDto GetById(int id)
        {
            var type = _dbContext
                .InsuranceTypes
                .FirstOrDefault(i => i.Id == id);

            if (type == null)
                throw new NotFoundException("Insurance Type not found");

            return _mapper.Map<InsuranceTypeDto>(type);
        }
    }
}
