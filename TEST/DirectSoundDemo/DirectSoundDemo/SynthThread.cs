using AudioSynthesis.Synthesis;
using AudioSynthesis.Sequencer;
using AudioSynthesis.Midi;
using NAudio.Wave;
using System.IO;
using AudioSynthesis.Bank;
using AudioSynthesis.Bank.Patches;

namespace DirectSoundDemo
{
    public class SynthThread
    {
        private Synthesizer synth;
        private MidiFileSequencer mseq;
        private DirectSoundOut direct_out;
        private SynthWaveProvider synth_provider;

        public SynthThread()
        {
            synth = new Synthesizer(Properties.Settings.Default.SampleRate, 2, Properties.Settings.Default.BufferSize, Properties.Settings.Default.BufferCount, Properties.Settings.Default.poly);
            mseq = new MidiFileSequencer(synth);
            synth_provider = new SynthWaveProvider(synth, mseq);
            direct_out = new DirectSoundOut(Properties.Settings.Default.Latency);
            direct_out.Init(synth_provider);
        }
        public PatchBank Bank
        {
            get { return synth.SoundBank; }
        }
        public SynthWaveProvider Provider
        {
            get { return synth_provider; }
        }
        public SynthWaveProvider.PlayerState State
        {
            get { return synth_provider.state; }
        }
        public Synthesizer Synth
        {
            get { return synth; }
        }
        public void LoadBank(string bankfile)
        {
            Stop();
            synth.LoadBank(bankfile);
        }
        public bool SongLoaded()
        {
            return mseq.IsMidiLoaded;
        }
        public bool BankLoaded()
        {
            return synth.SoundBank != null;
        }
        public bool SequencerStarted()
        {
            return mseq.IsPlaying;
        }
        public void UnloadSong()
        {
            if (!mseq.IsPlaying)
                mseq.UnloadMidi();
        }
        public void LoadSong(string fileName)
        {
            if (!mseq.IsPlaying)
                mseq.LoadMidi(new MidiFile(fileName));
        }
        public bool PlaySong(string fileName)
        {
            if (synth.SoundBank == null)
                return false;
            Stop();
            mseq.LoadMidi(new MidiFile(fileName));
            mseq.Play();
            Play();
            return true;
        }
        public void Play()
        {
            if (synth.SoundBank == null)
                return;
            if (mseq.IsMidiLoaded && !mseq.IsPlaying)
                mseq.Play();
            if (synth_provider.state != SynthWaveProvider.PlayerState.Playing)
            {
                synth_provider.state = SynthWaveProvider.PlayerState.Playing;
                if(direct_out.PlaybackState != PlaybackState.Playing)
                    direct_out.Play();
            }
        }
        public void Stop()
        {
            if (synth_provider.state != SynthWaveProvider.PlayerState.Stopped)
            {
                lock (synth_provider.lockObj)
                {
                    mseq.Stop();
                    synth.NoteOffAll(true);
                    synth.ResetSynthControls();
                    synth.ResetPrograms();
                    synth_provider.state = SynthWaveProvider.PlayerState.Stopped;
                    synth_provider.Reset();
                }
            }
        }
        public void TogglePause()
        {
            if (synth_provider.state == SynthWaveProvider.PlayerState.Playing)
            {
                synth_provider.state = SynthWaveProvider.PlayerState.Paused;
            }
            else if (synth_provider.state == SynthWaveProvider.PlayerState.Paused)
            {
                synth_provider.state = SynthWaveProvider.PlayerState.Playing;
            }
        }
        public void AddMessage(SynthWaveProvider.Message msg)
        {
            lock (synth_provider.lockObj)
            {
                synth_provider.msgQueue.Enqueue(msg);
            }
        }
        public bool isMuted(int channel)
        {
            return mseq.IsChannelMuted(channel);
        }
        public bool isHoldDown(int channel)
        {
            return synth.GetChannelHoldPedalStatus(channel);
        }
        public string getProgramName(int channel)
        {
            return synth.GetProgramName(channel);
        }
        public string[] getProgramNames(int bankNumber)
        {
            string[] names = new string[PatchBank.BankSize];
            for (int x = 0; x < names.Length; x++)
            {
                Patch p = synth.SoundBank.GetPatch(bankNumber, x);
                if (p == null)
                    names[x] = "Null";
                else
                    names[x] = p.Name;
            }
            return names;
        }
        public void Close()
        {
            Stop();
            direct_out.Stop();
            direct_out.Dispose();
            synth.UnloadBank();
            mseq.UnloadMidi();
        }
    }
}
