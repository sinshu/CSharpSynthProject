using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using AudioSynthesis.Bank;
using AudioSynthesis.Wave;
using AudioSynthesis.Bank.Descriptors;
using AudioSynthesis.Util;
using AudioSynthesis.Bank.Components;
using AudioSynthesis.Sfz;
using AudioSynthesis.Util.Riff;

namespace BankUtil
{
    public class BankCreator
    {
        private struct PatchInterval
        {
            public short Bank;
            public byte Start;
            public byte End;

            public PatchInterval(short bank, byte start, byte end)
            {
                Bank = bank;
                Start = start;
                End = end;
            }
            public bool WithinRange(PatchInterval pInfo)
            {
                return (pInfo.Bank == Bank) && ((pInfo.Start <= End && pInfo.Start >= Start) || (pInfo.End <= End && pInfo.End >= Start));
            }
        }
        private class PatchInfo
        {
            public int Size;
            public string Name;
            public string Type;
            public List<PatchInterval> Ranges = new List<PatchInterval>();
            public DescriptorList Description;
            public PatchInfo(string name, PatchInterval pInterval)
            {
                Name = name;
                Ranges.Add(pInterval);
            }
            public override string ToString()
            {
                return string.Format("{0}, {1}", Name, Type);
            }
        }
        private class SampleAsset
        {
            public string Name;
            public byte Channels;
            public byte Bits;
            public int SampleRate;
            public short RootKey = 60;
            public short Tune = 0;
            public double LoopStart = -1;
            public double LoopEnd = -1;
            public byte[] Data;

            public SampleAsset(string name, WaveFile wf)
            {
                Name = name;
                Channels = (byte)wf.Format.ChannelCount;
                Bits = (byte)wf.Format.BitsPerSample;

                SamplerChunk smpl = wf.FindChunk<SamplerChunk>();
                if (smpl != null)
                {
                    SampleRate = (int)(44100.0 * (1.0 / (smpl.SamplePeriod / 22675.0)));
                    RootKey = (short)smpl.UnityNote;
                    Tune = (short)(smpl.PitchFraction * 100);
                    if (smpl.Loops.Length > 0)
                    {
                        if (smpl.Loops[0].Type != SamplerChunk.SampleLoop.LoopType.Forward)
                            Console.WriteLine("Warning: Loopmode was not supported on asset: " + Name);
                        LoopStart = smpl.Loops[0].Start;
                        LoopEnd = smpl.Loops[0].End + smpl.Loops[0].Fraction + 1;
                    }
                }
                else
                {
                    SampleRate = wf.Format.SampleRate;
                }


                SampleRate = wf.Format.SampleRate;
                Data = wf.Data.RawSampleData;
            }
        }

        private string patchPath;
        private string assetPath;
        private string comments;
        private List<PatchInfo> patches = new List<PatchInfo>();
        private List<PatchInfo> multiPatches = new List<PatchInfo>();
        private List<SampleAsset> assets = new List<SampleAsset>();

        public BankCreator()
        {

        }
        public void CreateBankFile(string patchBankFileName, string outputFileName)
        {
            if (!File.Exists(patchBankFileName))
                throw new FileNotFoundException("Can not find the patch bank specified.");
            if (outputFileName.Trim().Equals(string.Empty))
                outputFileName = Path.ChangeExtension(patchBankFileName, ".bank");
            else if (!Path.HasExtension(outputFileName))
                outputFileName += Path.DirectorySeparatorChar + Path.GetFileNameWithoutExtension(patchBankFileName) + ".bank";
            else if (!Path.GetExtension(outputFileName).ToLower().Equals(".bank"))
                throw new Exception("Invalid output bank name. The bank should end with the extension (.bank)");
            ParseTextBank(patchBankFileName);
            //determine patch patch
            if (patchPath.Equals(string.Empty))
                patchPath = Path.GetDirectoryName(patchBankFileName) + Path.DirectorySeparatorChar;
            else if (!Path.IsPathRooted(patchPath))
                patchPath = Path.GetDirectoryName(patchBankFileName) + Path.DirectorySeparatorChar + patchPath;
            patchPath = patchPath.Trim();
            if (patchPath[patchPath.Length - 1] != Path.DirectorySeparatorChar && patchPath[patchPath.Length - 1] != Path.AltDirectorySeparatorChar)
                patchPath += Path.DirectorySeparatorChar;

            //determine asset path
            if (assetPath.Equals(string.Empty))
                assetPath = Path.GetDirectoryName(patchBankFileName) + Path.DirectorySeparatorChar;
            else if (!Path.IsPathRooted(assetPath))
                assetPath = Path.GetDirectoryName(patchBankFileName) + Path.DirectorySeparatorChar + assetPath;
            if (assetPath[assetPath.Length - 1] != Path.DirectorySeparatorChar && assetPath[assetPath.Length - 1] != Path.AltDirectorySeparatorChar)
                assetPath += Path.DirectorySeparatorChar;

            //Removes duplicates and checks if the patch files exist
            CheckPatches();
            LoadPatchData();
            LoadAssetData();
            CreateBank(outputFileName);
        }

