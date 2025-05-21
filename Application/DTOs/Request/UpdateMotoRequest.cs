namespace CP2_BackEndMottu_DotNet.Application.DTOs.Request
{
    public class UpdateMotoRequest
    {
        public Guid Id { get; set; }
        public string Placa { get; set; }
        public string Modelo { get; set; }
        public string Status { get; set; }
    }
}
