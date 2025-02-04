using FluentValidation;

namespace QuanLyNhanSu.Contract.Service.V1.Employee.Validators;
public class GetByMaNVValidator : AbstractValidator<Query.GetEmployeeByMaNVQuery>
{
    public GetByMaNVValidator()
    {
        RuleFor(x => x.MaNV).NotEmpty().WithMessage("MaNV is required");
    }
}
