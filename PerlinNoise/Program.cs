// Temat: Generator szumu Perlina
// Opis: Algorytm generuje wartość szumu Perlina dla konkretnego piksela
// Autor: Mateusz Babiński, Informatyka, rok 3, sem. 5, gr. 5, data: 24.10.2022
// Wersja: 1.0

namespace Interface
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();
            Application.Run(new Form1());
        }
    }
}