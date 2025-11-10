

namespace Backend_Dotnet_Mottu.Application.DTOs.Request
{
    public class UpdateTagUwbRequest
    {
        public string Codigo { get; set; } = string.Empty;
        public bool Status { get; set; }
        public long? MotoId { get; set; }
        public long? LocalizacaoId { get; set; }
    }
}
