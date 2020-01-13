using KeySoundMaker.Models;
using Stylet;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;

namespace KeySoundMaker.Pages
{
	public class MainViewModel : Screen
	{
		// Private
		private ReadOnlyCollection<string> _instTypeName;
		private ReadOnlyCollection<string> _instName;
		private ReadOnlyCollection<string> _pitchName;
		private ReadOnlyCollection<string> _octaveName;
		private string _soundfont2;
		private string _outputDir;
		private int _instTypeIdx;
		private int _instNameIdx;
		private int _pitchIdx;
		private int _octaveIdx;
		private double _bpm;
		private double _length;
		private int _volume;
		private string _outputFilename;
		private bool _buildTypeMid;
		private bool _buildTypeWav;
		private bool _buildTypeMp3;
		private bool _isDrum;

		private bool isWorking;
		private Maker maker;

		public bool IsDrum
		{
			get { return _isDrum; }
			set
			{
				SetAndNotify(ref _isDrum, value);
			}
		}

		// public
		public string Soundfont2
		{
			get { return _soundfont2; }
			set
			{
				SetAndNotify(ref _soundfont2, value);
				NotifyOfPropertyChange(() => CanListenDemo);
				NotifyOfPropertyChange(() => CanBuildKeySound);
			}
		}

		public string OutputDir
		{
			get { return _outputDir; }
			set
			{
				SetAndNotify(ref _outputDir, value);
				NotifyOfPropertyChange(() => CanListenDemo);
				NotifyOfPropertyChange(() => CanBuildKeySound);
			}
		}

		public ReadOnlyCollection<string> InstTypeName
		{
			get { return _instTypeName; }
		}

		public ReadOnlyCollection<string> InstName
		{
			get
			{
				int nameCount;
				if (InstTypeIdx == (int)Instrument.InstTypeEnum.Drum)
				{
					IsDrum = true;
					nameCount = 47;
				}
				else
				{
					IsDrum = false;
					nameCount = 8;
				}
				_instName = Array.AsReadOnly<string>(
					new List<string>(Instrument.InstName).GetRange(
						InstTypeIdx * 8, nameCount).ToArray());
				return _instName;
			}
		}

		public ReadOnlyCollection<string> PitchName
		{
			get { return _pitchName; }
		}

		public ReadOnlyCollection<string> OctaveName
		{
			get { return _octaveName; }
		}

		public int InstTypeIdx
		{
			get { return _instTypeIdx; }
			set
			{
				SetAndNotify(ref _instTypeIdx, value);
				NotifyOfPropertyChange(() => InstName);
				NotifyOfPropertyChange(() => CanListenDemo);
				NotifyOfPropertyChange(() => CanBuildKeySound);
			}
		}

		public int InstNameIdx
		{
			get { return _instNameIdx; }
			set
			{
				SetAndNotify(ref _instNameIdx, value);
				NotifyOfPropertyChange(() => CanListenDemo);
				NotifyOfPropertyChange(() => CanBuildKeySound);
			}
		}

		public int PitchIdx
		{
			get { return _pitchIdx; }
			set
			{
				SetAndNotify(ref _pitchIdx, value);
				NotifyOfPropertyChange(() => CanListenDemo);
				NotifyOfPropertyChange(() => CanBuildKeySound);
			}
		}

		public int OctaveIdx
		{
			get { return _octaveIdx; }
			set
			{
				SetAndNotify(ref _octaveIdx, value);
				NotifyOfPropertyChange(() => CanListenDemo);
				NotifyOfPropertyChange(() => CanBuildKeySound);
			}
		}

		public double Bpm
		{
			get { return _bpm; }
			set
			{
				SetAndNotify(ref _bpm, value);
				NotifyOfPropertyChange(() => CanListenDemo);
				NotifyOfPropertyChange(() => CanBuildKeySound);
			}
		}

		public double Length
		{
			get { return _length; }
			set
			{
				SetAndNotify(ref _length, value);
				NotifyOfPropertyChange(() => CanListenDemo);
				NotifyOfPropertyChange(() => CanBuildKeySound);
			}
		}

