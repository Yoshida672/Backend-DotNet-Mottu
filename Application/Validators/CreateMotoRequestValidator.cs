using CP2_BackEndMottu_DotNet.Application.DTOs.Request;
using FluentValidation;

namespace CP2_BackEndMottu_DotNet.Application.Validators
{
    public class CreateMotoRequestValidator : AbstractValidator<CreateMoto>
    {
        public CreateMotoRequestValidator()
        {
            RuleFor(x => x.Placa)
                .NotEmpty().WithMessage("Placa é obrigatória.")
                .Length(7).WithMessage("Placa deve ter 7 caracteres.");

            RuleFor(x => x.Modelo)
                .NotEmpty().WithMessage("Modelo é obrigatório.")
                .MaximumLength(100).WithMessage("Modelo deve ter no máximo 100 caracteres.");

            RuleFor(x => x.Status)
                .NotEmpty().WithMessage("Status é obrigatório.")
                .Must(s => s == "Ativo" || s == "Inativo")
                .WithMessage("Status deve ser 'Ativo' ou 'Inativo'.");
        }
    }
}
