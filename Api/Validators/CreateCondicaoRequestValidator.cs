﻿using CP2_BackEndMottu_DotNet.Application.DTOs.Request;
using FluentValidation;

namespace CP2_BackEndMottu_DotNet.Api.Validators
{
    public class CreateCondicaoRequestValidator : AbstractValidator<CreateCondicaoRequest>
    {
        public CreateCondicaoRequestValidator()
        {
            RuleFor(x => x.Nome)
                .NotEmpty().WithMessage("O nome da condição é obrigatório.")
                .MaximumLength(100).WithMessage("O nome da condição deve ter no máximo 100 caracteres.");

            RuleFor(x => x.Cor)
                .NotEmpty().WithMessage("A cor da condição é obrigatória.")
                .MaximumLength(50).WithMessage("A cor deve ter no máximo 50 caracteres.");
        }
    }
}
