﻿using System.IO;
using AudioSynthesis.Util;
using AudioSynthesis.Sf2.Chunks;

namespace AudioSynthesis.Sf2
{
    public class SoundFontPresets
    {
        private SampleHeader[] sHeaders;
        private PresetHeader[] pHeaders;
        private Instrument[] insts;

        public SampleHeader[] SampleHeaders
        {
            get { return sHeaders; }
        }
        public PresetHeader[] PresetHeaders
        {
            get { return pHeaders; }
        }
        public Instrument[] Instruments
        {
            get { return insts; }
        }

        public SoundFontPresets(BinaryReader reader)
        {
            string id = new string(IOHelper.Read8BitChars(reader, 4));
            int size = reader.ReadInt32();
            if(!id.ToLower().Equals("list"))
                throw new InvalidDataException("Invalid soundfont. Could not find pdta LIST chunk.");
            long readTo = reader.BaseStream.Position + size;
            id = new string(IOHelper.Read8BitChars(reader, 4));
            if (!id.ToLower().Equals("pdta"))
                throw new InvalidDataException("Invalid soundfont. The LIST chunk is not of type pdta.");

            Modulator[] presetModulators = null;
            Generator[] presetGenerators = null;
            Modulator[] instrumentModulators = null;
            Generator[] instrumentGenerators = null;

            ZoneChunk pbag = null;
            ZoneChunk ibag = null;
            PresetHeaderChunk phdr = null;
            InstrumentChunk inst = null;
            
            while (reader.BaseStream.Position < readTo)
            {
                id = new string(IOHelper.Read8BitChars(reader, 4));
                size = reader.ReadInt32();

                switch (id.ToLower())
                {
                    case "phdr":
                        phdr = new PresetHeaderChunk(id, size, reader);
                        break;
                    case "pbag":
                        pbag = new ZoneChunk(id, size, reader);
                        break;
                    case "pmod":
                        presetModulators = new ModulatorChunk(id, size, reader).Modulators;
                        break;
                    case "pgen":
                        presetGenerators = new GeneratorChunk(id, size, reader).Generators;
                        break;
                    case "inst":
                        inst = new InstrumentChunk(id, size, reader);
                        break;
                    case "ibag":
                        ibag = new ZoneChunk(id, size, reader);
                        break;
                    case "imod":
                        instrumentModulators = new ModulatorChunk(id, size, reader).Modulators;
                        break;
                    case "igen":
                        instrumentGenerators = new GeneratorChunk(id, size, reader).Generators;
                        break;
                    case "shdr":
                        sHeaders = new SampleHeaderChunk(id, size, reader).SampleHeaders;
                        break;
                    default:
                        throw new InvalidDataException("Invalid soundfont. Unrecognized sub chunk: " + id);
                }
            }
            Zone[] pZones = pbag.ToZones(presetModulators, presetGenerators);
            pHeaders = phdr.ToPresets(pZones);
            Zone[] iZones = ibag.ToZones(instrumentModulators, instrumentGenerators);
            insts = inst.ToInstruments(iZones);
        }
    }
}
