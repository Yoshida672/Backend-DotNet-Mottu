namespace Backend_Dotnet_Mottu.Domain.ValueObjects
{
    public class LocalizacaoUWB
    {
        public long Id { get; private set; }

        public double CoordenadaX { get; private set; }
        public double CoordenadaY { get; private set; }

        public DateTime DataHora { get; private set; }


        protected LocalizacaoUWB() { }

        public LocalizacaoUWB(double x, double y)
        {
            AtualizarCoordenadas(x, y);

        }

        public void AtualizarCoordenadas(double x, double y)
        {
            CoordenadaX = x;
            CoordenadaY = y;
            DataHora = DateTime.UtcNow;
        }
    }
}
