using MediatR;
using PolisProReminder.Application.Users.Dtos;

namespace PolisProReminder.Application.Users.Queries.GetAssistants;

public class GetAssistantsQuery : IRequest<IEnumerable<AssistantDto>>
{
}
