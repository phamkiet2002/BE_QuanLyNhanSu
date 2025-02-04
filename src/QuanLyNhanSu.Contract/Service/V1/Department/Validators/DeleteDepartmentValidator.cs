using FluentValidation;

namespace QuanLyNhanSu.Contract.Service.V1.Department.Validators;
public class DeleteDepartmentValidator : AbstractValidator<Command.DeleteDepartmentCommand>
{
    public DeleteDepartmentValidator()
    {
        RuleFor(x => x.Id).NotEmpty();
    }
}
