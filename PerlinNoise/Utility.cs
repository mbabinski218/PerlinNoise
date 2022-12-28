// Temat: Generator szumu Perlina
// Opis: Algorytm generuje wartość szumu Perlina dla konkretnego piksela
// Autor: Mateusz Babiński, Informatyka, rok 3, sem. 5, gr. 5, data: 24.10.2022
// Wersja: 1.0

namespace Interface
{
    public static class Utility
    {
        // Metoda konwertuje przekazaną tablicę wartości szumu Perlina na obraz bmp.
        public static void MakeBmp(ref float[] perlinNoise, int size, ref MemoryStream ms)
        {
            var byteArray = CreateByteArrayFromPerlinNoise(ref perlinNoise, size);
            var byteArrayWithBmpHeader = MakeBmpByteArray(ref byteArray, size);
            ms = new MemoryStream(byteArrayWithBmpHeader);
        }

        // Metoda konwertuje tablicę floatów na tablicę bajtów.
        private static byte[] CreateByteArrayFromPerlinNoise(ref float[] perlinNoise, int size)
        {
            var output = new byte[size * size * 3];
            var index = 0;

            var max = perlinNoise.Max();
            var min = perlinNoise.Min();

            for (var y = 0; y < size; y++)
            {
                for (var x = 0; x < size; x++)
                {
                    var value = (byte)((perlinNoise[x * size + y] - min) * byte.MaxValue / (max - min));

                    output[index] = value;
                    output[index + 1] = value;
                    output[index + 2] = value;
                    index += 3;
                }
            }

            return output;
        }

        // Metoda dodaje na początek tablicy bajtów nagłówek wymagany dla obrazu bmp.
        private static byte[] MakeBmpByteArray(ref byte[] byteArray, int size)
        {
            var temp = (uint)(size * size * 3 + 54);

            var bmp = new Bmp
            {
                FileType1 = 0x42,
                FileType2 = 0x4D,
                FileSize = temp,
                FileReserved1 = 0,
                FileReserved2 = 0,
                FileOffsetBits = 54,
                Size = 40,
                Width = size,
                Height = size,
                Planes = 0,
                BitCount = 24,
                Compression = 0,
                SizeImage = temp,
                XPelsPerMeter = 0x0EC4,
                YPelsPerMeter = 0xEC4,
                ColorUsed = 0,
                ColorImportant = 0,
                BmpBuffer = byteArray
            };

            return bmp.MakeBmp();
        }

        // Metoda zwraca liczbę pseudolosową z podanego przedziału.
        public static int GetRandomNumberInRange(Random random, int minNumber, int maxNumber)
        {
            return random.Next() * (maxNumber - minNumber) + minNumber;
        }
    }
}
