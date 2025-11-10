using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend_Dotnet_Mottu.Application.DTOs.Request
{
    public class CreateTagUwb
    {
        public string Codigo { get; set; } = string.Empty;
        public bool Status { get; set; }
        public long? MotoId { get; set; }
        public long? LocalizacaoId { get; set; }
    }
}
