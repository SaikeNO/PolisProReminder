using MediatR;
using PolisProReminder.Application.Insurers.Commands.BaseIndividualInsurer;

namespace PolisProReminder.Application.Insurers.Commands.CreateIndividualInsurer;

public record CreateIndividualInsurerCommand(string Pesel, string FirstName, string? LastName, string? PhoneNumber, string? Email, string? PostalCode, string? City, string? Street)
    : BaseIndividualInsurerCommand(Pesel, FirstName, LastName, PhoneNumber, Email, PostalCode, City, Street), IRequest<Guid>
{ }
