using Backend_Dotnet_Mottu.Domain.Enum;

namespace Backend_Dotnet_Mottu.Application.DTOs.Request
{
    public class CreateMoto
    {
        public string Placa { get; set; }
        public Modelo Modelo { get; set; }
        public string Dono { get; set; }
        public long CondicaoId { get; set; }
        public long PatioId { get; set; }
    }
}
