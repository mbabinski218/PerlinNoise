using System.Numerics;
using System.Runtime.InteropServices;
using PerlinNoiseCsLibrary;

namespace Interface
{
    public class PerlinNoise
    {
        // Ilość wątków, która zostanie przydzielona dla algorytmu generującego szum Perlina.
        private int NumberOfThreads { get; }

        // Każda oktawa dodaje warstwę szczegółów do obrazu.
        private int Octaves { get; }

        // Określa, w jakim stopniu każda oktawa ma wpływ na ogólny kształt (reguluję amplitudę).
        private double Persistence { get; }

        // Szerokość wygenerowanego obrazu.
        private int Width{ get; }

        // Wysokość wygenerowanego obrazu.
        private int Height{ get; }

        // Generator liczb pseudolosowych.
        private Random Random { get; }

        // Funckja, która zostanie wywołana do generowania konkretnej wartości tablicy szumu Perlina.
        private Func<int, Vector2, float> CalculatePerlinNoiseValueForPixelFunc { get; }

        public PerlinNoise(int numberOfThreads, int octaves, double persistence, int width, int height, Library library)
        {
            NumberOfThreads = numberOfThreads;
            Octaves = octaves;
            Persistence = persistence;
            Width = width;
            Height = height;
            Random = new Random();

            CalculatePerlinNoiseValueForPixelFunc = library switch
            {
                Library.Cs => Perlin.CalculatePerlinNoiseValueForPixel,
                Library.Assembly => CalculatePerlinNoiseValueForPixel,
                _ => throw new InvalidOperationException()
            };
        }

        [DllImport("PerlinNoiseAsmLibrary.dll")]
        private static extern float CalculatePerlinNoiseValueForPixel(int seed, Vector2 pixel);

        // Metoda rozdziela pomiędzy wątki ilość wartości szumu Perlina do obliczenia.
        public float[] Generate()
        {
            var output = new float[Width * Height];
            var seed = Utility.GetRandomNumberInRange(Random, 1911520717, int.MaxValue);
            var tasks = new List<Task>();

            var package = Height / NumberOfThreads;
            var rest = Height % NumberOfThreads;

            var packageForTask = Enumerable.Repeat(package, NumberOfThreads).ToArray();

            for (var i = rest; i > 0; i--)
            {
                packageForTask[i]++;
            }

            for (var i = 0; i < NumberOfThreads; i++)
            {
                var fromHeight = packageForTask.Take(i).Sum();
                var toHeight = packageForTask[i];

                tasks.Add(Task.Run(() => CreatePerlinNoiseArray(ref output, fromHeight, toHeight, (int)seed)));
            }

            Task.WaitAll(tasks.ToArray());
            return output;
        }

        // Metoda wywołuje funckję, która oblicza wartość szumu Perlina dla konkretnej pozycji w tablicy.
        // Piksel jest reprezentowany przez wbudowaną strukturę Vector2, ponieważ po przekazaniu takiej struktury
        // do asemblera zajmuje tylko jeden rejestr xmm, w którym pierwsza wartość zmiennoprzecinkowa znajduje się 
        // xmm0[0-31] a druga w xmm0[32-63].
        private void CreatePerlinNoiseArray(ref float[] output, int fromHeight, int toHeight, int seed)
        {
            for (var o = 1; o <= Octaves; o++)
            {
                var amplitude = Math.Pow(2, o);
                var frequency = Math.Pow(Persistence, o);
                var fixedHeight = toHeight + fromHeight;

                for (var y = fromHeight; y < fixedHeight; y++)
                {
                    for (var x = 0; x < Width; x++)
                    {
                        var pixel = new Vector2
                        {
                            X = (float)(x * frequency),
                            Y = (float)(y * frequency)
                        };
                        output[y * Width + x] += (float)(CalculatePerlinNoiseValueForPixelFunc(seed, pixel) * amplitude);
                    }
                }
            }
        }
    }
}