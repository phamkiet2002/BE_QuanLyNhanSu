using FluentValidation;

namespace QuanLyNhanSu.Contract.Service.V1.Employee.Validators;
public class UpdateEmployeePositionValidator : AbstractValidator<Command.UpdateEmployeePositionCommand>
{
    public UpdateEmployeePositionValidator()
    {
        RuleFor(x => x.Id).NotEmpty().WithMessage("Id is required");
        RuleFor(x => x.PositionId).NotEmpty().WithMessage("PositionId is required");
    }
}
