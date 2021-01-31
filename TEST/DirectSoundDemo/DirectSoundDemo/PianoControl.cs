using System;
using System.Windows.Forms;
using System.Collections.Generic;
using AudioSynthesis.Midi;
using AudioSynthesis.Bank;

namespace DirectSoundDemo
{
    public partial class PianoControl : Form
    {
        private static readonly Dictionary<Keys, int> keycodes;
        public int Channel = 0;

        static PianoControl()
        {
            int x = 40;
            keycodes = new Dictionary<Keys, int>();    
            keycodes.Add(Keys.D1, x++);
            keycodes.Add(Keys.D2, x++);
            keycodes.Add(Keys.D3, x++);
            keycodes.Add(Keys.D4, x++);
            keycodes.Add(Keys.D5, x++);
            keycodes.Add(Keys.D6, x++);
            keycodes.Add(Keys.D7, x++);
            keycodes.Add(Keys.D8, x++);
            keycodes.Add(Keys.D9, x++);
            keycodes.Add(Keys.D0, x++);
            keycodes.Add(Keys.OemMinus, x++);
            keycodes.Add(Keys.Oemplus, x++);
            keycodes.Add(Keys.Q, x++);
            keycodes.Add(Keys.W, x++);
            keycodes.Add(Keys.E, x++);
            keycodes.Add(Keys.R, x++);
            keycodes.Add(Keys.T, x++);
            keycodes.Add(Keys.Y, x++);
            keycodes.Add(Keys.U, x++);
            keycodes.Add(Keys.I, x++);
            keycodes.Add(Keys.O, x++);
            keycodes.Add(Keys.P, x++);
            keycodes.Add(Keys.OemOpenBrackets, x++);
            keycodes.Add(Keys.OemCloseBrackets, x++);
            keycodes.Add(Keys.A, x++);
            keycodes.Add(Keys.S, x++);
            keycodes.Add(Keys.D, x++);
            keycodes.Add(Keys.F, x++);
            keycodes.Add(Keys.G, x++);
            keycodes.Add(Keys.H, x++);
            keycodes.Add(Keys.J, x++);
            keycodes.Add(Keys.K, x++);
            keycodes.Add(Keys.L, x++);
            keycodes.Add(Keys.OemSemicolon, x++);
            keycodes.Add(Keys.OemQuotes, x++);
            keycodes.Add(Keys.Z, x++);
            keycodes.Add(Keys.X, x++);
            keycodes.Add(Keys.C, x++);
            keycodes.Add(Keys.V, x++);
            keycodes.Add(Keys.B, x++);
            keycodes.Add(Keys.N, x++);
            keycodes.Add(Keys.M, x++);
            keycodes.Add(Keys.Oemcomma, x++);
            keycodes.Add(Keys.OemPeriod, x++);
            keycodes.Add(Keys.OemQuestion, x++);
        }
        public PianoControl()
        {
            InitializeComponent();
        }
        public int GetBank()
        {
            if (Channel == MidiHelper.DrumChannel)
                return PatchBank.DrumBank;
            return 0;
        }
        public void updateBankList(string[] progNames)
        {
            comboBox1.Items.Clear();
            comboBox1.Items.AddRange(progNames);
            comboBox1.Text = comboBox1.Items[Math.Max(0,comboBox1.SelectedIndex)].ToString();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            SynthWaveProvider.Message msg = new SynthWaveProvider.Message();
            msg.type = SynthWaveProvider.MessageType.Midi;
            msg.channel = Channel;
            msg.command = 0xB0;
            msg.data1 = 0x7B;
            msg.data2 = 0;
            ((MainForm)this.MdiParent).AddMessage(msg);
        }
        private void button2_Click(object sender, EventArgs e)
        {
            SynthWaveProvider.Message msg = new SynthWaveProvider.Message();
            msg.type = SynthWaveProvider.MessageType.Midi;
            msg.channel = Channel;
            msg.command = 0xB0;
            msg.data1 = 0x79;
            msg.data2 = 0;
            ((MainForm)this.MdiParent).AddMessage(msg);
        }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex < 0)
                return;
            SynthWaveProvider.Message msg = new SynthWaveProvider.Message();
            msg.type = SynthWaveProvider.MessageType.Midi;
            msg.channel = Channel;
            msg.command = 0xC0;
            msg.data1 = comboBox1.SelectedIndex;
            msg.data2 = 0;
            ((MainForm)this.MdiParent).AddMessage(msg);
        }     
        private void keyBoardControl1_PianoKey_Down(int key)
        {
            SynthWaveProvider.Message msg = new SynthWaveProvider.Message();
            msg.type = SynthWaveProvider.MessageType.Midi;
            msg.channel = Channel;
            msg.command = 0x90;
            msg.data1 = key;
            msg.data2 = 127;
            ((MainForm)this.MdiParent).AddMessage(msg);
        }
        private void keyBoardControl1_PianoKey_Up(int key)
        {
            SynthWaveProvider.Message msg = new SynthWaveProvider.Message();
            msg.type = SynthWaveProvider.MessageType.Midi;
            msg.channel = Channel;
            msg.command = 0x80;
            msg.data1 = key;
            msg.data2 = 64;
            ((MainForm)this.MdiParent).AddMessage(msg);
        }
        private void PianoControl_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
                e.Cancel = true;
        }
        //key presses
        private void PianoControl_KeyDown(object sender, KeyEventArgs e)
        {
            int key;
            if (keycodes.TryGetValue(e.KeyCode, out key) && !keyBoardControl1.isKeyDown(key))
            {
                keyBoardControl1.PressKey(key);
            }
        }
        private void PianoControl_KeyUp(object sender, KeyEventArgs e)
        {
            int key;
            if (keycodes.TryGetValue(e.KeyCode, out key) && keyBoardControl1.isKeyDown(key))
            {
                keyBoardControl1.ReleaseKey(key);
            }
        }
        private void comboBox1_KeyPress(object sender, KeyPressEventArgs e)
        {//block keyboard input to this control
            e.Handled = true;
        }
    }
}
