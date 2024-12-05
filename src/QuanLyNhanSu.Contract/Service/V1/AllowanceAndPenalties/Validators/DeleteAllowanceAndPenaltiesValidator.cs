using FluentValidation;

namespace QuanLyNhanSu.Contract.Service.V1.AllowanceAndPenalties.Validators;
public class DeleteAllowanceAndPenaltiesValidator : AbstractValidator<Command.DeleteAllowanceAndPenaltiesCommand>
{
    public DeleteAllowanceAndPenaltiesValidator()
    {
        RuleFor(x => x.Id).NotEmpty().WithMessage("Id is required"); 
    }
}
