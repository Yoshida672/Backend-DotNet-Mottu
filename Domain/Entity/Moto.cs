using CP2_BackEndMottu_DotNet.Domain.Enum;

namespace CP2_BackEndMottu_DotNet.Domain.Entity;

public class Moto
{
    public Guid Id { get; private set; }
    public string Placa { get; private set; }
    public Modelo Modelo { get; private set; }
    public string Status { get; private set; }

    public Guid CondicaoId { get; private set; }
    public virtual Condicao Condicao { get; private set; }

    public virtual ICollection<LocalizacaoUWB> Localizacoes { get; private set; }

    public Moto() { }

    public Moto(string placa, Modelo modelo, string status, Condicao condicao)
    {
        Id = Guid.NewGuid();
        AtualizarDados(placa, modelo, status, condicao);
        Localizacoes = new List<LocalizacaoUWB>();
    }

    public void AtualizarDados(string placa, Modelo modelo, string status, Condicao condicao)
    {
        if (string.IsNullOrWhiteSpace(placa))
            throw new ArgumentException("Placa obrigatória");

        if (condicao == null)
            throw new ArgumentException("Condição obrigatória");

        Placa = placa;
        Modelo = modelo;
        Status = status;
        Condicao = condicao;
        CondicaoId = condicao.Id;
    }
}

