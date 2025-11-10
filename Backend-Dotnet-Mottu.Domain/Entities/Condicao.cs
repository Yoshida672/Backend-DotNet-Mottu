namespace Backend_Dotnet_Mottu.Domain.Entities
{
    public class Condicao
    {
        public long Id { get; set; }

        public string Nome { get; set; }

        public string Cor { get; set; }
        public Moto Moto { get; set; }

        public Condicao() { }


        public Condicao(string nome, string cor)
        {
            Nome = nome;
            Cor = cor;
        }
    }
}
