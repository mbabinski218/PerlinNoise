namespace Interface
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.threadTrackBar = new System.Windows.Forms.TrackBar();
            this.label1 = new System.Windows.Forms.Label();
            this.numberOfThreadsLabel = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.LibraryGroupBox = new System.Windows.Forms.GroupBox();
            this.CsRadioButton = new System.Windows.Forms.RadioButton();
            this.AsemblerRadioButton = new System.Windows.Forms.RadioButton();
            this.SizeGroupBox = new System.Windows.Forms.GroupBox();
            this.widthNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.numberOfOctavesLabel = new System.Windows.Forms.Label();
            this.octavesTrackBar = new System.Windows.Forms.TrackBar();
            this.label6 = new System.Windows.Forms.Label();
            this.persistenceTrackBar = new System.Windows.Forms.TrackBar();
            this.persistenceLevelLabel = new System.Windows.Forms.Label();
            this.PictureGroupBox = new System.Windows.Forms.GroupBox();
            this.pictureBox = new System.Windows.Forms.PictureBox();
            this.GenerateButton = new System.Windows.Forms.Button();
            this.saveButton = new System.Windows.Forms.Button();
            this.listBox = new System.Windows.Forms.ListBox();
            this.label7 = new System.Windows.Forms.Label();
            this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            ((System.ComponentModel.ISupportInitialize)(this.threadTrackBar)).BeginInit();
            this.LibraryGroupBox.SuspendLayout();
            this.SizeGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.widthNumericUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.octavesTrackBar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.persistenceTrackBar)).BeginInit();
            this.PictureGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // threadTrackBar
            // 
            this.threadTrackBar.Location = new System.Drawing.Point(144, 135);
            this.threadTrackBar.Maximum = 64;
            this.threadTrackBar.Minimum = 1;
            this.threadTrackBar.Name = "threadTrackBar";
            this.threadTrackBar.Size = new System.Drawing.Size(1116, 69);
            this.threadTrackBar.TabIndex = 0;
            this.threadTrackBar.Value = 1;
            this.threadTrackBar.Scroll += new System.EventHandler(this.ThreadTrackBar_Scroll);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 144);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(126, 25);
            this.label1.TabIndex = 1;
            this.label1.Text = "Liczba wątków";
            // 
            // numberOfThreadsLabel
            // 
            this.numberOfThreadsLabel.AutoSize = true;
            this.numberOfThreadsLabel.Location = new System.Drawing.Point(1260, 135);
            this.numberOfThreadsLabel.Name = "numberOfThreadsLabel";
            this.numberOfThreadsLabel.Size = new System.Drawing.Size(22, 25);
            this.numberOfThreadsLabel.TabIndex = 2;
            this.numberOfThreadsLabel.Text = "1";
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(0, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(100, 23);
            this.label2.TabIndex = 0;
            // 
            // LibraryGroupBox
            // 
            this.LibraryGroupBox.Controls.Add(this.CsRadioButton);
            this.LibraryGroupBox.Controls.Add(this.AsemblerRadioButton);
            this.LibraryGroupBox.Location = new System.Drawing.Point(12, 12);
            this.LibraryGroupBox.Name = "LibraryGroupBox";
            this.LibraryGroupBox.Size = new System.Drawing.Size(198, 102);
            this.LibraryGroupBox.TabIndex = 3;
            this.LibraryGroupBox.TabStop = false;
            this.LibraryGroupBox.Text = "Biblioteka";
            // 
            // CsRadioButton
            // 
            this.CsRadioButton.AutoSize = true;
            this.CsRadioButton.Location = new System.Drawing.Point(6, 65);
            this.CsRadioButton.Name = "CsRadioButton";
            this.CsRadioButton.Size = new System.Drawing.Size(59, 29);
            this.CsRadioButton.TabIndex = 4;
            this.CsRadioButton.Text = "C#";
            this.CsRadioButton.UseVisualStyleBackColor = true;
            this.CsRadioButton.CheckedChanged += new System.EventHandler(this.CsRadioButton_CheckedChanged);
            // 
            // AsemblerRadioButton
            // 
            this.AsemblerRadioButton.AutoSize = true;
            this.AsemblerRadioButton.Checked = true;
            this.AsemblerRadioButton.Location = new System.Drawing.Point(6, 30);
            this.AsemblerRadioButton.Name = "AsemblerRadioButton";
            this.AsemblerRadioButton.Size = new System.Drawing.Size(112, 29);
            this.AsemblerRadioButton.TabIndex = 0;
            this.AsemblerRadioButton.TabStop = true;
            this.AsemblerRadioButton.Text = "Asembler";
            this.AsemblerRadioButton.UseVisualStyleBackColor = true;
            this.AsemblerRadioButton.CheckedChanged += new System.EventHandler(this.AssemblerRadioButton_CheckedChanged);
            // 
            // SizeGroupBox
            // 
            this.SizeGroupBox.Controls.Add(this.widthNumericUpDown);
            this.SizeGroupBox.Location = new System.Drawing.Point(232, 12);
            this.SizeGroupBox.Name = "SizeGroupBox";
            this.SizeGroupBox.Size = new System.Drawing.Size(196, 102);
            this.SizeGroupBox.TabIndex = 4;
            this.SizeGroupBox.TabStop = false;
            this.SizeGroupBox.Text = "Rozmiar";
            // 
            // widthNumericUpDown
            // 
            this.widthNumericUpDown.Location = new System.Drawing.Point(6, 30);
            this.widthNumericUpDown.Maximum = new decimal(new int[] {
            10240,
            0,
            0,
            0});
            this.widthNumericUpDown.Name = "widthNumericUpDown";
            this.widthNumericUpDown.Size = new System.Drawing.Size(184, 31);
            this.widthNumericUpDown.TabIndex = 12;
            this.widthNumericUpDown.Value = new decimal(new int[] {
            1024,
            0,
            0,
            0});
            this.widthNumericUpDown.ValueChanged += new System.EventHandler(this.widthNumericUpDown_ValueChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 199);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(113, 25);
            this.label3.TabIndex = 5;
            this.label3.Text = "Liczba oktaw";
            // 
            // numberOfOctavesLabel
            // 
            this.numberOfOctavesLabel.AutoSize = true;
            this.numberOfOctavesLabel.Location = new System.Drawing.Point(1260, 190);
            this.numberOfOctavesLabel.Name = "numberOfOctavesLabel";
            this.numberOfOctavesLabel.Size = new System.Drawing.Size(22, 25);
            this.numberOfOctavesLabel.TabIndex = 6;
            this.numberOfOctavesLabel.Text = "1";
            // 
            // octavesTrackBar
            // 
            this.octavesTrackBar.Location = new System.Drawing.Point(144, 190);
            this.octavesTrackBar.Maximum = 12;
            this.octavesTrackBar.Minimum = 1;
            this.octavesTrackBar.Name = "octavesTrackBar";
            this.octavesTrackBar.Size = new System.Drawing.Size(1116, 69);
            this.octavesTrackBar.TabIndex = 7;
            this.octavesTrackBar.Value = 1;
            this.octavesTrackBar.Scroll += new System.EventHandler(this.OctavesTrackBar_Scroll);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(12, 248);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(107, 25);
            this.label6.TabIndex = 9;
            this.label6.Text = "Persystencja";
            // 
            // persistenceTrackBar
            // 
            this.persistenceTrackBar.Location = new System.Drawing.Point(144, 243);
            this.persistenceTrackBar.Maximum = 999;
            this.persistenceTrackBar.Minimum = 1;
            this.persistenceTrackBar.Name = "persistenceTrackBar";
            this.persistenceTrackBar.Size = new System.Drawing.Size(1116, 69);
            this.persistenceTrackBar.TabIndex = 10;
            this.persistenceTrackBar.TickFrequency = 5;
            this.persistenceTrackBar.Value = 1;
            this.persistenceTrackBar.Scroll += new System.EventHandler(this.PersistenceTrackBar_Scroll);
            // 
            // persistenceLevelLabel
            // 
            this.persistenceLevelLabel.AutoSize = true;
            this.persistenceLevelLabel.Location = new System.Drawing.Point(1260, 243);
            this.persistenceLevelLabel.Name = "persistenceLevelLabel";
            this.persistenceLevelLabel.Size = new System.Drawing.Size(22, 25);
            this.persistenceLevelLabel.TabIndex = 11;
            this.persistenceLevelLabel.Text = "1";
            // 
            // PictureGroupBox
            // 
            this.PictureGroupBox.Controls.Add(this.pictureBox);
            this.PictureGroupBox.Location = new System.Drawing.Point(130, 305);
            this.PictureGroupBox.Name = "PictureGroupBox";
            this.PictureGroupBox.Size = new System.Drawing.Size(1049, 827);
            this.PictureGroupBox.TabIndex = 13;
            this.PictureGroupBox.TabStop = false;
            this.PictureGroupBox.Text = "Wygenerowany obraz";
            this.PictureGroupBox.Visible = false;
            // 
            // pictureBox
            // 
            this.pictureBox.Location = new System.Drawing.Point(17, 30);
            this.pictureBox.Name = "pictureBox";
            this.pictureBox.Size = new System.Drawing.Size(1014, 775);
            this.pictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox.TabIndex = 0;
            this.pictureBox.TabStop = false;
            // 
            // GenerateButton
            // 
            this.GenerateButton.Font = new System.Drawing.Font("Segoe UI Semibold", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.GenerateButton.Location = new System.Drawing.Point(764, 22);
            this.GenerateButton.Name = "GenerateButton";
            this.GenerateButton.Size = new System.Drawing.Size(234, 92);
            this.GenerateButton.TabIndex = 0;
            this.GenerateButton.Text = "Generuj";
            this.GenerateButton.UseVisualStyleBackColor = true;
            this.GenerateButton.Click += new System.EventHandler(this.GenerateButton_Click);
            // 
            // saveButton
            // 
            this.saveButton.Enabled = false;
            this.saveButton.Font = new System.Drawing.Font("Segoe UI Semibold", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.saveButton.Location = new System.Drawing.Point(1021, 22);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(239, 92);
            this.saveButton.TabIndex = 14;
            this.saveButton.Text = "Zapisz obraz";
            this.saveButton.UseVisualStyleBackColor = true;
            this.saveButton.Click += new System.EventHandler(this.SaveButton_Click);
            // 
            // listBox
            // 
            this.listBox.FormattingEnabled = true;
            this.listBox.ItemHeight = 25;
            this.listBox.Location = new System.Drawing.Point(1316, 53);
            this.listBox.Name = "listBox";
            this.listBox.Size = new System.Drawing.Size(250, 1079);
            this.listBox.TabIndex = 0;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(1316, 22);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(203, 25);
            this.label7.TabIndex = 15;
            this.label7.Text = "Czas / Wątki / Biblioteka";
            // 
            // saveFileDialog
            // 
            this.saveFileDialog.DefaultExt = "bmp";
            this.saveFileDialog.FileName = "PerlinNoise";
            this.saveFileDialog.Filter = "Mapa 24-bitowa|*.bmp";
            this.saveFileDialog.FileOk += new System.ComponentModel.CancelEventHandler(this.saveFileDialog_FileOk);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1578, 1144);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.listBox);
            this.Controls.Add(this.saveButton);
            this.Controls.Add(this.GenerateButton);
            this.Controls.Add(this.PictureGroupBox);
            this.Controls.Add(this.persistenceLevelLabel);
            this.Controls.Add(this.persistenceTrackBar);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.octavesTrackBar);
            this.Controls.Add(this.numberOfOctavesLabel);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.SizeGroupBox);
            this.Controls.Add(this.LibraryGroupBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.numberOfThreadsLabel);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.threadTrackBar);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Text = "Generator szumu Perlina";
            ((System.ComponentModel.ISupportInitialize)(this.threadTrackBar)).EndInit();
            this.LibraryGroupBox.ResumeLayout(false);
            this.LibraryGroupBox.PerformLayout();
            this.SizeGroupBox.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.widthNumericUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.octavesTrackBar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.persistenceTrackBar)).EndInit();
            this.PictureGroupBox.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private TrackBar threadTrackBar;
        private Label label1;
        private Label numberOfThreadsLabel;
        private Label label2;
        private GroupBox LibraryGroupBox;
        private RadioButton AsemblerRadioButton;
        private RadioButton CsRadioButton;
        private GroupBox SizeGroupBox;
        private Label label3;
        private Label numberOfOctavesLabel;
        private TrackBar octavesTrackBar;
        private Label label6;
        private TrackBar persistenceTrackBar;
        private Label persistenceLevelLabel;
        private Label label4;
        private NumericUpDown widthNumericUpDown;
        private NumericUpDown heightNumericUpDown;
        private GroupBox PictureGroupBox;
        private Button GenerateButton;
        private Button saveButton;
        private PictureBox pictureBox;
        private ListBox listBox;
        private Label label7;
        private SaveFileDialog saveFileDialog;
    }
}