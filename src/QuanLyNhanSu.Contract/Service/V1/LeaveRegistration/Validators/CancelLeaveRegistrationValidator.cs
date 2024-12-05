using FluentValidation;

namespace QuanLyNhanSu.Contract.Service.V1.LeaveRegistration.Validators;
public class CancelLeaveRegistrationValidator : AbstractValidator<Command.CancelLeaveRegistrationCommand>
{
    public CancelLeaveRegistrationValidator()
    {
        RuleFor(x => x.Id).NotEmpty().WithMessage("Id không được để trống");
    }
}
