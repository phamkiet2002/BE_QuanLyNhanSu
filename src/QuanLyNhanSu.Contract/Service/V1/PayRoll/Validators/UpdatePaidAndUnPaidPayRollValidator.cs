using FluentValidation;

namespace QuanLyNhanSu.Contract.Service.V1.PayRoll.Validators;
public class UpdatePaidAndUnPaidPayRollValidator : AbstractValidator<Command.UpdatePaidPayRollCommand>
{
    public UpdatePaidAndUnPaidPayRollValidator()
    {
        RuleFor(x => x.PayRollStatus).NotNull().IsInEnum();
        RuleFor(x => x.ReasonNote).NotEmpty().When(x => x.PayRollStatus == Domain.Enumerations.PayRollEnums.PayRollStatus.UNPAID);
    }
}
