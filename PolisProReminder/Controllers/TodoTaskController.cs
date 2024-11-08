using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PolisProReminder.Application.TodoTasks.Commands.ChangeOrder;
using PolisProReminder.Application.TodoTasks.Commands.CreateTodoTask;
using PolisProReminder.Application.TodoTasks.Commands.DeleteTodoTask;
using PolisProReminder.Application.TodoTasks.Dtos;
using PolisProReminder.Application.TodoTasks.Queries.GetTodoTasks;

namespace PolisProReminder.API.Controllers;

[Authorize]
[Route("api/[controller]")]
public class TodoTaskController(IMediator mediator) : ControllerBase
{
    private readonly IMediator _mediator = mediator;

    [HttpPost]
    public async Task<ActionResult<Guid>> Create([FromBody] CreateTodoTaskCommand command)
    {
        var id = await _mediator.Send(command);
        return Ok(id);
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<TodoTaskDto>>> GetAll()
    {
        var tasks = await _mediator.Send(new GetTodoTasksQuery());

        return Ok(tasks);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete([FromRoute] Guid id)
    {
        await _mediator.Send(new DeleteTodoTaskCommand(id));

        return NoContent();
    }

    [HttpPost("Order")]
    public async Task<ActionResult> ChangeOrder([FromBody] ChangeOrderCommand command)
    {
        await _mediator.Send(command);
        return NoContent();
    }
}
