using FluentValidation;

namespace QuanLyNhanSu.Contract.Service.V1.AllowanceAndPenalties.Validators;
public class CreatePenaltiesValidator : AbstractValidator<Command.CreatePenaltiesCommand>
{
    public CreatePenaltiesValidator()
    {
        RuleFor(c => c.TypeOfPenalty)
            .NotNull()
            .WithMessage("Không được để trống.");

        RuleFor(command => command.Money)
            .GreaterThan(0)
            .WithMessage("Số tiền phả lớn hơn 0.")
            .NotEmpty().WithMessage("không được để trống");

        RuleFor(command => command.EffectiveDate)
            .NotEmpty()
            .WithMessage("Không được để trống.");

        RuleFor(command => command.Note)
            .MaximumLength(500)
            .WithMessage("Không được để trống.");

        RuleFor(command => command.IsAllWorkPlace)
            .NotNull()
            .WithMessage("Không được để trống.");

        RuleFor(command => command.WorkPlaceId)
            .NotNull()
            .When(command => command.IsAllWorkPlace == false)
            .WithMessage("Không được để trống.");
    }
}
