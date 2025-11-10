
using Backend_Dotnet_Mottu.Application.DTOs.Request;
using FluentValidation;

namespace Backend_Dotnet_Mottu.Application.DTOs.Validators
{
    public class CreateTagUwbValidator : AbstractValidator<CreateTagUwb>
    {
        public CreateTagUwbValidator()
        {
            RuleFor(x => x.Codigo)
                .NotEmpty().WithMessage("O código da Tag UWB é obrigatório.")
                .MaximumLength(10).WithMessage("O código deve ter no máximo 10 caracteres.");

            RuleFor(x => x.Status)
                .NotNull().WithMessage("O status da Tag é obrigatório.");
        }
    }
}
