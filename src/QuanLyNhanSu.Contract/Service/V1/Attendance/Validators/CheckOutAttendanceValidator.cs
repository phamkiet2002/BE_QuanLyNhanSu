using FluentValidation;

namespace QuanLyNhanSu.Contract.Service.V1.Attendance.Validators;
public class CheckOutAttendanceValidator : AbstractValidator<Command.CheckOutAttendanceCommand>
{
    public CheckOutAttendanceValidator()
    {
        //RuleFor(x => x.Id)
        //    .NotEmpty().WithMessage("Chưa chọn bản chấm công.");
        //RuleFor(x => x.EmployeeId)
        //    .NotEmpty().WithMessage("Chưa chọn cho nhân viên.");
    }
}
