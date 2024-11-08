using MediatR;

namespace PolisProReminder.Application.TodoTasks.Commands.DeleteTodoTask;

public record DeleteTodoTaskCommand(Guid Id) : IRequest;
