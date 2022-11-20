
namespace Interface
{
    public static class Utility
    {
        // Metoda konwertuje przekazaną tablicę wartości szumu Perlina na obraz bmp.
        public static Bitmap MakeBmp(ref float[] perlinNoise, int width, int height)
        {
            var byteArray = CreateByteArrayFromPerlinNoise(ref perlinNoise, width, height);
            var byteArrayWithBmpHeader = MakeBmpByteArray(ref byteArray, width, height);
            return (Bitmap)ConvertBmpByteArrayToImage(ref byteArrayWithBmpHeader);
        }

        // Metoda konwertuje tablicę floatów na tablicę bajtów.
        private static byte[] CreateByteArrayFromPerlinNoise(ref float[] perlinNoise, int width, int height)
        {
            var output = new byte[width * height * 3];
            var index = 0;

            var max = perlinNoise.Max();
            var min = perlinNoise.Min();

            for (var y = 0; y < height; y++)
            {
                for (var x = 0; x < width; x++)
                {
                    var value = (byte)((perlinNoise[x * height + y] - min) * byte.MaxValue / (max - min));

                    output[index] = value;
                    output[index + 1] = value;
                    output[index + 2] = value;
                    index += 3;
                }
            }

            return output;
        }

        // Metoda dodaje na początek tablicy bajtów nagłówek wymagany dla obrazu bmp.
        private static byte[] MakeBmpByteArray(ref byte[] byteArray, int width, int height)
        {
            var temp = (uint)(width * height * 3 + 54);

            var bmp = new Bmp
            {
                FileType1 = 0x42,
                FileType2 = 0x4D,
                FileSize = temp,
                FileReserved1 = 0,
                FileReserved2 = 0,
                FileOffsetBits = 54,
                Size = 40,
                Width = width,
                Height = height,
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

        // Metoda konwertuje tablicę bajtów na obraz bmp.
        private static Image ConvertBmpByteArrayToImage(ref byte[] byteArray)
        {
            using var ms = new MemoryStream(byteArray);
            return Image.FromStream(ms);
        }

        // Metoda zwraca liczbę pseudolosową z podanego przedziału.
        public static int GetRandomNumberInRange(Random random, int minNumber, int maxNumber)
        {
            return random.Next() * (maxNumber - minNumber) + minNumber;
        }
    }
}
