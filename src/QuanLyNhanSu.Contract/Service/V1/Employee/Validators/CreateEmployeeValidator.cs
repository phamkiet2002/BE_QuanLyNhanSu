using FluentValidation;

namespace QuanLyNhanSu.Contract.Service.V1.Employee.Validators;
public class CreateEmployeeValidator : AbstractValidator<Command.CreateEmployeeCommand>
{
    public CreateEmployeeValidator()
    {

    }
}
