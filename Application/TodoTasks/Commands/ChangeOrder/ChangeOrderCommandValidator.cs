using FluentValidation;
using PolisProReminder.Application.TodoTasks.Dtos;

namespace PolisProReminder.Application.TodoTasks.Commands.ChangeOrder;

internal class ChangeOrderCommandValidator : AbstractValidator<ChangeOrderCommand>
{
    public ChangeOrderCommandValidator()
    {
        RuleFor(command => command.TodoTasks)
            .NotNull().WithMessage("TodoTasks cannot be null.")
            .NotEmpty().WithMessage("TodoTasks cannot be empty.");

        RuleForEach(command => command.TodoTasks).ChildRules(todoTask =>
        {
            todoTask.RuleFor(task => task.Id)
                .NotEmpty().WithMessage("Task ID cannot be empty.");

            todoTask.RuleFor(task => task.Title)
                .NotEmpty().WithMessage("Task title cannot be empty.")
                .MaximumLength(250).WithMessage("Task title cannot exceed 100 characters.");

            todoTask.RuleFor(task => task.Order)
                .GreaterThanOrEqualTo(0).WithMessage("Task order must be zero or greater.");
        });

        RuleFor(command => command.TodoTasks)
            .Must(HaveUniqueOrder).WithMessage("Each task must have a unique order.")
            .Must(HaveUniqueIds).WithMessage("Each task must have a unique ID.");
    }

    private bool HaveUniqueOrder(IEnumerable<TodoTaskDto> todoTasks)
    {
        var orderList = todoTasks.Select(task => task.Order);
        return orderList.Distinct().Count() == orderList.Count();
    }

    private bool HaveUniqueIds(IEnumerable<TodoTaskDto> todoTasks)
    {
        var idList = todoTasks.Select(task => task.Id);
        return idList.Distinct().Count() == idList.Count();
    }
}
