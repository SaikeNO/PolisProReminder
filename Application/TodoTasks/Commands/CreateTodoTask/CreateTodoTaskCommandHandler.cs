using MediatR;
using PolisProReminder.Application.Users;
using PolisProReminder.Domain.Entities;
using PolisProReminder.Domain.Repositories;

namespace PolisProReminder.Application.TodoTasks.Commands.CreateTodoTask;

public class CreateTodoTaskCommandHandler(IUserContext userContext, ITodoTasksRepository todoTasksRepository) : IRequestHandler<CreateTodoTaskCommand, Guid>
{
    private readonly ITodoTasksRepository _todoTasksRepository = todoTasksRepository;
    private readonly IUserContext _userContext = userContext;
    public async Task<Guid> Handle(CreateTodoTaskCommand request, CancellationToken cancellationToken)
    {
        var currentUser = _userContext.GetCurrentUser();
        _ = currentUser ?? throw new InvalidOperationException("Current User is not present");

        var task = new TodoTask()
        {
            Title = request.Title,
            CreatedByUserId = currentUser.Id,
        };

        var id = await _todoTasksRepository.Create(task);
        await _todoTasksRepository.SaveChanges();

        return id;
    }
}
