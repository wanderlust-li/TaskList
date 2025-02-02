using FluentValidation;
using TaskList.Application.Commands;

namespace TaskList.Application.Validation.Commands;

public class UpdateTaskListCommandValidator : AbstractValidator<UpdateTaskListCommand>
{
    public UpdateTaskListCommandValidator()
    {
        RuleFor(cmd => cmd.TaskListId)
            .NotEmpty().WithMessage("TaskListId is required.");

        RuleFor(cmd => cmd.CurrentUserId)
            .NotEmpty().WithMessage("CurrentUserId is required.");

        RuleFor(cmd => cmd.NewName)
            .NotEmpty().WithMessage("NewName is required.")
            .Length(1, 255).WithMessage("NewName length must be between 1 and 255.");
    }
}