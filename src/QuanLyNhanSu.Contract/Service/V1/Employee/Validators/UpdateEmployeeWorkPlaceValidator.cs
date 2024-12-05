using FluentValidation;

namespace QuanLyNhanSu.Contract.Service.V1.Employee.Validators;
public class UpdateEmployeeWorkPlaceValidator : AbstractValidator<Command.UpdateEmployeeWorkPlaceCommand>
{
    public UpdateEmployeeWorkPlaceValidator()
    {
        RuleFor(x => x.Id).NotEmpty().WithMessage("EmployeeId is required");
        RuleFor(x => x.WorkPlaceId).NotEmpty().WithMessage("WorkPlaceId is required");
    }
}
