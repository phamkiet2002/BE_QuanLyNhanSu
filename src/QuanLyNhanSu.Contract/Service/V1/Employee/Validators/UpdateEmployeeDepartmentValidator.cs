using FluentValidation;

namespace QuanLyNhanSu.Contract.Service.V1.Employee.Validators;
public class UpdateEmployeeDepartmentValidator : AbstractValidator<Command.UpdateEmployeeDepartmentCommand>
{
    public UpdateEmployeeDepartmentValidator()
    {
        RuleFor(x => x.Id).NotEmpty().WithMessage("không được để trống.");
        RuleFor(x => x.DepartmentId).NotEmpty().WithMessage("không được để trống.");
    }
}
