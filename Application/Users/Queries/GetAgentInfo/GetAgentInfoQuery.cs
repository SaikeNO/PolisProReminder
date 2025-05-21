using MediatR;
using PolisProReminder.Application.Users.Dtos;

namespace PolisProReminder.Application.Users.Queries.GetAgentInfo;

public record GetAgentInfoQuery : IRequest<BaseUserDto>
{
}
