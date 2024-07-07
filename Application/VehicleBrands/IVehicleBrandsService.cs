using PolisProReminder.Application.VehicleBrands.Dtos;

namespace PolisProReminder.Application.VehicleBrands
{
    internal interface IVehicleBrandsService
    {
        Task<IEnumerable<VehicleBrandDto>> GetAll();
        Task<VehicleBrandDto?> GetById(Guid id);
    }
}