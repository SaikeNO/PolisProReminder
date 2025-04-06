using FluentValidation;

namespace PolisProReminder.Application.TodoTasks.Commands.UpdateTodoTask;

public class UpdateTodoTaskCommandValidator : AbstractValidator<UpdateTodoTaskCommand>
{
    public UpdateTodoTaskCommandValidator()
    {
        RuleFor(command => command.Id)
            .NotEmpty().WithMessage("Command ID cannot be empty.");

        RuleFor(command => command.Title)
            .NotEmpty().WithMessage("Command title cannot be empty.")
            .MaximumLength(250).WithMessage("Command title cannot exceed 250 characters.");

        RuleFor(command => command.Order)
            .GreaterThanOrEqualTo(0).WithMessage("Command order must be zero or greater.");
    }
}
