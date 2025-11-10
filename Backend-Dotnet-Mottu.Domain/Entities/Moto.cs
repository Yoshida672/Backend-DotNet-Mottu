using Backend_Dotnet_Mottu.Domain.Enum;
using Backend_Dotnet_Mottu.Domain.ValueObjects;

namespace Backend_Dotnet_Mottu.Domain.Entities;

public class Moto
{
    public long Id { get; private set; }
    public string Placa { get; private set; }
    public Modelo Modelo { get; private set; }
    public string Dono { get; private set; }

    public long CondicaoId { get; private set; }
    public long PatioId { get; private set; }
    public virtual Condicao Condicao { get; private set; }

    public virtual TagUwb ?TagUwb { get; private set; }

    public Moto() { }

    public Moto(string placa,string dono, Modelo modelo,  Condicao condicao,long patioId)
    {
        AtualizarDados(placa,dono, modelo,  condicao,patioId);
    }

    public void AtualizarDados(string placa, string dono,Modelo modelo,  Condicao condicao,long patioId)
    {
        if (string.IsNullOrWhiteSpace(placa))
            throw new ArgumentException("Placa obrigatória");

        if (condicao == null)
            throw new ArgumentException("Condição obrigatória");
        Dono = dono;
        Placa = placa;
        Modelo = modelo;
        Condicao = condicao;
        CondicaoId = condicao.Id;
        PatioId = patioId;
    }
    public void AdicionarTag(TagUwb tag)
    {
        if (tag == null)
            throw new ArgumentException("Tag UWB obrigatória");

        if (this.TagUwb != null)
            throw new InvalidOperationException("Esta moto já possui uma Tag UWB associada.");

        if (tag.MotoId != 0 && tag.MotoId != this.Id)
            throw new InvalidOperationException("Esta Tag UWB já está associada a outra moto.");

        this.TagUwb = tag;
    }

}

