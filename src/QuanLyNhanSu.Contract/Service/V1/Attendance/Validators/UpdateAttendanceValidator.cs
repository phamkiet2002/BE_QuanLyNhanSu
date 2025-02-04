using FluentValidation;

namespace QuanLyNhanSu.Contract.Service.V1.Attendance.Validators;
public class UpdateAttendanceValidator : AbstractValidator<Command.UpdateAttendanceCommand>
{
    public UpdateAttendanceValidator()
    {
        RuleFor(x => x.Id).NotEmpty().WithMessage("Id is required");
        RuleFor(x => x.CheckIn).NotEmpty().WithMessage("CheckIn is required");
        RuleFor(x => x.CheckOut).NotEmpty().WithMessage("CheckOut is required");
        RuleFor(x => x.ReasonNote).NotEmpty().WithMessage("ReasonNote is required");
    }
}
