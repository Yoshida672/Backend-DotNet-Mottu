namespace CP2_BackEndMottu_DotNet.Application.DTOs.Response
{
    public class MotoResponse
    {
        public Guid Id { get; set; }
        public string Placa { get; set; }
        public string Modelo { get; set; }
        public string Status { get; set; }
        public Guid CondicaoId { get; set; }
    }
}
