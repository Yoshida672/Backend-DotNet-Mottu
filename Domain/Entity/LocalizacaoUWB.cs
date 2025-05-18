namespace CP2_BackEndMottu_DotNet.Domain.Entity;

public class LocalizacaoUWB
{
    public Guid Id { get; private set; }
    public double CoordenadaX { get; private set; }
    public double CoordenadaY { get; private set; }
    public DateTime DataHora { get; private set; }

    public Guid MotoId { get; private set; }
    public virtual Moto Moto { get; private set; }
    public LocalizacaoUWB() { }


    public LocalizacaoUWB(double x, double y, Guid motoId)
    {
        Id = Guid.NewGuid();
        AtualizarCoordenadas(x, y);
        DataHora = DateTime.UtcNow;
        MotoId = motoId;
    }

    public void AtualizarCoordenadas(double x, double y)
    {
        if (x < 0 || y < 0)
            throw new ArgumentException("Coordenadas inválidas");

        CoordenadaX = x;
        CoordenadaY = y;
        DataHora = DateTime.UtcNow;
    }
}
