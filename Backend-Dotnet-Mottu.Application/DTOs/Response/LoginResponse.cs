namespace Backend_Dotnet_Mottu.Application.DTOs.Response;

public class LoginResponse
{
    public string Id { get; set; }

    public string Token { get; set; }

    public string Message { get; set; }

    public LoginResponse(string message)
    {
        Message = message;
    }

    public LoginResponse()
    {
        
    }
}