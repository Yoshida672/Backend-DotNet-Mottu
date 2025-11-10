using Backend_Dotnet_Mottu.Application.DTOs.Request;
using FluentValidation;

namespace Backend_Dotnet_Mottu.Application.DTOs.Validators
{
    public class CreateLocalizacaoRequestValidator : AbstractValidator<CreateLocalizacaoUwb>
    {
        public CreateLocalizacaoRequestValidator()
        {
            RuleFor(x => x.CoordenadaX)
                .NotNull().WithMessage("A coordenada X é obrigatória.")
                .InclusiveBetween(-10000, 10000).WithMessage("A coordenada X deve estar dentro de um intervalo válido.");

            RuleFor(x => x.CoordenadaY)
                .NotNull().WithMessage("A coordenada Y é obrigatória.")
                .InclusiveBetween(-10000, 10000).WithMessage("A coordenada Y deve estar dentro de um intervalo válido.");

          
        }
    }
}