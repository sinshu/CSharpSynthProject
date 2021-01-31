using System;
using System.Windows.Forms;
using AudioSynthesis.Synthesis;

namespace DirectSoundDemo
{
    public partial class PlayControls : Form
    {
        private delegate void timedelegate(int current, int max);
        private timedelegate tfunc;

        public PlayControls()
        {
            InitializeComponent();
            tfunc = new timedelegate(updateTimeControls);
        }

        public void updateButtons(SynthWaveProvider.PlayerState pstate)
        {
            if (pstate != SynthWaveProvider.PlayerState.Stopped)
                pictureBox2.Image = Properties.Resources.stop;
            else
                pictureBox2.Image = Properties.Resources.play;
        }

        public void updateTime(int current, int max)
        {
            if (trackBar1.InvokeRequired || label1.InvokeRequired)
                this.BeginInvoke(tfunc, new object[] {current, max});
            else
                updateTimeControls(current, max);
        }
        private void updateTimeControls(int current, int max)
        {
            max = (int)SynthHelper.TimeFromSamples(Properties.Settings.Default.SampleRate, max);
            current = (int)SynthHelper.TimeFromSamples(Properties.Settings.Default.SampleRate, current);
            current = Math.Min(max, current);
            if (trackBar1.Maximum != max)
            {
                trackBar1.Maximum = max;
                trackBar1.TickFrequency = Math.Max(1, trackBar1.Maximum / 20);
            }
            trackBar1.Value = current;
            label1.Text = new TimeSpan(0, 0, current).ToString(@"mm\:ss") + "/" + new TimeSpan(0, 0, max).ToString(@"mm\:ss"); 
        }
        public int getTime()
        {
            return trackBar1.Value;
        }
        public float getVolume()
        {
            return 2f*(hScrollBar1.Value / (float)hScrollBar1.Maximum);
        }
        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            pictureBox1.Image = Properties.Resources.forward_d;
        }
        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            pictureBox1.Image = Properties.Resources.forward;
        }
        private void pictureBox4_Click(object sender, EventArgs e)
        {
            ((MainForm)this.MdiParent).TogglePause();
        }
        private void pictureBox4_MouseDown(object sender, MouseEventArgs e)
        {
            pictureBox4.Image = Properties.Resources.pause_d;
        }
        private void pictureBox4_MouseUp(object sender, MouseEventArgs e)
        {
            pictureBox4.Image = Properties.Resources.pause;
        }
        private void pictureBox2_Click(object sender, EventArgs e)
        {
            MainForm pform = (MainForm)this.MdiParent;
            if (pform.GetPlayerState() == SynthWaveProvider.PlayerState.Playing)
            {
                pform.Stop();
                trackBar1.Value = 0;
            }
            else
            {
                pform.Play();
            }
        }
        private void pictureBox2_MouseDown(object sender, MouseEventArgs e)
        {
            if (((MainForm)this.ParentForm).GetPlayerState() == SynthWaveProvider.PlayerState.Playing)
                pictureBox2.Image = Properties.Resources.stop_d;
            else
                pictureBox2.Image = Properties.Resources.play_d;
        }
        private void pictureBox2_MouseUp(object sender, MouseEventArgs e)
        {
            if (((MainForm)this.ParentForm).GetPlayerState() == SynthWaveProvider.PlayerState.Playing)
                pictureBox2.Image = Properties.Resources.stop;
            else
                pictureBox2.Image = Properties.Resources.play;
        }
        private void pictureBox3_MouseDown(object sender, MouseEventArgs e)
        {
            pictureBox3.Image = Properties.Resources.backward_d;
        }
        private void pictureBox3_MouseUp(object sender, MouseEventArgs e)
        {
            pictureBox3.Image = Properties.Resources.backward;
        }
        private void hScrollBar1_Scroll(object sender, ScrollEventArgs e)
        {
            if (e.NewValue == e.OldValue)
                return;
            SynthWaveProvider.Message msg = new SynthWaveProvider.Message();
            msg.channel = 0;
            msg.command = 10;
            msg.data1 = hScrollBar1.Value;
            msg.data2 = hScrollBar1.Maximum;
            msg.type = SynthWaveProvider.MessageType.Synth;
            ((MainForm)this.ParentForm).AddMessage(msg);
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            SynthWaveProvider.Message msg = new SynthWaveProvider.Message();
            msg.channel = 0;
            msg.command = 15;
            msg.data1 = Math.Min(trackBar1.Maximum, trackBar1.Value + trackBar1.TickFrequency);
            msg.type = SynthWaveProvider.MessageType.Synth;
            ((MainForm)this.ParentForm).AddMessage(msg);
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            SynthWaveProvider.Message msg = new SynthWaveProvider.Message();
            msg.channel = 0;
            msg.command = 15;
            msg.data1 = Math.Max(0, trackBar1.Value - trackBar1.TickFrequency);
            msg.type = SynthWaveProvider.MessageType.Synth;
            ((MainForm)this.ParentForm).AddMessage(msg);
        }

        private void PlayControls_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
                e.Cancel = true;
        }
    }
}
