using FluentValidation;

namespace QuanLyNhanSu.Contract.Service.V1.WorkPlace.Validators;
public class GetWorkPlaceByIdValidator : AbstractValidator<Query.GetWorkPlaceByIdQuery>
{
    public GetWorkPlaceByIdValidator()
    {
        RuleFor(x => x.Id).NotEmpty();
    }
}
