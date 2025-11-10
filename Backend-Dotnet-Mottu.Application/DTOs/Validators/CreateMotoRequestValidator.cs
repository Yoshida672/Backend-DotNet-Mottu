using Backend_Dotnet_Mottu.Application.DTOs.Request;
using FluentValidation;

namespace Backend_Dotnet_Mottu.Application.DTOs.Validators
{
    public class CreateMotoRequestValidator : AbstractValidator<CreateMoto>
    {
        public CreateMotoRequestValidator()
        {
            RuleFor(x => x.Placa)
                .NotEmpty().WithMessage("Placa obrigatória")
                .MaximumLength(10).WithMessage("Placa deve ter no máximo 10 caracteres");

            RuleFor(x => x.Modelo)
                .IsInEnum().WithMessage("Modelo inválido");


            RuleFor(x => x.Dono)
                .NotEmpty().WithMessage("Dono obrigatório")
                .MaximumLength(100).WithMessage("Dono deve ter no máximo 100 caracteres");

            RuleFor(x => x.CondicaoId)
                .NotEmpty().WithMessage("CondicaoId obrigatório");

            RuleFor(x => x.PatioId)
                .NotEmpty().WithMessage("PatioId obrigatório");
        }
    }
}
