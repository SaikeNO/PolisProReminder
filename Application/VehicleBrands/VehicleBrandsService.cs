using AutoMapper;
using PolisProReminder.Application.VehicleBrands.Dtos;
using PolisProReminder.Domain.Repositories;

namespace PolisProReminder.Application.VehicleBrands;

internal class VehicleBrandsService(IVehicleBrandsRepository vehicleBrandsRepository, IMapper mapper) : IVehicleBrandsService
{
    public async Task<IEnumerable<VehicleBrandDto>> GetAll()
    {
        var brands = await vehicleBrandsRepository.GetAll();

        return mapper.Map<List<VehicleBrandDto>>(brands);
    }

    public async Task<VehicleBrandDto?> GetById(Guid id)
    {
        var brand = await vehicleBrandsRepository.GetById(id);

        return mapper.Map<VehicleBrandDto?>(brand);
    }
}
