using FluentValidation;

namespace QuanLyNhanSu.Contract.Service.V1.Employee.Validators;
public class LeaveWorkEmployeeValidator : AbstractValidator<Command.LeaveWorkEmployeeCommand>
{
    public LeaveWorkEmployeeValidator()
    {
        //RuleFor(x => x.Id)
        //   .Must(id => id != Guid.Empty)
        //   .WithMessage("Id không thể là Guid.Empty.");
    }
}
