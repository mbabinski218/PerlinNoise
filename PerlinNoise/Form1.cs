// Temat: Generator szumu Perlina
// Opis: Algorytm generuje wartość szumu Perlina dla konkretnego piksela
// Autor: Mateusz Babiński, Informatyka, rok 3, sem. 5, gr. 5, data: 24.10.2022
// Wersja: 1.0

using System.Diagnostics;
using System.Drawing.Imaging;

namespace Interface
{
    public partial class Form1 : Form
    {
        // Ilość wątków, która zostanie przydzielona dla algorytmu generującego szum Perlina
        private int _numberOfThreads = Environment.ProcessorCount;

        // Każda oktawa dodaje warstwę szczegółów do obrazu
        private int _octaves = 1;

        // Określa, w jakim stopniu każda oktawa ma wpływ na ogólny kształt (reguluję amplitudę)
        private double _persistence = 0.001;

        // Szerokość wygenerowanego obrazu
        private int _size = 1024;

        // W jakim języku będzie napisana bibloteka użyta do generowania szumu Perlina
        private Library _library = Library.Assembly;

        // Przechowuje obraz bmp
        private MemoryStream _stream = new();

        public Form1()
        {
            InitializeComponent();

            // ustawienie początkowej wartość liczby wątków na ilość procesorów posiadanych przez komputer
            threadTrackBar.Value = _numberOfThreads;
            numberOfThreadsLabel.Text = _numberOfThreads.ToString();
        }

        private void ThreadTrackBar_Scroll(object sender, EventArgs e)
        {
            _numberOfThreads = threadTrackBar.Value;
            numberOfThreadsLabel.Text = _numberOfThreads.ToString();
        }

        private void OctavesTrackBar_Scroll(object sender, EventArgs e)
        {
            _octaves = octavesTrackBar.Value;
            numberOfOctavesLabel.Text = _octaves.ToString();
        }

        private void PersistenceTrackBar_Scroll(object sender, EventArgs e)
        {
            _persistence = persistenceTrackBar.Value / 1000d;
            persistenceLevelLabel.Text = _persistence.ToString();
        }

        private void widthNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            _size = (int)widthNumericUpDown.Value;
        }

        private void AssemblerRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            _library = AsemblerRadioButton.Checked ? Library.Assembly : Library.Cs;
        }

        private void CsRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            _library = CsRadioButton.Checked ? Library.Cs : Library.Assembly;
        }

        // Metoda wywoływana po kliknięciu przycisku generuj.
        // Odpowiada za tablicę wartości szumu Perlina oraz za zliczanie czasu wykonania algorytmu.
        // Następnie konwertuje tę tablicę na obraz bmp i wyświetla go użytkownikowi.
        private void GenerateButton_Click(object sender, EventArgs e)
        {
            var stopWatch = new Stopwatch();
            var perlinNoise = new PerlinNoise(_numberOfThreads, _octaves, _persistence, _size, _library);

            try
            {
                stopWatch.Start();
                var perlinNoiseArray = perlinNoise.Generate();
                stopWatch.Stop();

                Utility.MakeBmp(ref perlinNoiseArray, _size, ref _stream);
                pictureBox.Image = Image.FromStream(_stream);
                saveButton.Enabled = true;
                PictureGroupBox.Visible = true;
                var ts = stopWatch.Elapsed;
                var elapsedTime = $"{ts.Minutes:00}:{ts.Seconds:00}.{ts.Milliseconds:000}";
                listBox.Items.Add(elapsedTime + " / " + _numberOfThreads + " / " + _library);
            }
            catch (Exception ex) when (!Env._debugging)
            {
                stopWatch.Stop();
                saveButton.Enabled = false;
                PictureGroupBox.Visible = false;
                Console.WriteLine(ex.Message);
            }
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            saveFileDialog.ShowDialog();
        }

        private void saveFileDialog_FileOk(object sender, System.ComponentModel.CancelEventArgs e)
        {
            pictureBox.Image.Save(saveFileDialog.FileName, ImageFormat.Bmp);
        }
    }

    public static class Env
    {
#if DEBUG
        public static readonly bool _debugging = true;
#else
        public static readonly bool _debugging = false;
#endif
    }
}