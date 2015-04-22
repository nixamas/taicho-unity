//using System;
//using System.Text;
//
//namespace com.cosmichorizons.utilities
//{
//
//	using Gdx = com.badlogic.gdx.Gdx;
//	using FileHandle = com.badlogic.gdx.files.FileHandle;
//	using TaichoGameData = com.cosmichorizons.gameparts.TaichoGameData;
//
//	public class SystemConfiguration
//	{
//		private string GAME_IN_PROGRESS_KEY = "game_in_progress";
//		private string NO_GAME_IN_PROGRESS = "NO_GAME_IN_PROGRESS";
//		private string SOUND_STATE_KEY = "sound_on";
//		private string MUSIC_STATE_KEY = "music_on";
//		private string STATE_ON = "true";
//		private string STATE_OFF = "false";
//		private string NEW_LINE_SEP = "/r/n";
//		private string KEY_SEP = ":";
//		private bool sound = false;
//		private bool music = false;
//		private bool in_progress = false;
//
//		internal FileHandle _configFileHandle = null;
//		internal FileHandle _savedGameFileHandle = null;
//
//		public static void Main(string[] args)
//		{
//	//		SystemConfiguration me = new SystemConfiguration();
//	//		BoardComponent bc = new BoardComponent(new TaichoUnit(Player.PLAYER_ONE), Location.PLAYER_TWO_CASTLE, new Coordinate(2, 1, 4));
//	//		StringBuffer sb = new StringBuffer();
//	//		try {
//	//			writeObject(bc, sb);
//	//		} catch (IllegalArgumentException e) {
//	//			// TODO Auto-generated catch block
//	//			e.printStackTrace();
//	//		} catch (IllegalAccessException e) {
//	//			// TODO Auto-generated catch block
//	//			e.printStackTrace();
//	//		}
//		}
//
//		public SystemConfiguration()
//		{
//			Console.WriteLine("SystemConfiguration...");
//			bool windows = false;
//			switch (Gdx.app.Type)
//			{
//				case Desktop:
//					//assuming Windows
//					windows = true;
//					this.NEW_LINE_SEP = "\r\n";
//					goto case Android;
//				case Android:
//					//not sure about android yet
//				default:
//					break;
//			}
//			try
//			{
//				if (!windows)
//				{
//					string extPath = Gdx.files.ExternalStoragePath;
//					_configFileHandle = Gdx.files.external(extPath + "files/myconfigurationfile.txt");
//				}
//				else if (windows)
//				{
//					_configFileHandle = Gdx.files.local("files\\myconfigurationfile.txt");
//				}
//				if (!windows)
//				{
//					_savedGameFileHandle = Gdx.files.external("files/savedgamefile.txt");
//				}
//				else if (windows)
//				{
//					_savedGameFileHandle = Gdx.files.local("files\\savedgamefile.txt");
//				}
//	//			String contents = _fileHandle.readString();
//				if (_configFileHandle.exists())
//				{
//					parseFileContents(_configFileHandle.readString()); //fills out parameters
//				}
//				else
//				{
//					_configFileHandle.writeString(this.DEFAULT_CONFIG_FILE, false);
//					parseFileContents(_configFileHandle.readString()); //fills out parameters
//				}
//
//				this.in_progress = checkForGameInProgress();
//
//			}
//			catch (Exception e)
//			{
//				Console.Error.WriteLine("SystemConfiguration Error :: " + e.Message);
//			}
//		}
//
//		public virtual bool checkForGameInProgress()
//		{
//			string contents = _savedGameFileHandle.readString();
//			return !contents.Contains(this.NO_GAME_IN_PROGRESS);
//		}
//
//		public virtual void writeTaichoGameData(TaichoGameData taichoGameData)
//		{
//			StringBuilder sb = new StringBuilder();
//			SaveGameObject saveObj = new SaveGameObject(taichoGameData);
//			sb.Append(saveObj.UnitSaveObjectList);
//			_savedGameFileHandle.writeString(sb.ToString(), false);
//		}
//
//		/// <summary>
//		/// writes a string in the savedgamefile.txt signifying that now game was saved
//		/// </summary>
//		public virtual void writeNoSaveData()
//		{
//			_savedGameFileHandle.writeString(this.NO_GAME_IN_PROGRESS, false);
//		}
//
//		private void writeConfigFile()
//		{
//			string contents = _configFileHandle.readString();
//			StringBuilder sbContents = new StringBuilder();
//			string[] lines = contents.Split(this.NEW_LINE_SEP, true);
//			foreach (string line in lines)
//			{
//				if (line.StartsWith("#", StringComparison.Ordinal))
//				{ //comment line
//					sbContents.Append(line);
//				}
//				else if (line.Contains(this.KEY_SEP))
//				{
//					string[] @params = line.Split(this.KEY_SEP, true);
//					string key = @params[0].Trim();
//					//check the key and store in member
//					if (this.GAME_IN_PROGRESS_KEY.Equals(key, StringComparison.CurrentCultureIgnoreCase))
//					{
//						sbContents.Append(getKeyBoolValString(key, GameInProgress));
//					}
//					else if (this.SOUND_STATE_KEY.Equals(key, StringComparison.CurrentCultureIgnoreCase))
//					{
//						sbContents.Append(getKeyBoolValString(key, SoundSet));
//					}
//					else if (this.MUSIC_STATE_KEY.Equals(key, StringComparison.CurrentCultureIgnoreCase))
//					{
//						sbContents.Append(getKeyBoolValString(key, MusicSet));
//					}
//				}
//				sbContents.Append(this.NEW_LINE_SEP);
//			}
//			string newContents = sbContents.ToString();
//			_configFileHandle.writeString(newContents, false);
//		}
//
//		private string getKeyBoolValString(string key, bool val)
//		{
//			StringBuilder sb = new StringBuilder();
//			sb.Append(key + this.KEY_SEP + val);
//			return sb.ToString();
//		}
//
//		private void parseFileContents(string data)
//		{
//			Console.WriteLine("parsing config file contents");
//			string[] lines = data.Split(this.NEW_LINE_SEP, true);
//			for (int i = 0 ; i < lines.Length ; i++)
//			{
//				if (lines[i].Contains(this.KEY_SEP) && !lines[i].StartsWith("#", StringComparison.Ordinal))
//				{
//					string[] @params = lines[i].Split(this.KEY_SEP, true);
//					string key = @params[0].Trim();
//					string val = @params[1].Trim();
//					//check the key and store in obj
//					if (this.GAME_IN_PROGRESS_KEY.Equals(key, StringComparison.CurrentCultureIgnoreCase))
//					{ //dont call 'set' as it will write config file
//						this.in_progress = setBooleanVal(val);
//					}
//					else if (this.SOUND_STATE_KEY.Equals(key, StringComparison.CurrentCultureIgnoreCase))
//					{
//						this.sound = setBooleanVal(val);
//					}
//					else if (this.MUSIC_STATE_KEY.Equals(key, StringComparison.CurrentCultureIgnoreCase))
//					{
//						this.music = setBooleanVal(val);
//					}
//				}
//			}
//		}
//
//		public virtual SaveGameObject parseSaveFile()
//		{
//			SaveGameObject saveObj = null;
//			Console.WriteLine("parsing save file contents");
//			string contents = _savedGameFileHandle.readString();
//			if (contents.Contains(this.NO_GAME_IN_PROGRESS))
//			{
//				this.in_progress = false;
//			}
//			else
//			{
//				this.in_progress = true;
//				saveObj = new SaveGameObject(contents); //.substring(1, contents.length() - 1 )
//			}
//			return saveObj;
//		}
//
//		private bool setBooleanVal(string val)
//		{
//			bool state = false;
//			if (this.STATE_ON.Equals(val, StringComparison.CurrentCultureIgnoreCase))
//			{
//				state = true;
//			}
//			else if (this.STATE_OFF.Equals(val, StringComparison.CurrentCultureIgnoreCase))
//			{
//				state = false;
//			}
//			return state;
//		}
//
//		public virtual bool SoundSet
//		{
//			get
//			{
//				return sound;
//			}
//		}
//		public virtual bool Sound
//		{
//			set
//			{
//				this.sound = value;
//				writeConfigFile();
//			}
//		}
//		public virtual bool MusicSet
//		{
//			get
//			{
//				return music;
//			}
//		}
//		public virtual bool Music
//		{
//			set
//			{
//				this.music = value;
//				writeConfigFile();
//			}
//		}
//		public virtual bool GameInProgress
//		{
//			get
//			{
//				return in_progress;
//			}
//			set
//			{
//				this.in_progress = value;
//				writeConfigFile();
//			}
//		}
//
//		private string DEFAULT_CONFIG_FILE = "#<--starts a comment line\r\n#   key:value\r\n#   look @ SystemConfiguration on what uses this file\r\n#\r\ngame_in_progress:true\r\nsound_on:true\r\nmusic_on:false\r\n";
//
//	}
//
//}