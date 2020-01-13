using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KeySoundMaker.Models
{
	class MusicTheory
	{
		public enum Pitch
		{
			C,
			CSharp,
			D,
			DSharp,
			E,
			F,
			FSharp,
			G,
			GSharp,
			A,
			ASharp,
			B
		}

		public static string[] PitchName =
		{
			"C",
			"C#",
			"D",
			"D#",
			"E",
			"F",
			"F#",
			"G",
			"G#",
			"A",
			"A#",
			"B"
		};

		public static string[] OctaveName =
		{
			"0",
			"1",
			"2",
			"3",
			"4",
			"5",
			"6",
			"7",
			"8",
			"9",
			"10",
			"11"
		};

		public static string keyToPitchName(int key)
		{
			int pc = key % 12;
			int octave = key / 12 - 1;
			string res = $"{PitchName[pc]}{octave}";
			return res;
		}

		public static string GetFileName(int instrument, int key, int bpm, int length, int volume, bool isDrum)
		{
			string res = "";
			if (isDrum)
			{
				res += "drum_";
			}
			res += $"{instrument}_{keyToPitchName(key)}_{bpm}_{length}";
			if (volume != 100)
			{
				res += $"_{volume}";
			}
			return res;
		}
	}
}
