using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PolisProReminder.Application.TodoTasks.Commands.ChangeOrder;
using PolisProReminder.Application.TodoTasks.Commands.CreateTodoTask;
using PolisProReminder.Application.TodoTasks.Commands.DeleteTodoTask;
using PolisProReminder.Application.TodoTasks.Commands.UpdateTodoTask;
using PolisProReminder.Application.TodoTasks.Dtos;
using PolisProReminder.Application.TodoTasks.Queries.GetTodoTasks;

namespace PolisProReminder.API.Controllers;

[Authorize]
[Route("api/[controller]")]
public class TodoTaskController(IMediator mediator) : ControllerBase
{
    private readonly IMediator _mediator = mediator;

    [HttpPost]
    public async Task<ActionResult<Guid>> Create([FromBody] CreateTodoTaskCommand command, CancellationToken cancellationToken)
    {
        var id = await _mediator.Send(command, cancellationToken);
        return Ok(id);
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<TodoTaskDto>>> GetAll(CancellationToken cancellationToken)
    {
        var tasks = await _mediator.Send(new GetTodoTasksQuery(), cancellationToken);

        return Ok(tasks);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(Guid id, CancellationToken cancellationToken)
    {
        await _mediator.Send(new DeleteTodoTaskCommand(id), cancellationToken);

        return NoContent();
    }

    [HttpPut]
    public async Task<ActionResult> Update([FromBody] UpdateTodoTaskCommand command, CancellationToken cancellationToken)
    {
        await _mediator.Send(command, cancellationToken);
        return NoContent();
    }

    [HttpPost("Order")]
    public async Task<ActionResult> UpdateOrder([FromBody] ChangeOrderCommand command, CancellationToken cancellationToken)
    {
        await _mediator.Send(command, cancellationToken);
        return NoContent();
    }
}
