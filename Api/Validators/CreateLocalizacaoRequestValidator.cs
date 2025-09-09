using CP2_BackEndMottu_DotNet.Application.DTOs.Request;
using FluentValidation;

namespace CP2_BackEndMottu_DotNet.Api.Validators
{
    public class CreateLocalizacaoRequestValidator : AbstractValidator<CreateLocalizacaoUwb>
    {
        public CreateLocalizacaoRequestValidator()
        {
            RuleFor(x => x.CoordenadaX)
               .NotNull().WithMessage("Coordenada X é obrigatória.");

            RuleFor(x => x.CoordenadaY)
                .NotNull().WithMessage("Coordenada Y é obrigatória.");
            RuleFor(x => x.MotoId)
                .NotEmpty().WithMessage("O ID da moto é obrigatório.");
        }
    }
}