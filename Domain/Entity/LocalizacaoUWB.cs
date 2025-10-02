namespace CP2_BackEndMottu_DotNet.Domain.Entity;

public class LocalizacaoUWB
{
    internal double CoordenadaX;
    internal double CoordenadaY;

    public Guid Id { get; private set; }
    public Coordenada Coordenada { get; private set; } 
    public DateTime DataHora { get; private set; }

    public Guid MotoId { get; private set; }
    public virtual Moto Moto { get; private set; }

    public LocalizacaoUWB() { }

    public LocalizacaoUWB(double x, double y, Guid motoId)
    {
        Id = Guid.NewGuid();
        AtualizarCoordenadas(x, y);
        MotoId = motoId;
    }

    public void AtualizarCoordenadas(double x, double y)
    {
        Coordenada = new Coordenada(x, y); 
        DataHora = DateTime.UtcNow;
    }

    public void AtualizarMotoId(Guid motoId)
    {
        MotoId = motoId;
    }
}
