
using Backend_Dotnet_Mottu.Domain.Enum;

namespace Backend_Dotnet_Mottu.Application.DTOs.Response
{
    public class MotoResponse
    {
        public long Id { get; set; }
        public string Placa { get; set; }
        public Modelo Modelo { get; set; }
        public string Dono { get; set; }
        public long CondicaoId { get; set; }
        public long PatioId { get; set; }
        public string Localizacao { get; set; }
        public string? Condicao { get; internal set; }
    }
}
