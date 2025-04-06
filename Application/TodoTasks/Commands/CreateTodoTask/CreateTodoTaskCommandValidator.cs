using FluentValidation;

namespace PolisProReminder.Application.TodoTasks.Commands.CreateTodoTask;

public class CreateTodoTaskCommandValidator : AbstractValidator<CreateTodoTaskCommand>
{
    public CreateTodoTaskCommandValidator()
    {
        RuleFor(dto => dto.Title)
            .NotEmpty()
            .MaximumLength(250);

        RuleFor(dto => dto.Order)
            .NotEmpty()
            .GreaterThanOrEqualTo(0);
    }
}
