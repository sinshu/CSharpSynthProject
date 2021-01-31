using System;
using System.Windows.Forms;
using AudioSynthesis.Synthesis;

namespace DirectSoundDemo
{
    public partial class MidiControl : Form
    {
        private delegate void mctrldelegate(int[] ccList);
        private mctrldelegate mcfunc;

        public MidiControl()
        {
            InitializeComponent();
            mcfunc = new mctrldelegate(updatemidiCtrls);
        }
        public void updateMidiControls(int[] ccList)
        {
            if (this.InvokeRequired)
                this.BeginInvoke(mcfunc, new object[] { ccList });
            else
                updatemidiCtrls(ccList);
        }
        private void updatemidiCtrls(int[] ccList)
        {
            Synthesizer synth = ((MainForm)this.MdiParent).getSynth();
            if (ccList[0] != 0)
            {
                if ((ccList[0] & 0x1) == 0x1)
                    label1.Text = "Program Name: " + synth.GetProgramName(0);
                if ((ccList[0] & 0x2) == 0x2)
                    trackBar4.Value = (synth.GetChannelPitchBend(0) + 1f) / 2f;
                if ((ccList[0] & 0x4) == 0x4)
                    trackBar1.Value = synth.GetChannelVolume(0);
                if ((ccList[0] & 0x8) == 0x8)
                    trackBar3.Value = (synth.GetChannelPan(0) + 1f) / 2f;
                if ((ccList[0] & 0x10) == 0x10)
                    trackBar2.Value = synth.GetChannelExpression(0);
                if ((ccList[0] & 0x20) == 0x20)
                    pictureBox2.Image = synth.GetChannelHoldPedalStatus(0) ? Properties.Resources.holdon : Properties.Resources.holdoff;
                //pictureBox1.Image = infos[0].mute ? Properties.Resources.mute : Properties.Resources.unmute;
            }
            if (ccList[1] != 0)
            {
                if ((ccList[1] & 0x1) == 0x1)
                    label10.Text = "Program Name: " + synth.GetProgramName(1);
                if ((ccList[1] & 0x2) == 0x2)
                    trackBar5.Value = (synth.GetChannelPitchBend(1) + 1f) / 2f;
                if ((ccList[1] & 0x4) == 0x4)
                    trackBar8.Value = synth.GetChannelVolume(1);
                if ((ccList[1] & 0x8) == 0x8)
                    trackBar6.Value = (synth.GetChannelPan(1) + 1f) / 2f;
                if ((ccList[1] & 0x10) == 0x10)
                    trackBar7.Value = synth.GetChannelExpression(1);
                if ((ccList[1] & 0x20) == 0x20)
                    pictureBox3.Image = synth.GetChannelHoldPedalStatus(1) ? Properties.Resources.holdon : Properties.Resources.holdoff;
                //pictureBox4.Image = infos[0].mute ? Properties.Resources.mute : Properties.Resources.unmute;
            }
            if (ccList[2] != 0)
            {
                if ((ccList[2] & 0x1) == 0x1)
                    label15.Text = "Program Name: " + synth.GetProgramName(2);
                if ((ccList[2] & 0x2) == 0x2)
                    trackBar9.Value = (synth.GetChannelPitchBend(2) + 1f) / 2f;
                if ((ccList[2] & 0x4) == 0x4)
                    trackBar12.Value = synth.GetChannelVolume(2);
                if ((ccList[2] & 0x8) == 0x8)
                    trackBar10.Value = (synth.GetChannelPan(2) + 1f) / 2f;
                if ((ccList[2] & 0x10) == 0x10)
                    trackBar11.Value = synth.GetChannelExpression(2);
                if ((ccList[2] & 0x20) == 0x20)
                    pictureBox5.Image = synth.GetChannelHoldPedalStatus(2) ? Properties.Resources.holdon : Properties.Resources.holdoff;
                //pictureBox6.Image = infos[0].mute ? Properties.Resources.mute : Properties.Resources.unmute;
            }
            if (ccList[3] != 0)
            {
                if ((ccList[3] & 0x1) == 0x1)
                    label20.Text = "Program Name: " + synth.GetProgramName(3);
                if ((ccList[3] & 0x2) == 0x2)
                    trackBar13.Value = (synth.GetChannelPitchBend(3) + 1f) / 2f;
                if ((ccList[3] & 0x4) == 0x4)
                    trackBar16.Value = synth.GetChannelVolume(3);
                if ((ccList[3] & 0x8) == 0x8)
                    trackBar14.Value = (synth.GetChannelPan(3) + 1f) / 2f;
                if ((ccList[3] & 0x10) == 0x10)
                    trackBar15.Value = synth.GetChannelExpression(3);
                if ((ccList[3] & 0x20) == 0x20)
                    pictureBox7.Image = synth.GetChannelHoldPedalStatus(3) ? Properties.Resources.holdon : Properties.Resources.holdoff;
                //pictureBox8.Image = infos[0].mute ? Properties.Resources.mute : Properties.Resources.unmute;
            }
            if (ccList[4] != 0)
            {
                if ((ccList[4] & 0x1) == 0x1)
                    label25.Text = "Program Name: " + synth.GetProgramName(4);
                if ((ccList[4] & 0x2) == 0x2)
                    trackBar17.Value = (synth.GetChannelPitchBend(4) + 1f) / 2f;
                if ((ccList[4] & 0x4) == 0x4)
                    trackBar20.Value = synth.GetChannelVolume(4);
                if ((ccList[4] & 0x8) == 0x8)
                    trackBar18.Value = (synth.GetChannelPan(4) + 1f) / 2f;
                if ((ccList[4] & 0x10) == 0x10)
                    trackBar19.Value = synth.GetChannelExpression(4);
                if ((ccList[4] & 0x20) == 0x20)
                    pictureBox9.Image = synth.GetChannelHoldPedalStatus(4) ? Properties.Resources.holdon : Properties.Resources.holdoff;
                //pictureBox10.Image = infos[0].mute ? Properties.Resources.mute : Properties.Resources.unmute;
            }
            if (ccList[5] != 0)
            {
                if ((ccList[5] & 0x1) == 0x1)
                    label30.Text = "Program Name: " + synth.GetProgramName(5);
                if ((ccList[5] & 0x2) == 0x2)
                    trackBar21.Value = (synth.GetChannelPitchBend(5) + 1f) / 2f;
                if ((ccList[5] & 0x4) == 0x4)
                    trackBar24.Value = synth.GetChannelVolume(5);
                if ((ccList[5] & 0x8) == 0x8)
                    trackBar22.Value = (synth.GetChannelPan(5) + 1f) / 2f;
                if ((ccList[5] & 0x10) == 0x10)
                    trackBar23.Value = synth.GetChannelExpression(5);
                if ((ccList[5] & 0x20) == 0x20)
                    pictureBox11.Image = synth.GetChannelHoldPedalStatus(5) ? Properties.Resources.holdon : Properties.Resources.holdoff;
                //pictureBox12.Image = infos[0].mute ? Properties.Resources.mute : Properties.Resources.unmute;
            }
            if (ccList[6] != 0)
            {
                if ((ccList[6] & 0x1) == 0x1)
                    label35.Text = "Program Name: " + synth.GetProgramName(6);
                if ((ccList[6] & 0x2) == 0x2)
                    trackBar25.Value = (synth.GetChannelPitchBend(6) + 1f) / 2f;
                if ((ccList[6] & 0x4) == 0x4)
                    trackBar28.Value = synth.GetChannelVolume(6);
                if ((ccList[6] & 0x8) == 0x8)
                    trackBar26.Value = (synth.GetChannelPan(6) + 1f) / 2f;
                if ((ccList[6] & 0x10) == 0x10)
                    trackBar27.Value = synth.GetChannelExpression(6);
                if ((ccList[6] & 0x20) == 0x20)
                    pictureBox13.Image = synth.GetChannelHoldPedalStatus(6) ? Properties.Resources.holdon : Properties.Resources.holdoff;
                //pictureBox14.Image = infos[0].mute ? Properties.Resources.mute : Properties.Resources.unmute;
            }
            if (ccList[7] != 0)
            {
                if ((ccList[7] & 0x1) == 0x1)
                    label40.Text = "Program Name: " + synth.GetProgramName(7);
                if ((ccList[7] & 0x2) == 0x2)
                    trackBar29.Value = (synth.GetChannelPitchBend(7) + 1f) / 2f;
                if ((ccList[7] & 0x4) == 0x4)
                    trackBar32.Value = synth.GetChannelVolume(7);
                if ((ccList[7] & 0x8) == 0x8)
                    trackBar30.Value = (synth.GetChannelPan(7) + 1f) / 2f;
                if ((ccList[7] & 0x10) == 0x10)
                    trackBar31.Value = synth.GetChannelExpression(7);
                if ((ccList[7] & 0x20) == 0x20)
                    pictureBox15.Image = synth.GetChannelHoldPedalStatus(7) ? Properties.Resources.holdon : Properties.Resources.holdoff;
                //pictureBox16.Image = infos[0].mute ? Properties.Resources.mute : Properties.Resources.unmute;
            }
            if (ccList[8] != 0)
            {
                if ((ccList[8] & 0x1) == 0x1)
                    label45.Text = "Program Name: " + synth.GetProgramName(8);
                if ((ccList[8] & 0x2) == 0x2)
                    trackBar33.Value = (synth.GetChannelPitchBend(8) + 1f) / 2f;
                if ((ccList[8] & 0x4) == 0x4)
                    trackBar36.Value = synth.GetChannelVolume(8);
                if ((ccList[8] & 0x8) == 0x8)
                    trackBar34.Value = (synth.GetChannelPan(8) + 1f) / 2f;
                if ((ccList[8] & 0x10) == 0x10)
                    trackBar35.Value = synth.GetChannelExpression(8);
                if ((ccList[8] & 0x20) == 0x20)
                    pictureBox17.Image = synth.GetChannelHoldPedalStatus(8) ? Properties.Resources.holdon : Properties.Resources.holdoff;
                //pictureBox18.Image = infos[0].mute ? Properties.Resources.mute : Properties.Resources.unmute;
            }
            if (ccList[9] != 0)
            {
                if ((ccList[9] & 0x1) == 0x1)
                    label50.Text = "Program Name: " + synth.GetProgramName(9);
                if ((ccList[9] & 0x2) == 0x2)
                    trackBar37.Value = (synth.GetChannelPitchBend(9) + 1f) / 2f;
                if ((ccList[9] & 0x4) == 0x4)
                    trackBar40.Value = synth.GetChannelVolume(9);
                if ((ccList[9] & 0x8) == 0x8)
                    trackBar38.Value = (synth.GetChannelPan(9) + 1f) / 2f;
                if ((ccList[9] & 0x10) == 0x10)
                    trackBar39.Value = synth.GetChannelExpression(9);
                if ((ccList[9] & 0x20) == 0x20)
                    pictureBox19.Image = synth.GetChannelHoldPedalStatus(9) ? Properties.Resources.holdon : Properties.Resources.holdoff;
                //pictureBox20.Image = infos[0].mute ? Properties.Resources.mute : Properties.Resources.unmute;
            }
            if (ccList[10] != 0)
            {
                if ((ccList[10] & 0x1) == 0x1)
                    label55.Text = "Program Name: " + synth.GetProgramName(10);
                if ((ccList[10] & 0x2) == 0x2)
                    trackBar41.Value = (synth.GetChannelPitchBend(10) + 1f) / 2f;
                if ((ccList[10] & 0x4) == 0x4)
                    trackBar44.Value = synth.GetChannelVolume(10);
                if ((ccList[10] & 0x8) == 0x8)
                    trackBar42.Value = (synth.GetChannelPan(10) + 1f) / 2f;
                if ((ccList[10] & 0x10) == 0x10)
                    trackBar43.Value = synth.GetChannelExpression(10);
                if ((ccList[10] & 0x20) == 0x20)
                    pictureBox21.Image = synth.GetChannelHoldPedalStatus(10) ? Properties.Resources.holdon : Properties.Resources.holdoff;
                //pictureBox22.Image = infos[0].mute ? Properties.Resources.mute : Properties.Resources.unmute;
            }
            if (ccList[11] != 0)
            {
                if ((ccList[11] & 0x1) == 0x1)
                    label60.Text = "Program Name: " + synth.GetProgramName(11);
                if ((ccList[11] & 0x2) == 0x2)
                    trackBar45.Value = (synth.GetChannelPitchBend(11) + 1f) / 2f;
                if ((ccList[11] & 0x4) == 0x4)
                    trackBar48.Value = synth.GetChannelVolume(11);
                if ((ccList[11] & 0x8) == 0x8)
                    trackBar46.Value = (synth.GetChannelPan(11) + 1f) / 2f;
                if ((ccList[11] & 0x10) == 0x10)
                    trackBar47.Value = synth.GetChannelExpression(11);
                if ((ccList[11] & 0x20) == 0x20)
                    pictureBox23.Image = synth.GetChannelHoldPedalStatus(11) ? Properties.Resources.holdon : Properties.Resources.holdoff;
                //pictureBox24.Image = infos[0].mute ? Properties.Resources.mute : Properties.Resources.unmute;
            }
            if (ccList[12] != 0)
            {
                if ((ccList[12] & 0x1) == 0x1)
                    label65.Text = "Program Name: " + synth.GetProgramName(12);
                if ((ccList[12] & 0x2) == 0x2)
                    trackBar49.Value = (synth.GetChannelPitchBend(12) + 1f) / 2f;
                if ((ccList[12] & 0x4) == 0x4)
                    trackBar52.Value = synth.GetChannelVolume(12);
                if ((ccList[12] & 0x8) == 0x8)
                    trackBar50.Value = (synth.GetChannelPan(12) + 1f) / 2f;
                if ((ccList[12] & 0x10) == 0x10)
                    trackBar51.Value = synth.GetChannelExpression(12);
                if ((ccList[12] & 0x20) == 0x20)
                    pictureBox25.Image = synth.GetChannelHoldPedalStatus(12) ? Properties.Resources.holdon : Properties.Resources.holdoff;
                //pictureBox26.Image = infos[0].mute ? Properties.Resources.mute : Properties.Resources.unmute;
            }
            if (ccList[13] != 0)
            {
                if ((ccList[13] & 0x1) == 0x1)
                    label70.Text = "Program Name: " + synth.GetProgramName(13);
                if ((ccList[13] & 0x2) == 0x2)
                    trackBar53.Value = (synth.GetChannelPitchBend(13) + 1f) / 2f;
                if ((ccList[13] & 0x4) == 0x4)
                    trackBar56.Value = synth.GetChannelVolume(13);
                if ((ccList[13] & 0x8) == 0x8)
                    trackBar54.Value = (synth.GetChannelPan(13) + 1f) / 2f;
                if ((ccList[13] & 0x10) == 0x10)
                    trackBar55.Value = synth.GetChannelExpression(13);
                if ((ccList[13] & 0x20) == 0x20)
                    pictureBox27.Image = synth.GetChannelHoldPedalStatus(13) ? Properties.Resources.holdon : Properties.Resources.holdoff;
                //pictureBox28.Image = infos[0].mute ? Properties.Resources.mute : Properties.Resources.unmute;
            }
            if (ccList[14] != 0)
            {
                if ((ccList[14] & 0x1) == 0x1)
                    label75.Text = "Program Name: " + synth.GetProgramName(14);
                if ((ccList[14] & 0x2) == 0x2)
                    trackBar57.Value = (synth.GetChannelPitchBend(14) + 1f) / 2f;
                if ((ccList[14] & 0x4) == 0x4)
                    trackBar60.Value = synth.GetChannelVolume(14);
                if ((ccList[14] & 0x8) == 0x8)
                    trackBar58.Value = (synth.GetChannelPan(14) + 1f) / 2f;
                if ((ccList[14] & 0x10) == 0x10)
                    trackBar59.Value = synth.GetChannelExpression(14);
                if ((ccList[14] & 0x20) == 0x20)
                    pictureBox29.Image = synth.GetChannelHoldPedalStatus(14) ? Properties.Resources.holdon : Properties.Resources.holdoff;
                //pictureBox30.Image = infos[0].mute ? Properties.Resources.mute : Properties.Resources.unmute;
            }
            if (ccList[15] != 0)
            {
                if ((ccList[15] & 0x1) == 0x1)
                    label80.Text = "Program Name: " + synth.GetProgramName(15);
                if ((ccList[15] & 0x2) == 0x2)
                    trackBar61.Value = (synth.GetChannelPitchBend(15) + 1f) / 2f;
                if ((ccList[15] & 0x4) == 0x4)
                    trackBar64.Value = synth.GetChannelVolume(15);
                if ((ccList[15] & 0x8) == 0x8)
                    trackBar62.Value = (synth.GetChannelPan(15) + 1f) / 2f;
                if ((ccList[15] & 0x10) == 0x10)
                    trackBar63.Value = synth.GetChannelExpression(15);
                if ((ccList[15] & 0x20) == 0x20)
                    pictureBox31.Image = synth.GetChannelHoldPedalStatus(15) ? Properties.Resources.holdon : Properties.Resources.holdoff;
                //pictureBox32.Image = infos[0].mute ? Properties.Resources.mute : Properties.Resources.unmute;
            }
        }

