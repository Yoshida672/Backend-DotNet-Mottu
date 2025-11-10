using Backend_Dotnet_Mottu.Domain.ValueObjects;

namespace Backend_Dotnet_Mottu.Domain.Entities
{
    public class TagUwb
    {
        public long Id { get; private set; }
        public string Codigo { get; private set; }
        public bool Status { get; private set; }

        public long? MotoId { get; private set; }
        public virtual Moto? Moto { get; private set; }
        public LocalizacaoUWB? Localizacao { get; private set; }

        public TagUwb() { }

        public TagUwb(string codigo, bool status, long? motoId = null)
        {
            AtualizarDados(codigo, status, motoId);
        }

        public void AtualizarDados(string codigo, bool status, long? motoId = null)
        {
            if (string.IsNullOrWhiteSpace(codigo))
                throw new ArgumentException("Código obrigatório");

            Codigo = codigo;
            Status = status;
            MotoId = motoId; 
        }

        public void AssociarMoto(Moto? moto)
        {
            Moto = moto;
            MotoId = moto?.Id; 
        }
    }
}
