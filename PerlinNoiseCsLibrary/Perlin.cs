// Temat: Generator szumu Perlina
// Opis: Algorytm generuje wartość szumu Perlina dla konkretnego piksela
// Autor: Mateusz Babiński, Informatyka, rok 3, sem. 5, gr. 5, data: 24.10.2022
// Wersja: 1.0

using System.Numerics;

namespace PerlinNoiseCsLibrary
{
    public static class Perlin
    {
        // Seed, używany do nadania losowości generowanym wektorom.
        private static int Seed { get; set; }

        // Interpolacja za pomocą wzoru: (y - x) * (3 - 2 * w) * w * w + x, gdzie x i y to współrzędne piksela a parametr w to waga interpolacji.
        private static float Interpolate(Vector2 a, float w) => (float)((a.Y - a.X) * (3.0 - w * 2.0) * w * w + a.X);

        // Metoda generuje losowy wektor dwuwymiarowy z wartościami w przedziale od -1 do 1.
        // Cały obraz składu się z wielu pikseli a te w zależności od wartości otrzymanego wersora będą rosły lub malały.
        private static Vector2 RandomGradient(float value1, float value2)
        {
            const int w = 8 * sizeof(int);
            const int s = w / 2;
            var a = (int)value1;
            var b = (int)value2;
            a *= Seed;
            b ^= a << s | a >> w - s;
            b *= Seed;
            a ^= b << s | b >> w - s;
            a *= Seed;
            var value = a * (Math.PI / ((uint)int.MaxValue + 1));

            return new Vector2
            {
                X = (float)Math.Cos(value),
                Y = (float)Math.Sin(value)
            };
        }

        // Metoda zwraca iloczyn skalarny pomiędzy przekazanym pikselem a losowym gradientem.
        private static float DotGridGradient(float value1, float value2, Vector2 pixel)
        {
            var gradient = RandomGradient(value1, value2);

            var distanceVector = new Vector2
            {
                X = pixel.X - value1,
                Y = pixel.Y - value2
            };

            return (distanceVector.X * gradient.X + distanceVector.Y * gradient.Y);
        }

        // Metoda oblicza wartość szumu Perlina dla konkretnego piksela.
        public static float CalculatePerlinNoiseValueForPixel(int seed, Vector2 pixel)
        {
            Seed = seed;

            var vec1 = new Vector2
            {
                X = (int)Math.Floor(pixel.X),
                Y = (int)Math.Floor(pixel.Y)
            };

            var vec2 = new Vector2
            {
                X = vec1.X + 1,
                Y = vec1.Y + 1
            };

            var lerpWeights = new Vector2
            {
                X = pixel.X - vec1.X,
                Y = pixel.Y - vec1.Y
            };

            var n1 = new Vector2
            {
                X = DotGridGradient(vec1.X, vec1.Y, pixel),
                Y = DotGridGradient(vec2.X, vec1.Y, pixel)
            };

            var n2 = new Vector2
            {
                X = DotGridGradient(vec1.X, vec2.Y, pixel),
                Y = DotGridGradient(vec2.X, vec2.Y, pixel)
            };

            var lerp = new Vector2
            {
                X = Interpolate(n1, lerpWeights.X),
                Y = Interpolate(n2, lerpWeights.X)
            };

            var value = Interpolate(lerp, lerpWeights.Y);
            return (float)(value * 0.5 + 0.5);
        }
    }
}