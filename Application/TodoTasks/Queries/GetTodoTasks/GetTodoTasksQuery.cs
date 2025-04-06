using MediatR;
using PolisProReminder.Application.TodoTasks.Dtos;

namespace PolisProReminder.Application.TodoTasks.Queries.GetTodoTasks;

public class GetTodoTasksQuery : IRequest<IEnumerable<TodoTaskDto>> { }
