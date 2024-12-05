using FluentValidation;

namespace QuanLyNhanSu.Contract.Service.V1.Employee.Validators;
public class UpdateEmployeeLevelValidator : AbstractValidator<Command.UpdateEmployeeLevelCommand>
{
    public UpdateEmployeeLevelValidator()
    {
        RuleFor(x => x.Id).NotEmpty().WithMessage("EmployeeId is required");
        RuleFor(x => x.LevelId).NotEmpty().WithMessage("LevelId is required");
    }
}
