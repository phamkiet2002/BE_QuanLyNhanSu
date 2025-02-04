using FluentValidation;

namespace QuanLyNhanSu.Contract.Service.V1.Position.Validators;
public class DeletePositionValidator : AbstractValidator<Command.DeletePositionCommand>
{
    public DeletePositionValidator()
    {
        RuleFor(x => x.Id).NotEmpty().WithMessage("Id không được để trống.");
    }
}