		public int Volume
		{
			get { return _volume; }
			set
			{
				SetAndNotify(ref _volume, value);
				NotifyOfPropertyChange(() => CanListenDemo);
				NotifyOfPropertyChange(() => CanBuildKeySound);
			}
		}

		public string OutputFilename
		{
			get { return _outputFilename; }
			set
			{
				SetAndNotify(ref _outputFilename, value);
			}
		}

		public bool BuildTypeMid
		{
			get { return _buildTypeMid; }
			set
			{
				SetAndNotify(ref _buildTypeMid, value);
				if (value)
				{
					SetAndNotify(ref _buildTypeWav, false);
					SetAndNotify(ref _buildTypeMp3, false);
				}
			}
		}

		public bool BuildTypeWav
		{
			get { return _buildTypeWav; }
			set
			{
				SetAndNotify(ref _buildTypeWav, value);
				if (value)
				{
					SetAndNotify(ref _buildTypeMid, false);
					SetAndNotify(ref _buildTypeMp3, false);
				}
			}
		}

		public bool BuildTypeMp3
		{
			get { return _buildTypeMp3; }
			set
			{
				SetAndNotify(ref _buildTypeMp3, value);
				if (value)
				{
					SetAndNotify(ref _buildTypeMid, false);
					SetAndNotify(ref _buildTypeWav, false);
				}
			}
		}

		public bool CanOpenSoundfont2 => !isWorking;
		public bool CanListenDemo => CheckInput();
		public bool CanBuildKeySound => CheckInput();

		private bool CheckInput()
		{
			if (isWorking)
			{
				return false;
			}
			if (string.IsNullOrEmpty(Soundfont2) || string.IsNullOrEmpty(OutputDir))
			{
				return false;
			}
			if (InstNameIdx == -1 || InstTypeIdx == -1)
			{
				return false;
			}
			if (Bpm == 0 || Length == 0 || Volume == 0)
			{
				return false;
			}
			if (PitchIdx == -1 || OctaveIdx == -1)
			{
				return false;
			}

			return true;
		}

		public MainViewModel()
		{
			// enable to type dot in textbox binding to double
			FrameworkCompatibilityPreferences.KeepTextBoxDisplaySynchronizedWithTextProperty = false;

			_instTypeName = Array.AsReadOnly(Instrument.InstTypeName);
			_instName = Array.AsReadOnly(new string[] { "unknown" });
			_pitchName = Array.AsReadOnly(MusicTheory.PitchName);
			_octaveName = Array.AsReadOnly(MusicTheory.OctaveName);
			_outputDir = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
			_instTypeIdx = 0;
			_instNameIdx = 0;
			_pitchIdx = 0;
			_octaveIdx = 5;
			_bpm = 120;
			_length = 0.25;
			_volume = 100;
			_buildTypeMid = false;
			_buildTypeWav = true;
			_buildTypeMp3 = false;

			isWorking = false;
			maker = new Maker();
		}

		public void OpenSoundfont2()
		{
			Microsoft.Win32.OpenFileDialog openfile = new Microsoft.Win32.OpenFileDialog
			{
				Title = "选择Soundfont2音色库文件",
				Filter = "Soundfont2 file|*.sf2|*.*|*.*"
			};
			if (openfile.ShowDialog() == true)
			{
				if (!string.IsNullOrEmpty(openfile.FileName))
				{
					Soundfont2 = openfile.FileName;
					if(maker.SetSoundFont2(Soundfont2) != 0){
						Soundfont2 = "";
						MessageBox.Show("Soundfont打开失败，请检查文件是否出错或路径中包含中文?","Error");
					}
				}
			}
		}

