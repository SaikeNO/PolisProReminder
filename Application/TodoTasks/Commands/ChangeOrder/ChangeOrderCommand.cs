using MediatR;
using PolisProReminder.Application.TodoTasks.Dtos;

namespace PolisProReminder.Application.TodoTasks.Commands.ChangeOrder;

public class ChangeOrderCommand : IRequest
{
    public IEnumerable<TodoTaskDto> TodoTasks { get; set; }
}
