using FluentValidation;

namespace QuanLyNhanSu.Contract.Service.V1.WorkShedule.Validators;
public class DeleteWorkSheduleValidator : AbstractValidator<Command.DeleteWorkSheduleCommand>
{
    public DeleteWorkSheduleValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Id không được để trống");
    }
}
