using MediatR;

namespace PolisProReminder.Application.TodoTasks.Commands.UpdateTodoTask;

public class UpdateTodoTaskCommand : IRequest
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public bool IsCompleted { get; set; }
    public int Order { get; set; }
}
