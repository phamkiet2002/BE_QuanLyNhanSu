using FluentValidation;

namespace QuanLyNhanSu.Contract.Service.V1.Employee.Validators;
public class GetByIdEmployeeValidator : AbstractValidator<Query.GetEmployeeByIdQuery>
{
    public GetByIdEmployeeValidator()
    {
        //RuleFor(x => x.Id).NotEmpty().WithMessage("Id is required");
    }
}
