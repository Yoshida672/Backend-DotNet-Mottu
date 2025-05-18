using CP2_BackEndMottu_DotNet.Application.DTOs.Request;
using FluentValidation;

namespace CP2_BackEndMottu_DotNet.Application.Validators
{
    public class CreateLocalizacaoRequestValidator : AbstractValidator<CreateLocalizacaoUwb>
    {
        public CreateLocalizacaoRequestValidator()
        {
            RuleFor(x => x.CoordenadaX)
                .GreaterThanOrEqualTo(0).WithMessage("Coordenada X deve ser positiva.");

            RuleFor(x => x.CoordenadaY)
                .GreaterThanOrEqualTo(0).WithMessage("Coordenada Y deve ser positiva.");

            RuleFor(x => x.MotoId)
                .NotEmpty().WithMessage("O ID da moto é obrigatório.");
        }
    }
}