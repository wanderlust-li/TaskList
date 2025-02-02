using FluentValidation;
using TaskList.Application.Commands;

namespace TaskList.Application.Validation.Commands;

public class RemoveTaskListRelationCommandValidator : AbstractValidator<RemoveTaskListRelationCommand>
{
    public RemoveTaskListRelationCommandValidator()
    {
        RuleFor(cmd => cmd.TaskListId)
            .NotEmpty().WithMessage("TaskListId is required.");

        RuleFor(cmd => cmd.CurrentUserId)
            .NotEmpty().WithMessage("CurrentUserId is required.");

        RuleFor(cmd => cmd.SharedUserId)
            .NotEmpty().WithMessage("SharedUserId is required.");
    }
}