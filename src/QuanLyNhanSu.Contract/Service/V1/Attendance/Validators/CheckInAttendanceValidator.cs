using FluentValidation;

namespace QuanLyNhanSu.Contract.Service.V1.Attendance.Validators;
public class CheckInAttendanceValidator : AbstractValidator<Command.CheckInAttendanceCommand>
{
    public CheckInAttendanceValidator()
    {
        //RuleFor(x => x.EmployeeId)
        //    .NotEmpty().WithMessage("Chưa chọn cho nhân viên.");
    }
}
