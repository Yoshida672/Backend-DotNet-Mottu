using System.ComponentModel.DataAnnotations;
using Backend_Dotnet_Mottu.Domain.Entities;

namespace Backend_Dotnet_Mottu.Application.DTOs.Request;

public class CreateUserRequest
{
    [Required(ErrorMessage = "O nome é obrigatório.")]
    public string Name { get; set; }

    [Required(ErrorMessage = "O e-mail é obrigatório.")]
    [EmailAddress(ErrorMessage = "O e-mail informado é inválido.")]
    public string Email { get; set; }

    [Required(ErrorMessage = "A senha é obrigatória.")]
    [MinLength(6, ErrorMessage = "A senha deve ter no mínimo 6 caracteres.")]
    public string Password { get; set; }
    
    [Required(ErrorMessage = "O atributo admin é obrigatório.")]
    public bool Admin { get; set; }
    
    public User ToDomain()
    {
        return new User(Name, Email, Password, Admin);
    }
}