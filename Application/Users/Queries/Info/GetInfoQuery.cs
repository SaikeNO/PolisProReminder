using MediatR;
using PolisProReminder.Application.Users.Dtos;

namespace PolisProReminder.Application.Users.Queries.Info;

public class GetInfoQuery : IRequest<UserDto>
{
}
