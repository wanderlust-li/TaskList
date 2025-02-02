using FluentValidation;
using TaskList.Application.Queries;

namespace TaskList.Application.Validation.Queries;

public class GetTaskListQueryValidator : AbstractValidator<GetTaskListQuery>
{
    public GetTaskListQueryValidator()
    {
        RuleFor(q => q.Id)
            .NotEmpty().WithMessage("Id is required.");
    }
}