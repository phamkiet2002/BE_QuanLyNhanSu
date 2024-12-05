using FluentValidation;

namespace QuanLyNhanSu.Contract.Service.V1.LeaveDate.Validators;
public class DeleteLeaveDateValidator : AbstractValidator<Command.DeleteLeaveDateCommand>
{
    public DeleteLeaveDateValidator()
    {
        RuleFor(x => x.Id).NotEmpty();
    }
}
