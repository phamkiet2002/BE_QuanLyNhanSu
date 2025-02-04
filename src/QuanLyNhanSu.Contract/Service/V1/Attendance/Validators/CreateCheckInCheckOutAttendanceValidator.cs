using FluentValidation;

namespace QuanLyNhanSu.Contract.Service.V1.Attendance.Validators;
public class CreateCheckInCheckOutAttendanceValidator : AbstractValidator<Command.CreateCheckInCheckOutAttendanceCommand>
{
    public CreateCheckInCheckOutAttendanceValidator()
    {
        RuleFor(x => x.CheckIn).NotEmpty().WithMessage("CheckIn is required");
        RuleFor(x => x.CheckOut).NotEmpty().WithMessage("CheckOut is required");
        RuleFor(x => x.EmployeeId).NotEmpty().WithMessage("EmployeeId is required");
    }
}
