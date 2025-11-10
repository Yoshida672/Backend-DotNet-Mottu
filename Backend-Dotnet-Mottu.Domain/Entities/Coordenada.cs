namespace Backend_Dotnet_Mottu.Domain.Entities
{
    public class Coordenada
    {
        public double X { get; }
        public double Y { get; }

        public Coordenada(double x, double y)
        {
            if (x < 0 || y < 0)
                throw new ArgumentException("As coordenadas não podem ser negativas.");

            X = x;
            Y = y;
        }

        public override bool Equals(object obj)
        {
            if (obj is Coordenada outra)
                return X == outra.X && Y == outra.Y;

            return false;
        }

        public override int GetHashCode() => HashCode.Combine(X, Y);

        public override string ToString() => $"({X}, {Y})";
    }

}