		public void SetOutputDir()
		{
			System.Windows.Forms.FolderBrowserDialog folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
			if (folderBrowserDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
			{
				if (!string.IsNullOrEmpty(folderBrowserDialog.SelectedPath))
				{
					OutputDir = folderBrowserDialog.SelectedPath;
				}
			}
			folderBrowserDialog.Dispose();
		}

		public void ListenDemo()
		{
			Task.Run(() =>
			{
				isWorking = true;
				NotifyOfPropertyChange(() => CanListenDemo);
				NotifyOfPropertyChange(() => CanBuildKeySound);
				NotifyOfPropertyChange(() => CanOpenSoundfont2);

				int instrument;
				int key;
				if (!IsDrum)
				{
					key = PitchIdx + (OctaveIdx + 1) * 12;
					instrument = InstTypeIdx * 8 + InstNameIdx;
				}
				else
				{
					instrument = 0;
					key = 35 + InstNameIdx;
				}
				if (key > 127)
				{
					MessageBox.Show("No such key");
				}
				else
				{
					string filename = MusicTheory.GetFileName(instrument, key, (int)Bpm, (int)Length, Volume, IsDrum);
					string midiFilename = $"{System.IO.Path.GetTempPath()}\\{filename}.mid";
					string wavFilename = $"{System.IO.Path.GetTempPath()}\\{filename}.wav";

					maker.GenKeySoundMidi(midiFilename, instrument, key, Bpm, Length, Volume, IsDrum);
					maker.SetOutputFilename(wavFilename);
					maker.RenderMiditoWav(midiFilename);
					System.Media.SoundPlayer player = new System.Media.SoundPlayer(wavFilename);
					player.Play();
				}
				isWorking = false;
				NotifyOfPropertyChange(() => CanListenDemo);
				NotifyOfPropertyChange(() => CanBuildKeySound);
				NotifyOfPropertyChange(() => CanOpenSoundfont2);
			});
		}

		public void BuildKeySound()
		{
			Task.Run(() =>
			{
				isWorking = true;
				NotifyOfPropertyChange(() => CanListenDemo);
				NotifyOfPropertyChange(() => CanBuildKeySound);
				NotifyOfPropertyChange(() => CanOpenSoundfont2);

				int instrument;
				int key;
				if (!IsDrum)
				{
					key = PitchIdx + (OctaveIdx + 1) * 12;
					instrument = InstTypeIdx * 8 + InstNameIdx;
				}
				else
				{
					instrument = 0;
					key = 35 + InstNameIdx;
				}
				if (key > 127)
				{
					MessageBox.Show("No such key");
				}
				else
				{
					int tick = (int)(480 * Length);
					string filename = MusicTheory.GetFileName(instrument, key, (int)Bpm, (int)tick, Volume, IsDrum);
					string tmpDir = System.IO.Path.GetTempPath();
					if (BuildTypeMid)
					{
						string midiFilename = $"{OutputDir}\\{filename}.mid";
						maker.GenKeySoundMidi(midiFilename, instrument, key, Bpm, Length, Volume, IsDrum);
						OutputFilename = midiFilename;
					}
					else
					{
						string midiFilename = $"{tmpDir}\\{filename}.mid";
						maker.GenKeySoundMidi(midiFilename, instrument, key, Bpm, Length, Volume, IsDrum);
						if (BuildTypeWav)
						{
							string wavFilename = $"{OutputDir}\\{filename}.wav";
							maker.SetOutputFilename(wavFilename);
							maker.RenderMiditoWav(midiFilename);
							OutputFilename = wavFilename;
						}
						else
						{
							string wavFilename = $"{tmpDir}\\{filename}.wav";
							maker.SetOutputFilename(wavFilename);
							maker.RenderMiditoWav(midiFilename);
							string mp3Filename = $"{OutputDir}\\{filename}.mp3";
							maker.EncodeWavtoMp3(wavFilename, mp3Filename);
							OutputFilename = mp3Filename;
						}
					}
				}
				isWorking = false;
				NotifyOfPropertyChange(() => CanListenDemo);
				NotifyOfPropertyChange(() => CanBuildKeySound);
				NotifyOfPropertyChange(() => CanOpenSoundfont2);
			});
		}
	}
}