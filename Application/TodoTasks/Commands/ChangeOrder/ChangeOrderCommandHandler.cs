using MediatR;
using PolisProReminder.Application.Users;
using PolisProReminder.Domain.Repositories;

namespace PolisProReminder.Application.TodoTasks.Commands.ChangeOrder;

internal class ChangeOrderCommandHandler(IUserContext userContext, ITodoTasksRepository todoTasksRepository) : IRequestHandler<ChangeOrderCommand>
{
    private readonly ITodoTasksRepository _todoTasksRepository = todoTasksRepository;
    private readonly IUserContext _userContext = userContext;
    public async Task Handle(ChangeOrderCommand request, CancellationToken cancellationToken)
    {
        var currentUser = _userContext.GetCurrentUser();
        _ = currentUser ?? throw new InvalidOperationException("Current User is not present");

        foreach (var requestTask in request.TodoTasks)
        {
            var task = await _todoTasksRepository.Get(currentUser.Id, requestTask.Id);
            if (task == null) continue;

            task.Order = requestTask.Order;
            task.IsCompleted = requestTask.IsCompleted;
        }

        await _todoTasksRepository.SaveChanges(cancellationToken);
    }
}
