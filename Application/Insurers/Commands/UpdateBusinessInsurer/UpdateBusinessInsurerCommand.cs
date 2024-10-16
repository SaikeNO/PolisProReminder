﻿using MediatR;
using PolisProReminder.Application.Insurers.Commands.BaseBusinessInsurer;

namespace PolisProReminder.Application.Insurers.Commands.UpdateBusinessInsurer;

public record UpdateBusinessInsurerCommand(string? Nip, string? Regon, string Name, string? PhoneNumber, string? Email, string? PostalCode, string? City, string? Street)
    : BaseBusinessInsurerCommand(Nip, Regon, Name, PhoneNumber, Email, PostalCode, City, Street), IRequest
{
    public Guid Id { get; set; }
}