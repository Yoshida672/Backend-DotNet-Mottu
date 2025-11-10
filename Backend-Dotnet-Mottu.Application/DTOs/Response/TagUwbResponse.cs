

namespace Backend_Dotnet_Mottu.Application.DTOs.Response
{
    public class TagUwbResponse
    {
        public long Id { get; set; }
        public string Codigo { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public long? MotoId { get; set; }
        public LocalizacaoResponse? Localizacao { get; set; }
    }
}
