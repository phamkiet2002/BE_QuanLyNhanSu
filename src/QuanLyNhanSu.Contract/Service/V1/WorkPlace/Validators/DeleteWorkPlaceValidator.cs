using FluentValidation;

namespace QuanLyNhanSu.Contract.Service.V1.WorkPlace.Validators;
public class DeleteWorkPlaceValidator : AbstractValidator<Command.DeleteWorkPlaceCommand>
{
    public DeleteWorkPlaceValidator()
    {
        RuleFor(x => x.Id).NotEmpty().WithMessage("không được để trống.");
    }
}
