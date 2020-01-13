﻿namespace KeySoundMaker.Models
{
	static internal class Instrument
	{
		public enum InstTypeEnum
		{
			Piano,
			ChromaticPercussion,
			Organ,
			Guitar,
			Bass,
			Strings,
			Ensemble,
			Brass,
			Reed,
			Pipe,
			SynthLead,
			SynthPad,
			SynthEffects,
			Ethnic,
			Percussive,
			SoundEffects,
			Drum
		}

		public static readonly string[] InstTypeName =
		{
			"Piano",
			"Chromatic percussion",
			"Organ",
			"Guitar",
			"Bass",
			"Strings",
			"Ensemble",
			"Brass",
			"Reed",
			"Pipe",
			"Synth lead",
			"Synth pad",
			"Synth effects",
			"Ethnic",
			"Percussive",
			"Sound effects",
			"Drum"
		};

		public static string[] InstName =
		{
			// Piano
			"Acoustic grand piano",
			"Bright acoustic piano",
			"Electric grand piano",
			"Honky tonk piano",
			"Electric piano 1",
			"Electric piano 2",
			"Harpsicord",
			"Clavinet",
			// Chromatic percussion
			"Celesta",
			"Glockenspiel",
			"Music box",
			"Vibraphone",
			"Marimba",
			"Xylophone",
			"Tubular bell",
			"Dulcimer",
			// Organ
			"Hammond / drawbar organ",
			"Percussive organ",
			"Rock organ",
			"Church organ",
			"Reed organ",
			"Accordion",
			"Harmonica",
			"Tango accordion",
			// Guitar
			"Nylon string acoustic guitar",
			"Steel string acoustic guitar",
			"Jazz electric guitar",
			"Clean electric guitar",
			"Muted electric guitar",
			"Overdriven guitar",
			"Distortion guitar",
			"Guitar harmonics",
			// Bass
			"Acoustic bass",
			"Fingered electric bass",
			"Picked electric bass",
			"Fretless bass",
			"Slap bass 1",
			"Slap bass 2",
			"Synth bass 1",
			"Synth bass 2",
			// Strings
			"Violin",
			"Viola",
			"Cello",
			"Contrabass",
			"Tremolo strings",
			"Pizzicato strings",
			"Orchestral strings / harp",
			"Timpani",
			// Ensemble
			"String ensemble 1",
			"String ensemble 2 / slow strings",
			"Synth strings 1",
			"Synth strings 2",
			"Choir aahs",
			"Voice oohs",
			"Synth choir / voice",
			"Orchestra hit",
			// Brass
			"Trumpet",
			"Trombone",
			"Tuba",
			"Muted trumpet",
			"French horn",
			"Brass ensemble",
			"Synth brass 1",
			"Synth brass 2",
			// Reed
			"Soprano sax",
			"Alto sax",
			"Tenor sax",
			"Baritone sax",
			"Oboe",
			"English horn",
			"Bassoon",
			"Clarinet",
			// Pipe
			"Piccolo",
			"Flute",
			"Recorder",
			"Pan flute",
			"Bottle blow / blown bottle",
			"Shakuhachi",
			"Whistle",
			"Ocarina",
			// 	Synth lead
			"Synth square wave",
			"Synth saw wave",
			"Synth calliope",
			"Synth chiff",
			"Synth charang",
			"Synth voice",
			"Synth fifths saw",
			"Synth brass and lead",
			// Synth pad
			"Fantasia / new age",
			"Warm pad",
			"Polysynth",
			"Space vox / choir",
			"Bowed glass",
			"Metal pad",
			"Halo pad",
			"Sweep pad",
			// Synth effects
			"Ice rain",
			"Soundtrack",
			"Crystal",
			"Atmosphere",
			"Brightness",
			"Goblins",
			"Echo drops / echoes",
			"Sci fi",
			// Ethnic
			"Sitar",
			"Banjo",
			"Shamisen",
			"Koto",
			"Kalimba",
			"Bag pipe",
			"Fiddle",
			"Shanai",
			// Percussive
			"Tinkle bell",
			"Agogo",
			"Steel drums",
			"Woodblock",
			"Taiko drum",
			"Melodic tom",
			"Synth drum",
			"Reverse cymbal",
			// 	Sound effects
			"Guitar fret noise",
			"Breath noise",
			"Seashore",
			"Bird tweet",
			"Telephone ring",
			"Helicopter",
			"Applause",
			"Gunshot",
			// Drum *47
			"Acoustic bass drum",
			"Bass drum 1",
			"Side stick",
			"Acoustic snare",
			"Hand clap",
			"Electric snare",
			"Low floor tom",
			"Closed hihat",
			"High floor tom",
			"Pedal hihat",
			"Low tom",
			"Open hihat",
			"Low-mid tom",
			"High-mid tom",
			"Crash cymbal 1",
			"High tom",
			"Ride cymbal 1",
			"Chinese cymbal",
			"Ride bell",
			"Tambourine",
			"Splash cymbal",
			"Cowbell",
			"Crash cymbal 2",
			"Vibraslap",
			"Ride cymbal 2",
			"High bongo",
			"Low bongo",
			"Mute high conga",
			"Open high conga",
			"Low conga",
			"High timbale",
			"Low timbale",
			"High agogo",
			"Low agogo",
			"Cabasa",
			"Maracas",
			"Short whistle",
			"Long whistle",
			"Short guiro",
			"Long guiro",
			"Claves",
			"High wood block",
			"Low wood block",
			"Mute cuica",
			"Open cuica",
			"Mute triangle",
			"Open triangle"
		};
	}
}