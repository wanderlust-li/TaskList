using FluentValidation;
using TaskList.Application.Commands;

namespace TaskList.Application.Validation.Commands;

public class DeleteTaskListCommandValidator : AbstractValidator<DeleteTaskListCommand>
{
    public DeleteTaskListCommandValidator()
    {
        RuleFor(cmd => cmd.TaskListId)
            .NotEmpty().WithMessage("TaskListId is required.");

        RuleFor(cmd => cmd.CurrentUserId)
            .NotEmpty().WithMessage("CurrentUserId is required.");
    }
}
