using FluentValidation;
using TaskList.Application.Queries;

namespace TaskList.Application.Validation.Queries;

public class GetTaskListRelationsQueryValidator : AbstractValidator<GetTaskListRelationsQuery>
{
    public GetTaskListRelationsQueryValidator()
    {
        RuleFor(q => q.TaskListId)
            .NotEmpty().WithMessage("TaskListId is required.");

        RuleFor(q => q.CurrentUserId)
            .NotEmpty().WithMessage("CurrentUserId is required.");
    }
}