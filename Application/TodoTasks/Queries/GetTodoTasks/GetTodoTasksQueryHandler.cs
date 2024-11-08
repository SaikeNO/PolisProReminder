using AutoMapper;
using MediatR;
using PolisProReminder.Application.TodoTasks.Dtos;
using PolisProReminder.Application.Users;
using PolisProReminder.Domain.Repositories;

namespace PolisProReminder.Application.TodoTasks.Queries.GetTodoTasks;

internal class GetTodoTasksQueryHandler(IUserContext userContext, ITodoTasksRepository todoTasksRepository, IMapper mapper) : IRequestHandler<GetTodoTasksQuery, IEnumerable<TodoTaskDto>>
{
    private readonly ITodoTasksRepository _todoTasksRepository = todoTasksRepository;
    private readonly IUserContext _userContext = userContext;
    private readonly IMapper _mapper = mapper;
    public async Task<IEnumerable<TodoTaskDto>> Handle(GetTodoTasksQuery request, CancellationToken cancellationToken)
    {
        var currentUser = _userContext.GetCurrentUser();
        _ = currentUser ?? throw new InvalidOperationException("Current User is not present");

        var tasks = await _todoTasksRepository.GetAll(currentUser.Id);

        return _mapper.Map<IEnumerable<TodoTaskDto>>(tasks);
    }
}
