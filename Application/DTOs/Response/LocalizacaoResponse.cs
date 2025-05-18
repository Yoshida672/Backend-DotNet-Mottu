namespace CP2_BackEndMottu_DotNet.Application.DTOs.Response
{
    public class LocalizacaoResponse
    {
        public Guid Id { get; set; }
        public double CoordenadaX { get; set; }
        public double CoordenadaY { get; set; }
        public DateTime DataHora { get; set; }
        public Guid MotoId { get; set; }
    }
}
