﻿using MediatR;

namespace PolisProReminder.Application.Vehicles.Commands.CreateVehicle;

public class CreateVehicleCommand : IRequest<Guid>
{
    public string Name { get; set; } = null!;
    public string RegistrationNumber { get; set; } = null!;
    public DateOnly? FirstRegistrationDate { get; set; }
    public DateOnly? ProductionYear { get; set; }
    public string? VIN { get; set; }
    public decimal? KW { get; set; }
    public decimal? KM { get; set; }
    public decimal? Capacity { get; set; }
    public uint? Mileage { get; set; }

    public Guid InsurerId { get; set; }
    public Guid VehicleBrandId { get; set; }
}
