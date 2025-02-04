using FluentValidation;

namespace QuanLyNhanSu.Contract.Service.V1.Attendance.Validators;
public class ApproveAttendanceValidator : AbstractValidator<Command.ApproveAttendanceCommand>
{
    public ApproveAttendanceValidator()
    {
        RuleFor(x => x.Id).NotEmpty().WithMessage("Id không được để trống");
        RuleFor(x => x.ApprovalNote).NotEmpty().When(x => x.PendingApproval == Domain.Enumerations.ApproveEmuns.PendingApproval.Tuchoi).WithMessage("ApprovalNote không được để trống");
        RuleFor(x => x.PendingApproval).NotEmpty().WithMessage("PendingApproval không được để trống");
    }
}
