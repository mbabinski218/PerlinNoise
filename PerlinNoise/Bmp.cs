
namespace Interface
{
    public class Bmp
    {
        public byte FileType1 { get; init; }     // 1 bytes
        public byte FileType2 { get; init; }     // 1 bytes
        public uint FileSize { get; init; }          // 4 bytes
        public ushort FileReserved1 { get; init; }   // 2 bytes
        public ushort FileReserved2 { get; init; }   // 2 bytes
        public uint FileOffsetBits { get; init; }    // 4 bytes
        public uint Size { get; init; }              // 4 bytes
        public int Width { get; init; }              // 4 bytes
        public int Height { get; init; }             // 4 bytes
        public ushort Planes { get; init; }          // 2 bytes
        public ushort BitCount { get; init; }        // 2 bytes
        public uint Compression { get; init; }       // 4 bytes
        public uint SizeImage { get; init; }         // 4 bytes
        public int XPelsPerMeter { get; init; }      // 4 bytes
        public int YPelsPerMeter { get; init; }      // 4 bytes
        public uint ColorUsed { get; init; }         // 4 bytes
        public uint ColorImportant { get; init; }	 // 4 bytes
        public byte[]? BmpBuffer { get; init; }      // image

        // Metoda dodaje nagłówek na początek tablicy bajtów BmpBuffer.
        public byte[] MakeBmp()
        {
            var bmp = new byte[FileSize];

            bmp[0] = FileType1;
            bmp[1] = FileType2;

            Array.Copy(BitConverter.GetBytes(FileSize), 0, bmp, 2, 4);
            Array.Copy(BitConverter.GetBytes(FileReserved1), 0, bmp, 6, 2);
            Array.Copy(BitConverter.GetBytes(FileReserved2), 0, bmp, 8, 2);
            Array.Copy(BitConverter.GetBytes(FileOffsetBits), 0, bmp, 10, 4);
            Array.Copy(BitConverter.GetBytes(Size), 0, bmp, 14, 4);
            Array.Copy(BitConverter.GetBytes(Width), 0, bmp, 18, 4);
            Array.Copy(BitConverter.GetBytes(Height), 0, bmp, 22, 4);
            Array.Copy(BitConverter.GetBytes(Planes), 0, bmp, 26, 2);
            Array.Copy(BitConverter.GetBytes(BitCount), 0, bmp, 28, 2);
            Array.Copy(BitConverter.GetBytes(Compression), 0, bmp, 30, 4);
            Array.Copy(BitConverter.GetBytes(SizeImage), 0, bmp, 34, 4);
            Array.Copy(BitConverter.GetBytes(XPelsPerMeter), 0, bmp, 38, 4);
            Array.Copy(BitConverter.GetBytes(YPelsPerMeter), 0, bmp, 42, 4);
            Array.Copy(BitConverter.GetBytes(ColorUsed), 0, bmp, 46, 4);
            Array.Copy(BitConverter.GetBytes(ColorImportant), 0, bmp, 50, 4);

            if (BmpBuffer != null) Array.Copy(BmpBuffer, 0, bmp, 54, FileSize - FileOffsetBits);

            return bmp;
        }
    }
}