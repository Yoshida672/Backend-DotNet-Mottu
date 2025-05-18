namespace CP2_BackEndMottu_DotNet.Domain.Entity;

public class Moto
{
    public Guid Id { get; private set; }
    public string Placa { get; private set; }
    public string Modelo { get; private set; }
    public string Status { get; private set; } // Ativa, Inativa, Manutencao

    public virtual ICollection<LocalizacaoUWB> Localizacoes { get; private set; }
    public Moto() { 
    }
    public Moto(string placa, string modelo, string status)
    {
        Id = Guid.NewGuid();
        AtualizarDados(placa, modelo, status);
        Localizacoes = new List<LocalizacaoUWB>();
    }

    public void AtualizarDados(string placa, string modelo, string status)
    {
        if (string.IsNullOrWhiteSpace(placa))
            throw new ArgumentException("Placa obrigatória");

        if (string.IsNullOrWhiteSpace(modelo))
            throw new ArgumentException("Modelo obrigatório");

        if (!new[] { "Ativa", "Inativa", "Manutencao" }.Contains(status))
            throw new ArgumentException("Status inválido");

        Placa = placa;
        Modelo = modelo;
        Status = status;
    }
}
