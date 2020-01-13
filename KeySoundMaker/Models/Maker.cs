using Melanchall.DryWetMidi.Common;
using Melanchall.DryWetMidi.Core;
using Melanchall.DryWetMidi.Interaction;
using NAudio.Lame;
using NAudio.Wave;
using NFluidsynth;

namespace KeySoundMaker.Models
{
	internal class Maker
	{
		private Settings settings;
		private Synth synth;

		private int sfontId;

		public Maker()
		{
			sfontId = -1;
			settings = new Settings();
			settings["player.timing-source"].StringValue = "sample";
			settings["synth.gain"].DoubleValue = 1;
			settings["synth.sample-rate"].DoubleValue = 44100;
			settings["synth.lock-memory"].IntValue = 0;
			synth = new Synth(settings);
		}

		~Maker()
		{
			synth.Dispose();
			settings.Dispose();
		}

		public int SetSoundFont2(string sfont)
		{
			try
			{
				if (sfontId != -1)
				{
					synth.UnloadSoundFont(sfontId, true);
				}
				sfontId = synth.LoadSoundFont(sfont, true);
			}
			catch
			{
				return 1;
			}
			return 0;
		}

		public int SetOutputFilename(string filename)
		{
			try
			{
				settings["audio.file.name"].StringValue = filename;
			}
			catch
			{
				return 1;
			}
			return 0;
		}

		public int RenderMiditoWav(string midi)
		{
			try
			{
				var renderer = new FileRenderer(synth);
				var player = new Player(synth);

				player.Add(midi);
				player.Play();

				while (player.Status == FluidPlayerStatus.Playing)
				{
					renderer.ProcessBlock();
				}

				player.Dispose();
				renderer.Dispose();
			}
			catch
			{
				return 1;
			}

			return 0;
		}

		public void GenKeySoundMidi(string midiFilename, int instrument, int key, double bpm, double length, int volume, bool isDrum)
		{
			short ticksPerQuarterNoteTimeDivision = 120;
			byte channel = 0;
			if (isDrum)
			{
				channel = 9;
			}
			var midiFile = new MidiFile(
				   new TrackChunk(
					   new SetTempoEvent(Tempo.FromBeatsPerMinute((int)bpm).MicrosecondsPerQuarterNote),
					   new ProgramChangeEvent((SevenBitNumber)instrument)
				   ),
				   new TrackChunk(
						new NoteOnEvent((SevenBitNumber)key, (SevenBitNumber)volume)
						{
							Channel = (FourBitNumber)channel
						},
						new NoteOffEvent((SevenBitNumber)key, (SevenBitNumber)0)
						{
							Channel = (FourBitNumber)channel,
							DeltaTime = (long)(ticksPerQuarterNoteTimeDivision * 4 * length)
						},
						new TextEvent("End")
						{
							DeltaTime = ticksPerQuarterNoteTimeDivision
						}
					)
			)
			{
				TimeDivision = new TicksPerQuarterNoteTimeDivision(ticksPerQuarterNoteTimeDivision)
			};
			midiFile.Write(midiFilename, true);
		}

		public void EncodeWavtoMp3(string wavFilename, string mp3Filename)
		{
			using (var reader = new AudioFileReader(wavFilename))
			using (var writer = new LameMP3FileWriter(mp3Filename, reader.WaveFormat, LAMEPreset.ABR_160))
				reader.CopyTo(writer);
		}
	}
}