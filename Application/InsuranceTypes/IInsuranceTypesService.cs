using PolisProReminder.Application.InsuranceTypes.Dtos;

namespace PolisProReminder.Application.InsuranceTypes
{
    public interface IInsuranceTypesService
    {
        Task<Guid> Create(CreateInsuranceTypeDto dto);
        Task Delete(Guid id);
        Task<IEnumerable<InsuranceTypeDto>> GetAll();
        Task<InsuranceTypeDto?> GetById(Guid id);
        Task Update(Guid id, CreateInsuranceTypeDto dto);
    }
}