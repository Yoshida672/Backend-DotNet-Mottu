namespace CP2_BackEndMottu_DotNet.Application.DTOs.Request
{
    public class UpdateMotoRequest
    {
        public string Placa { get; set; }
        public string Modelo { get; set; }
        public string Status { get; set; }
        public Guid CondicaoId { get; set; }

    }
}
