using PolisProReminder.Application.Insurers.Dtos;

namespace PolisProReminder.Application.Insurers
{
    internal interface IInsurersService
    {
        Task<Guid> Create(CreateInsurerDto dto);
        Task<bool> Delete(Guid id);
        Task<IEnumerable<InsurerDto>> GetAll();
        Task<InsurerDto?> GetById(Guid id);
        Task<bool> Update(Guid id, CreateInsurerDto dto);
    }
}