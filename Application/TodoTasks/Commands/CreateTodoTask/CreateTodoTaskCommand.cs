using MediatR;

namespace PolisProReminder.Application.TodoTasks.Commands.CreateTodoTask;

public class CreateTodoTaskCommand : IRequest<Guid>
{
    public string Title { get; set; } = string.Empty;
    public int Order { get; set; } = 1;
}
