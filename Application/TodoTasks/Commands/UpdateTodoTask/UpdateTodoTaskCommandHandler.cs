using MediatR;
using PolisProReminder.Application.Users;
using PolisProReminder.Domain.Exceptions;
using PolisProReminder.Domain.Repositories;

namespace PolisProReminder.Application.TodoTasks.Commands.UpdateTodoTask;

internal class UpdateTodoTaskCommandHandler(IUserContext userContext, ITodoTasksRepository todoTasksRepository) : IRequestHandler<UpdateTodoTaskCommand>
{
    private readonly ITodoTasksRepository _todoTasksRepository = todoTasksRepository;
    private readonly IUserContext _userContext = userContext;
    public async Task Handle(UpdateTodoTaskCommand request, CancellationToken cancellationToken)
    {
        var currentUser = _userContext.GetCurrentUser();
        _ = currentUser ?? throw new InvalidOperationException("Current User is not present");

        var task = await _todoTasksRepository.Get(currentUser.Id, request.Id)
            ?? throw new NotFoundException($"TodoTask with id {request.Id} not found");

        task.Order = request.Order;
        task.IsCompleted = request.IsCompleted;
        task.Title = request.Title;

        await _todoTasksRepository.SaveChanges(cancellationToken);
    }
}
