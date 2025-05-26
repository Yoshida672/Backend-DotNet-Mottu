namespace CP2_BackEndMottu_DotNet.Application.DTOs.Request
{
    public class UpdateLocalizacaoRequest
    {
        public double CoordenadaX { get; set; }
        public double CoordenadaY { get; set; }
        public Guid MotoId { get; set; }
    }
}
