using FluentValidation;
using TaskList.Application.Queries;

namespace TaskList.Application.Validation.Queries;

public class GetTaskListsQueryValidator : AbstractValidator<GetTaskListsQuery>
{
    public GetTaskListsQueryValidator()
    {
        RuleFor(q => q.CurrentUserId)
            .NotEmpty().WithMessage("CurrentUserId is required.");
        
        RuleFor(q => q.PageNumber)
            .GreaterThanOrEqualTo(1).WithMessage("PageNumber must be >= 1.");
        
        RuleFor(q => q.PageSize)
            .GreaterThanOrEqualTo(1).WithMessage("PageSize must be >= 1.");
    }
}