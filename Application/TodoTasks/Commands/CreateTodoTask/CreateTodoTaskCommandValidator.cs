using FluentValidation;

namespace PolisProReminder.Application.TodoTasks.Commands.CreateTodoTask;

internal class CreateTodoTaskCommandValidator : AbstractValidator<CreateTodoTaskCommand>
{
    public CreateTodoTaskCommandValidator()
    {
        RuleFor(dto => dto.Title)
            .NotEmpty()
            .MaximumLength(250);
    }
}
