using System.ComponentModel.DataAnnotations;

namespace Backend_Dotnet_Mottu.Application.DTOs.Request;

public class LoginRequest
{
    [Required(ErrorMessage = "O campo 'Email' é obrigatório.")]
    [EmailAddress(ErrorMessage = "O 'Email' não está em um formato válido.")]
    public string Email { get; set; }

    [Required(ErrorMessage = "O campo 'Password' é obrigatório.")]
    [MinLength(6, ErrorMessage = "A senha deve conter pelo menos 6 caracteres.")]
    public string Password { get; set; }
}