        private void MidiControl_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
                e.Cancel = true;
        }
        private void pictureBox1_Click(object sender, EventArgs e)
        {//set mute
            MainForm mf = (MainForm)this.MdiParent;
            SynthWaveProvider.Message msg = new SynthWaveProvider.Message();
            PictureBox pic = (PictureBox)sender;
            msg.channel = int.Parse(pic.Tag.ToString());
            if (mf.ismuted(msg.channel))
            {
                msg.command = 21;
                pic.Image = Properties.Resources.unmute;
            }
            else
            {
                msg.command = 20;
                pic.Image = Properties.Resources.mute;
            }
            mf.AddMessage(msg);
        }
        private void pictureBox2_Click(object sender, EventArgs e)
        {//set hold
            MainForm mf = (MainForm)this.MdiParent;
            SynthWaveProvider.Message msg = new SynthWaveProvider.Message();
            PictureBox pic = (PictureBox)sender;
            msg.type = SynthWaveProvider.MessageType.Midi;
            msg.channel = int.Parse(pic.Tag.ToString());
            msg.command = 0xB0;
            msg.data1 = 0x40;
            if (mf.isholding(msg.channel))
            {
                msg.data2 = 0;
                pic.Image = Properties.Resources.holdoff;
            }
            else
            {
                msg.data2 = 127;
                pic.Image = Properties.Resources.holdon;
            }
            mf.AddMessage(msg);
        }
    }
}
