using System;
using System.Windows.Forms;
using AudioSynthesis.Bank;
using AudioSynthesis.Bank.Components.Generators;
using AudioSynthesis.Bank.Components;

namespace DirectSoundDemo
{
    public partial class OptionsForm : Form
    {
        public int latency;
        public int sampleRate;
        public int bufferSize;
        public int bufferCount;
        public int interpolation;
        public int polyphony;

        public OptionsForm()
        {
            InitializeComponent();
        }

        private void OptionsForm_Load(object sender, EventArgs e)
        {
            numericUpDown1.Value = Properties.Settings.Default.Latency;
            numericUpDown2.Value = Properties.Settings.Default.SampleRate;
            numericUpDown3.Value = Properties.Settings.Default.BufferSize;
            numericUpDown4.Value = Properties.Settings.Default.BufferCount;
            numericUpDown5.Value = Properties.Settings.Default.poly;
            comboBox1.Items.AddRange(Enum.GetNames(typeof(InterpolationEnum)));
            comboBox1.SelectedIndex = Properties.Settings.Default.Interp;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult = System.Windows.Forms.DialogResult.OK;
            latency = (int)numericUpDown1.Value;
            sampleRate = (int)numericUpDown2.Value;
            bufferSize = (int)numericUpDown3.Value;
            bufferCount = (int)numericUpDown4.Value;
            polyphony = (int)numericUpDown5.Value;
            interpolation = comboBox1.SelectedIndex;
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            this.Close();
        }

    }
}
