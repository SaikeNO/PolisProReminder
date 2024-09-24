using MediatR;
using PolisProReminder.Application.Insurers.Commands.BaseInsurer;

namespace PolisProReminder.Application.Insurers.Commands.CreateInsurer;

public record CreateInsurerCommand(string Pesel, string FirstName, string? LastName, string? PhoneNumber, string? Email, string? PostalCode, string? City, string? Street)
    : BaseInsurerCommand(Pesel, FirstName, LastName, PhoneNumber, Email, PostalCode, City, Street), IRequest<Guid> { }
