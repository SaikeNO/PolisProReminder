using MediatR;
using PolisProReminder.Application.Users;
using PolisProReminder.Domain.Exceptions;
using PolisProReminder.Domain.Repositories;

namespace PolisProReminder.Application.TodoTasks.Commands.DeleteTodoTask;

internal class DeleteTodoTaskCommandHandler(IUserContext userContext, ITodoTasksRepository todoTasksRepository) : IRequestHandler<DeleteTodoTaskCommand>
{
    private readonly ITodoTasksRepository _todoTasksRepository = todoTasksRepository;
    private readonly IUserContext _userContext = userContext;
    public async Task Handle(DeleteTodoTaskCommand request, CancellationToken cancellationToken)
    {
        var currentUser = _userContext.GetCurrentUser();
        _ = currentUser ?? throw new InvalidOperationException("Current User is not present");

        var task = await _todoTasksRepository.Get(currentUser.Id, request.Id);
        _ = task ?? throw new NotFoundException("Zadanie o podanym ID nie istnieje");

        _todoTasksRepository.Delete(task);
        await _todoTasksRepository.SaveChanges();
    }
}
