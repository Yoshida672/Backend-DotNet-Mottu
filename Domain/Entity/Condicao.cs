using System.Runtime.ConstrainedExecution;

namespace CP2_BackEndMottu_DotNet.Domain.Entity
{
    public class Condicao
    {
        public Guid Id { get; set; }

        public string Nome { get; set; }

        public string Cor { get; set; }
        
        public Condicao() { }

        
        public Condicao( string nome, string cor)
        {
            Nome = nome;
            Cor = cor;
        }
    }
}
