using FluentValidation;

namespace QuanLyNhanSu.Contract.Service.V1.Employee.Validators;
public class UpdateEmployeeSalaryValidator : AbstractValidator<Command.UpdateEmployeeSalaryCommand>
{
    public UpdateEmployeeSalaryValidator()
    {
        RuleFor(x => x.Id).NotEmpty().WithMessage("Id is required");
        RuleFor(x => x.Salarys).NotEmpty().WithMessage("Salarys is required").GreaterThan(0).WithMessage("Mức lương phải lớn hơn 0.");
    }
}