        private void ParseTextBank(string patchBank)
        {
            StreamReader reader = new StreamReader(patchBank);
            string id = reader.ReadLine().Trim().ToLower();
            if (!id.Equals("[patchbank]"))
                throw new InvalidDataException("Invalid patch bank!");
            comments = ReadTag(reader, "comment").Trim();
            patchPath = ReadTag(reader, "patchpath").Trim();
            assetPath = ReadTag(reader, "assetpath").Trim();
            string[] strPatches = ReadTag(reader, "patches").Split(new string[] {"\n", "\r\n"}, StringSplitOptions.RemoveEmptyEntries);
            reader.Close();
            for (int x = 0; x < strPatches.Length; x++)
            {
                string[] args = strPatches[x].Split(new char[] { '/' }, StringSplitOptions.None);
                short bank;
                if(args[3][0] == 'i')
                    bank = 0;
                else if (args[3][0] == 'd')
                    bank = PatchBank.DrumBank;
                else
                    bank = short.Parse(args[3]);
                PatchInfo pInfo = new PatchInfo(args[0], new PatchInterval(bank, byte.Parse(args[1]), byte.Parse(args[2])));
                foreach (PatchInfo p in patches)
                {
                    if(pInfo.Ranges[0].WithinRange(p.Ranges[0]))
                    {
                        throw new Exception(string.Format("Invalid Bank. An overlap between ({0}) and ({1}) is not allowed.", p, pInfo));
                    }
                }
                patches.Add(pInfo);
            }
        }
        private string ReadTag(StreamReader reader, string expectedTag)
        {
            StringBuilder sbuild = new StringBuilder();
            int i = reader.Read();
            while (i >= 0 && i != '<')
            {
                i = reader.Read();
            }
            i = reader.Read();
            while (i >= 0 && i != '>')
            {
                sbuild.Append((char)i);
                i = reader.Read();
            }
            string tagName = sbuild.ToString().ToLower();
            if (!tagName.Equals(expectedTag))
                throw new Exception("Expected to find tag: " + expectedTag + ", but found tag: " + tagName);
            sbuild.Clear();
            i = reader.Read();
            while (i >= 0 && i != '<')
            {
                sbuild.Append((char)i);
                i = reader.Read();
            }
            string description = sbuild.ToString();
            sbuild.Clear();
            i = reader.Read();
            while (i >= 0 && i != '>')
            {
                sbuild.Append((char)i);
                i = reader.Read();
            }
            string endTagName = sbuild.ToString().ToLower();

            if (!endTagName.StartsWith("/") || !endTagName.Remove(0, 1).Equals(tagName))
                throw new Exception("Invalid tag: <" + tagName + ">.");
            return description;
        }
        private void CheckPatches()
        {
            for (int x = 0; x < patches.Count; x++)
            {
                if (!File.Exists(patchPath + patches[x].Name))
                    throw new FileNotFoundException("Can not find the patch: " + patches[x].Name);
                string patchName = Path.GetFileNameWithoutExtension(patches[x].Name).ToLower();
                for (int y = x + 1; y < patches.Count; y++)
                {
                    if (patchName.Equals(Path.GetFileNameWithoutExtension(patches[y].Name).ToLower()))
                    {
                        patches[x].Ranges.AddRange(patches[y].Ranges.ToArray());
                        patches.RemoveAt(y);
                        y--;
                    }
                }
            }
        }
        private void LoadPatchData()
        {
            for (int x = 0; x < patches.Count; x++)
            {
                switch (Path.GetExtension(patches[x].Name).ToLower())
                {
                    case ".patch":
                        using (StreamReader reader = new StreamReader(patchPath + patches[x].Name))
                        {
                            string str = reader.ReadLine();
                            if (PatchBank.BankVersion != float.Parse(str.Substring(str.IndexOf("v") + 1)))
                                throw new Exception("The patch " + patches[x].Name + " has an incorrect version.");
                            patches[x].Type = reader.ReadLine().Trim().ToLower();
                            patches[x].Description = new DescriptorList(reader);
                        }
                        if (patches[x].Type.Equals("multi"))
                        {
                            PatchInfo pInfo = patches[x];
                            patches.RemoveAt(x);
                            x--;
                            multiPatches.Add(pInfo);
                            //load sub patches
                            for (int i = 0; i < pInfo.Description.CustomDescriptions.Length; x++)
                            {
                                if (!pInfo.Description.CustomDescriptions[i].ID.Equals("mpat"))
                                    throw new Exception("Invalid multi patch: " + pInfo.Name);
                                string subPatchName = (string)pInfo.Description.CustomDescriptions[i].Objects[0];
                                if (ContainsPatch(subPatchName, patches) == false)
                                {
                                    PatchInfo subPatch = new PatchInfo(subPatchName, new PatchInterval(-1, 0, 0));
                                    patches.Add(subPatch);
                                }
                            }
                        }
                        break;
                    case ".sfz":
                    {
                        SfzReader sfz = new SfzReader(patchPath + patches[x].Name);
                        PatchInfo[] pInfos = new PatchInfo[sfz.Regions.Length];
                        int nameLen = sfz.Name.Length + 1 + sfz.Regions.Length.ToString().Length;
                        if (nameLen > 20)
                        {
                            sfz.Name = sfz.Name.Remove(0, nameLen - 20);
                        }
                        for (int i = 0; i < pInfos.Length; i++)
                        {
                            pInfos[i] = new PatchInfo(sfz.Name + "_" + i, new PatchInterval(-1, 0, 0));
                            pInfos[i].Type = "sfz ";
                            pInfos[i].Description = new DescriptorList(sfz.Regions[i]);
                        }
                        DescriptorList multiDesc = new DescriptorList();
                        multiDesc.CustomDescriptions = new CustomDescriptor[sfz.Regions.Length];
                        for (int i = 0; i < multiDesc.CustomDescriptions.Length; i++)
                        {
                            SfzRegion r = sfz.Regions[i];
                            multiDesc.CustomDescriptions[i] = new CustomDescriptor("mpat", pInfos[i].Name.Length + 14,
                                new object[] { pInfos[i].Name, r.loChan, r.hiChan, r.loKey, r.hiKey, r.loVel, r.hiVel });
                        }
                        patches[x].Type = "mult";
                        patches[x].Description = multiDesc;
                        multiPatches.Add(patches[x]);
                        patches.RemoveAt(x);
                        patches.InsertRange(0, pInfos);
                        x += pInfos.Length - 1;
                    }
                    break;
                }
            }
        }
        private void LoadAssetData()
        {
            for (int x = 0; x < patches.Count; x++)
            {
                for (int y = 0; y < patches[x].Description.GenDescriptions.Length; y++)
                {
                    GeneratorDescriptor genDesc = patches[x].Description.GenDescriptions[y];
                    string assetName = genDesc.AssetName;
                    if (genDesc.SamplerType == WaveformEnum.SampleData && !assetName.Equals("null") && ContainsAsset(assetName, assets) == false)
                    {
                        switch (Path.GetExtension(assetName).ToLower())
                        {
                            case ".wav":
                                using (WaveFileReader wr = new WaveFileReader(assetPath + assetName))
                                {
                                    assets.Add(new SampleAsset(assetName, wr.ReadWaveFile()));
                                }
                                break;
                        }
                    }
                }
            }
        }
        private void CreateBank(string bankFileName)
        {
            int infoSize = 12 + comments.Length;
            int assetListSize = GetAssetListSize(assets);
            int patchListSize = GetPatchListSize(patches, multiPatches);

            using (BinaryWriter bw = new BinaryWriter(File.Create(bankFileName)))
            {
                IOHelper.Write8BitString(bw, "RIFF", 4);
                bw.Write((int)(4 + infoSize + assetListSize + patchListSize));
                IOHelper.Write8BitString(bw, "BANK", 4);
                IOHelper.Write8BitString(bw, "INFO", 4);
                bw.Write((int)(infoSize- 8));
                bw.Write(PatchBank.BankVersion);
                IOHelper.Write8BitString(bw, comments, comments.Length);
                IOHelper.Write8BitString(bw, "LIST", 4);
                bw.Write((int)(assetListSize - 8));
                IOHelper.Write8BitString(bw, "ASET", 4);
                for (int x = 0; x < assets.Count; x++)
                {
                    IOHelper.Write8BitString(bw, "SMPL", 4);
                    bw.Write((int)(46 + assets[x].Data.Length));
                    IOHelper.Write8BitString(bw, Path.GetFileNameWithoutExtension(assets[x].Name), 20);
                    bw.Write((int)assets[x].SampleRate);
                    bw.Write((short)assets[x].RootKey);
                    bw.Write((short)assets[x].Tune);
                    bw.Write((double)assets[x].LoopStart);
                    bw.Write((double)assets[x].LoopEnd);
                    bw.Write((byte)assets[x].Bits);
                    bw.Write((byte)assets[x].Channels);
                    bw.Write(assets[x].Data);
                }
                assets.Clear();
                IOHelper.Write8BitString(bw, "LIST", 4);
                bw.Write((int)(patchListSize - 8));
                IOHelper.Write8BitString(bw, "INST", 4);
                for (int x = 0; x < patches.Count; x++)
                {
                    IOHelper.Write8BitString(bw, "PTCH", 4);
                    bw.Write((int)(patches[x].Size));
                    IOHelper.Write8BitString(bw, Path.GetFileNameWithoutExtension(patches[x].Name), 20);
                    IOHelper.Write8BitString(bw, patches[x].Type.PadRight(4,' '), 4);
                    bw.Write((short)patches[x].Description.DescriptorCount);
                    patches[x].Description.Write(bw);
                    bw.Write((short)patches[x].Ranges.Count);
                    for (int i = 0; i < patches[x].Ranges.Count; i++)
                    {
                        bw.Write((short)patches[x].Ranges[i].Bank);
                        bw.Write((byte)patches[x].Ranges[i].Start);
                        bw.Write((byte)patches[x].Ranges[i].End);
                    }
                }
                for (int x = 0; x < multiPatches.Count; x++)
                {
                    IOHelper.Write8BitString(bw, "PTCH", 4);
                    bw.Write((int)(multiPatches[x].Size));
                    IOHelper.Write8BitString(bw, Path.GetFileNameWithoutExtension(multiPatches[x].Name), 20);
                    IOHelper.Write8BitString(bw, multiPatches[x].Type.PadRight(4, ' '), 4);
                    bw.Write((short)multiPatches[x].Description.DescriptorCount);
                    multiPatches[x].Description.Write(bw);
                    bw.Write((short)multiPatches[x].Ranges.Count);
                    for (int i = 0; i < multiPatches[x].Ranges.Count; i++)
                    {
                        bw.Write((short)multiPatches[x].Ranges[i].Bank);
                        bw.Write((byte)multiPatches[x].Ranges[i].Start);
                        bw.Write((byte)multiPatches[x].Ranges[i].End);
                    }
                }
                patches.Clear();
                multiPatches.Clear();
                bw.Close();
            }
        }
        private static int GetAssetListSize(List<SampleAsset> assets)
        {
            int size = 12;
            for (int x = 0; x < assets.Count; x++)
                size += 54 + assets[x].Data.Length;
            return size;
        }
        private static int GetPatchListSize(List<PatchInfo> patches, List<PatchInfo> multiPatches)
        {
            int size = 12;
            for (int x = 0; x < patches.Count; x++)
            {
                int pSize = 28 + 4 * patches[x].Ranges.Count;
                for (int i = 0; i < patches[x].Description.CustomDescriptions.Length; i++)
                    pSize += patches[x].Description.CustomDescriptions[i].Size + 8;
                pSize += patches[x].Description.EnvelopeDescriptions.Length * (EnvelopeDescriptor.SIZE + 8);
                pSize += patches[x].Description.FilterDescriptions.Length * (FilterDescriptor.SIZE + 8);
                pSize += patches[x].Description.GenDescriptions.Length * (GeneratorDescriptor.SIZE + 8);
                pSize += patches[x].Description.LfoDescriptions.Length * (LfoDescriptor.SIZE + 8);
                patches[x].Size = pSize;
                size += 8 + pSize;
            }
            for (int x = 0; x < multiPatches.Count; x++)
            {
                int pSize = 28 + 4 * multiPatches[x].Ranges.Count;
                for (int i = 0; i < multiPatches[x].Description.CustomDescriptions.Length; i++)
                    pSize += multiPatches[x].Description.CustomDescriptions[i].Size + 8;
                multiPatches[x].Size = pSize;
                size += 8 + pSize;
            }
            return size;
        }
        private static bool ContainsPatch(string patchName, List<PatchInfo> patchList)
        {
            patchName = Path.GetFileNameWithoutExtension(patchName).ToLower();
            for (int x = 0; x < patchList.Count; x++)
            {
                string secondPatch = Path.GetFileNameWithoutExtension(patchList[x].Name).ToLower();
                if (patchName.Equals(secondPatch))
                    return true;
            }
            return false;
        }
        private static bool ContainsAsset(string assetName, List<SampleAsset> assetList)
        {
            assetName = assetName.ToLower();
            for (int x = 0; x < assetList.Count; x++)
            {
                if (assetName.Equals(assetList[x].Name))
                    return true;
            }
            return false;
        }
    }
}
