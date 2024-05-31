using PolisProReminder.Application.InsuranceTypes.Dtos;

namespace PolisProReminder.Application.InsuranceTypes
{
    internal interface IInsuranceTypesService
    {
        Task<Guid> Create(CreateInsuranceTypeDto dto);
        Task<bool> Delete(Guid id);
        Task<IEnumerable<InsuranceTypeDto>> GetAll();
        Task<InsuranceTypeDto?> GetById(Guid id);
        Task<bool> Update(Guid id, CreateInsuranceTypeDto dto);
    }
}