using FluentValidation;

namespace QuanLyNhanSu.Contract.Service.V1.Level.Validators;
public class DeleteLevelValidator : AbstractValidator<Command.DeleteLevelCommand>
{
    public DeleteLevelValidator()
    {
        RuleFor(x => x.Id).NotEmpty();
    }
}
