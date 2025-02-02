using FluentValidation;
using TaskList.Application.Commands;

namespace TaskList.Application.Validation.Commands;

public class CreateTaskListCommandValidator : AbstractValidator<CreateTaskListCommand>
{
    public CreateTaskListCommandValidator()
    {
        RuleFor(cmd => cmd.Name)
            .NotEmpty().WithMessage("Name cannot be empty.")
            .Length(1, 255).WithMessage("Name length must be between 1 and 255.");
            
        RuleFor(cmd => cmd.OwnerId)
            .NotEmpty().WithMessage("OwnerId is required.");
    }
}