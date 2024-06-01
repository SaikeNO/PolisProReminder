using PolisProReminder.Application.Insurers.Dtos;

namespace PolisProReminder.Application.Insurers
{
    public interface IInsurersService
    {
        Task<Guid> Create(CreateInsurerDto dto);
        Task<bool> Delete(Guid id);
        Task<IEnumerable<InsurerDto>> GetAll();
        Task<InsurerDto?> GetById(Guid id);
        Task Update(Guid id, CreateInsurerDto dto);
    }
}