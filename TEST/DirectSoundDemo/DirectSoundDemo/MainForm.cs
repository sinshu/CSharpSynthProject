using System;
using System.IO;
using System.Collections;
using System.Configuration;
using System.Reflection;
using System.Drawing;
using System.Windows.Forms;
using AudioSynthesis.Synthesis;
using AudioSynthesis.Bank.Components;

namespace DirectSoundDemo
{
    public partial class MainForm : Form
    {
        private Log lctrl = new Log();
        private PlayList plist = new PlayList();
        private PlayControls pctrl = new PlayControls();
        private MidiControl mctrol = new MidiControl();
        private PianoControl kctrl = new PianoControl();
        private SynthThread sthread;

        public MainForm()
        {
            InitializeComponent();
            lctrl.MdiParent = this;
            plist.MdiParent = this;
            pctrl.MdiParent = this;
            mctrol.MdiParent = this;
            kctrl.MdiParent = this;
            lctrl.Visible = false;
            pctrl.Visible = true;
            plist.Visible = false;
            mctrol.Visible = false;
            kctrl.Visible = false;
        }
        public bool PlaySong(string fileName)
        {
            bool result = sthread.PlaySong(fileName);
            if (result)
            {
                pctrl.updateButtons(sthread.State);
            }
            updatetoolstripdisplay();
            return result;
        }
        public void Play()
        {
            sthread.Play();
            pctrl.updateButtons(sthread.State);
            updatetoolstripdisplay();
        }
        public void Stop()
        {
            sthread.Stop();
        }
        public void TogglePause()
        {
            sthread.TogglePause();
        }
        public void AddMessage(SynthWaveProvider.Message msg)
        {
            lctrl.addString(msg.ToString() + "\n");
            sthread.AddMessage(msg);
        }
        public void LogMessage(string value)
        {
            lctrl.addString(value + "\n");
        }
        public Synthesizer getSynth()
        {
            return sthread.Synth;
        }
        public string getProgramName(int channel)
        {
            return sthread.getProgramName(channel);
        }
        public bool ismuted(int channel)
        {
            return sthread.isMuted(channel);
        }
        public bool isholding(int channel)
        {
            return sthread.isHoldDown(channel);
        }
        public SynthWaveProvider.PlayerState GetPlayerState()
        {
            return sthread.State;
        }
        private void OpenFile(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = Properties.Settings.Default.midi_path;
            openFileDialog.Multiselect = true;
            openFileDialog.Filter = "Midi Files (*.mid;*.midi)|*.mid;*.midi";
            if (openFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                if (!sthread.SongLoaded() && !sthread.SequencerStarted())
                {
                    plist.addSongs(openFileDialog.FileNames, 0);
                    sthread.LoadSong(openFileDialog.FileNames[0]);
                    updatetoolstripdisplay();
                }
                else
                    plist.addSongs(openFileDialog.FileNames);
            }
            try
            {
                if (Directory.Exists(openFileDialog.FileName))
                    Properties.Settings.Default.midi_path = openFileDialog.FileName;
                else if (Directory.Exists(Path.GetDirectoryName(openFileDialog.FileName)))
                    Properties.Settings.Default.midi_path = Path.GetDirectoryName(openFileDialog.FileName);
            }
            catch (ArgumentException ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
        }
        private void ExitToolsStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void openBankFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = Properties.Settings.Default.bank_path;
            openFileDialog.Filter = "Bank Files (*.bank)|*.bank|All Files (*.*)|*.*";
            if (openFileDialog.ShowDialog(this) == DialogResult.OK && File.Exists(openFileDialog.FileName))
            {
                Properties.Settings.Default.BankFile = openFileDialog.FileName;
                Properties.Settings.Default.Save();
                sthread.LoadBank(Properties.Settings.Default.BankFile);
                kctrl.updateBankList(sthread.getProgramNames(kctrl.GetBank()));
                updatetoolstripdisplay();
            }
            try
            {
                if (Directory.Exists(openFileDialog.FileName))
                    Properties.Settings.Default.bank_path = openFileDialog.FileName;
                else if (Directory.Exists(Path.GetDirectoryName(openFileDialog.FileName)))
                    Properties.Settings.Default.bank_path = Path.GetDirectoryName(openFileDialog.FileName);
            }
            catch (ArgumentException ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
        }
        private void MainForm_Load(object sender, EventArgs e)
        {
            Synthesizer.InterpolationMode = (InterpolationEnum)Properties.Settings.Default.Interp;
            sthread = new SynthThread();
            sthread.Provider.TimeUpdate += new SynthWaveProvider.UpdateTime(pctrl.updateTime);
            if (File.Exists(Properties.Settings.Default.BankFile))
            {
                sthread.LoadBank(Properties.Settings.Default.BankFile);
                kctrl.updateBankList(sthread.getProgramNames(kctrl.GetBank()));
            }
            playListToolStripMenuItem.Checked = Properties.Settings.Default.show_plist;
            midiControlsToolStripMenuItem.Checked = Properties.Settings.Default.show_mctrl;
            logToolStripMenuItem.Checked = Properties.Settings.Default.show_log;
            keyBoardToolStripMenuItem.Checked = Properties.Settings.Default.show_keyboard;
            updatetoolstripdisplay();
        }
        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (sthread != null)
            {
                sthread.Close();
                sthread.Provider.TimeUpdate -= new SynthWaveProvider.UpdateTime(pctrl.updateTime);
            }
            Properties.Settings.Default.Save();
        }
        private void debugInfoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            IEnumerator er = Properties.Settings.Default.PropertyValues.GetEnumerator();
            string final = "";
            while (er.MoveNext())
            {
                SettingsPropertyValue set = (SettingsPropertyValue)er.Current;
                string s = set.PropertyValue.ToString();
                if (s.Length > 20)
                    s = "..." + s.Substring(s.Length - 20);
                final += set.Name + ": " + s + "\n";
            }
            int total = Properties.Settings.Default.BufferSize * Properties.Settings.Default.BufferCount;
            float val = total / (float)Properties.Settings.Default.SampleRate;
            final += "Total Buffer Size: " + total + " = " + (int)(val * 1000f) + "ms";
            MessageBox.Show(final);
        }
        private void playListToolStripMenuItem_CheckedChanged(object sender, EventArgs e)
        {
            if (playListToolStripMenuItem.Checked)
                plist.Visible = true;
            else
                plist.Visible = false;
            Properties.Settings.Default.show_plist = playListToolStripMenuItem.Checked;
        }
        private void midiControlsToolStripMenuItem_CheckedChanged(object sender, EventArgs e)
        {
            if (midiControlsToolStripMenuItem.Checked)
            {
                mctrol.Visible = true;
                mctrol.updateMidiControls(new int[] {-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1});
                sthread.Provider.UpdateMidiControllers += new SynthWaveProvider.UpdateTrackBars(mctrol.updateMidiControls);
            }
            else
            {
                mctrol.Visible = false;
                sthread.Provider.UpdateMidiControllers -= new SynthWaveProvider.UpdateTrackBars(mctrol.updateMidiControls);
            }
            Properties.Settings.Default.show_mctrl = midiControlsToolStripMenuItem.Checked;
        }      
        private void keyBoardToolStripMenuItem_CheckedChanged(object sender, EventArgs e)
        {
            if (keyBoardToolStripMenuItem.Checked)
                kctrl.Visible = true;
            else
                kctrl.Visible = false;
            Properties.Settings.Default.show_keyboard = keyBoardToolStripMenuItem.Checked;
        }
        private void logToolStripMenuItem_CheckedChanged(object sender, EventArgs e)
        {
            if (logToolStripMenuItem.Checked)
                lctrl.Visible = true;
            else
                lctrl.Visible = false;
            Properties.Settings.Default.show_log = logToolStripMenuItem.Checked;
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string msg = "C#Synth using DirectSound via Naudio.\n\nBuild Information: ";
            #if DEBUG
            msg += "Mode: Debug\n";
            #else
            msg += "Mode: Release\n";
            #endif
            AssemblyName nameInfo = Assembly.GetExecutingAssembly().GetName();
            msg += nameInfo.Name + ": Ver. (" + nameInfo.Version + ") Arch (" + nameInfo.ProcessorArchitecture + ")\n";
            nameInfo = Assembly.GetAssembly(typeof(AudioSynthesis.Synthesis.Synthesizer)).GetName();
            msg += nameInfo.Name + ": Ver. (" + nameInfo.Version + ") Arch (" + nameInfo.ProcessorArchitecture + ")";
            MessageBox.Show(msg, "About", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        private void unloadCurrentMidiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!sthread.SequencerStarted())
            {
                sthread.UnloadSong();
                updatetoolstripdisplay();
            }
            else
                MessageBox.Show("Sequencer must be stopped first!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }
        private void updatetoolstripdisplay()
        {
            if (sthread.SongLoaded())
            {
                toolStripStatusLabel.Text = "Song Loaded";
                toolStripStatusLabel.ForeColor = Color.Green;
            }
            else
            {
                toolStripStatusLabel.Text = "Song NOT Loaded";
                toolStripStatusLabel.ForeColor = Color.Black;
            }
            if (sthread.BankLoaded())
            {
                toolStripStatusLabel1.Text = "Bank Loaded";
                toolStripStatusLabel1.ForeColor = Color.Green;
            }
            else
            {
                toolStripStatusLabel1.Text = "Bank NOT Loaded";
                toolStripStatusLabel1.ForeColor = Color.Red;
            }
        }
        private void optionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OptionsForm ops = new OptionsForm();
            if (ops.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                plist.clearImages();
                pctrl.updateButtons(SynthWaveProvider.PlayerState.Stopped);
                if (sthread != null)
                {
                    sthread.Close();
                    sthread.Provider.TimeUpdate -= new SynthWaveProvider.UpdateTime(pctrl.updateTime);
                }
                Properties.Settings.Default.Latency = ops.latency;
                Properties.Settings.Default.SampleRate = ops.sampleRate;
                Properties.Settings.Default.BufferSize = ops.bufferSize;
                Properties.Settings.Default.BufferCount = ops.bufferCount;
                Properties.Settings.Default.Interp = ops.interpolation;
                Properties.Settings.Default.poly = ops.polyphony;
                Properties.Settings.Default.Save();
                Synthesizer.InterpolationMode = (InterpolationEnum)Properties.Settings.Default.Interp;
                //dispose of event handlers
                sthread.Provider.TimeUpdate -= new SynthWaveProvider.UpdateTime(pctrl.updateTime);
                if (mctrol.Visible == true)
                    sthread.Provider.UpdateMidiControllers -= new SynthWaveProvider.UpdateTrackBars(mctrol.updateMidiControls);
                //create new object with new event handlers
                sthread = new SynthThread();
                sthread.Provider.TimeUpdate += new SynthWaveProvider.UpdateTime(pctrl.updateTime);
                if (mctrol.Visible == true)
                    sthread.Provider.UpdateMidiControllers += new SynthWaveProvider.UpdateTrackBars(mctrol.updateMidiControls);
                if (File.Exists(Properties.Settings.Default.BankFile))
                {
                    sthread.LoadBank(Properties.Settings.Default.BankFile);
                    kctrl.updateBankList(sthread.getProgramNames(kctrl.GetBank()));
                }
            }
        }

        private void openPatchToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (sthread.State == SynthWaveProvider.PlayerState.Stopped)
            {
                PatchSelect ps = new PatchSelect(sthread.Bank);
                ps.ShowDialog();
            }
        }
    }
}
