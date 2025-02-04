﻿using FluentValidation;

namespace QuanLyNhanSu.Contract.Service.V1.Position.Validators;
public class UpdatePositionValidator : AbstractValidator<Command.UpdatePositionCommand>
{
    public UpdatePositionValidator()
    {
        RuleFor(x => x.Name).NotEmpty().WithMessage("Tên vị trí không được để trống.");
        RuleFor(x => x.Description)
            .NotEmpty().WithMessage("Mô tả không được để trống.")
            .MaximumLength(100).WithMessage("Không được dài hơn 100 ký tự.");
    }
}
