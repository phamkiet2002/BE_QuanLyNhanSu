using FluentValidation;

namespace QuanLyNhanSu.Contract.Service.V1.Attendance.Validators;
public class OvertimeOutsideWorkHourAttendanceValidator : AbstractValidator<Command.OvertimeOutsideWorkHourAttendanceCommand>
{
    public OvertimeOutsideWorkHourAttendanceValidator()
    {
        RuleFor(x => x.Id).NotEmpty().WithMessage("Id is required");
        RuleFor(x => x.StartTime).NotEmpty().WithMessage("StartTime is required");
        RuleFor(x => x.EndTime).NotEmpty().WithMessage("EndTime is required");
    }
}
