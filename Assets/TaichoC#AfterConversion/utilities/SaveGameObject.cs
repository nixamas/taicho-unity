//using System;
//using System.Collections.Generic;
//using System.Text;
//
//namespace com.cosmichorizons.utilities
//{
//
//	using BoardComponent = com.cosmichorizons.basecomponents.BoardComponent;
//	using Coordinate = com.cosmichorizons.basecomponents.Coordinate;
//	using MovableObject = com.cosmichorizons.basecomponents.MovableObject;
//	using OneUnit = com.cosmichorizons.characters.OneUnit;
//	using TaichoUnit = com.cosmichorizons.characters.TaichoUnit;
//	using ThreeUnit = com.cosmichorizons.characters.ThreeUnit;
//	using TwoUnit = com.cosmichorizons.characters.TwoUnit;
//	using Player = com.cosmichorizons.enums.Player;
//	using Ranks = com.cosmichorizons.enums.Ranks;
//	using TaichoGameData = com.cosmichorizons.gameparts.TaichoGameData;
//
//	public class SaveGameObject
//	{
//		private LinkedList<UnitSaveObject> saveParts;
//		private Player currentPlayer = Player.NONE;
//		private string MEM_SEP = "@";
//		private string UNIT_SEP = "%";
//		private string LIST_START = "[";
//		private string LIST_END = "]";
//		private string CURRENT_PLAYER = "CURRENT_PLAYER";
//
//		public SaveGameObject(TaichoGameData taichoGameData)
//		{
//			saveParts = new LinkedList<UnitSaveObject>();
//			currentPlayer = taichoGameData.CurrentPlayer;
//			for (int col = 0; col < 15; col++)
//			{
//				for (int row = 0; row < 9; row++)
//				{
//					BoardComponent bc = taichoGameData.pieceAt(row, col);
//					if (bc.Occupied)
//					{
//						saveParts.AddLast(new UnitSaveObject(this, bc));
//					}
//				}
//			}
//
//		}
//
//		public SaveGameObject(string saveFileContents)
//		{
//			saveParts = new LinkedList<UnitSaveObject>();
//			string currPlayerString = saveFileContents.Substring(1, saveFileContents.IndexOf(this.LIST_END + this.LIST_START, StringComparison.Ordinal) - 1); //get current player data
//			saveFileContents = saveFileContents.Replace(this.LIST_START + currPlayerString + this.LIST_END, ""); //remove current player data
//			currPlayerString = currPlayerString.Replace(this.CURRENT_PLAYER + ":", ""); //remove current player key string
//			currentPlayer = Player.valueOf(currPlayerString); //set player enum
//			saveFileContents = saveFileContents.Substring(1, saveFileContents.Length - 1 - 1);
//			string[] elems = saveFileContents.Split(this.UNIT_SEP, true);
//			foreach (string elem in elems)
//			{
//				string[] mems = elem.Split(this.MEM_SEP, true);
//				Coordinate coor = null;
//				MovableObject mo = null;
//				foreach (string param in mems)
//				{
//					param = param.Substring(1, param.Length - 1 - 1);
//					string className = param.Split(":", true)[0];
//					if ("Coordinate".Equals(className, StringComparison.CurrentCultureIgnoreCase))
//					{
//						coor = parseCoordinateString(param.Split(":", true)[1]);
//					}
//					else if ("MovableObject".Equals(className, StringComparison.CurrentCultureIgnoreCase))
//					{
//						mo = paraseMovableObjString(param.Split(":", true)[1]);
//					}
//				}
//				saveParts.AddLast(new UnitSaveObject(this, new BoardComponent(mo, TaichoGameData.getLocationFromBoardComponentId(coor.Id), coor)));
//			}
//		}
//
//		private Coordinate parseCoordinateString(string cont)
//		{
//			// "{Coordinate:[id=" + id + ", posX=" + posX + ", posY=" + posY + "]}"
//			string[] fields = ((string) cont.Substring(1, cont.Length - 1 - 1)).Split(",", true);
//			return new Coordinate(int.Parse(fields[1].Trim().Replace("posX=", "")), int.Parse(fields[2].Trim().Replace("posY=", "")), int.Parse(fields[0].Trim().Replace("id=", "")));
//		}
//
//		private MovableObject paraseMovableObjString(string cont)
//		{
//			//  "{MovableObject:[player=" + player + ", rank=" + rank + "]}"
//			string[] fields = ((string) cont.Substring(1, cont.Length - 1 - 1)).Split(",", true);
//			Player p = Player.NONE;
//			Ranks r = Ranks.NONE;
//			MovableObject moveObj = null;
//			if (fields[0].Contains("player"))
//			{
//				string player = fields[0].Trim().Replace("player=", "");
//				p = Player.valueOf(player);
//			}
//			if (fields[1].Contains("rank"))
//			{
//				string rank = fields[1].Trim().Replace("rank=", "");
//				r = Ranks.valueOf(rank);
//			}
//			switch (r)
//			{
//			case TAICHO:
//				moveObj = new TaichoUnit(p);
//				break;
//			case LEVEL_ONE:
//				moveObj = new OneUnit(p);
//				break;
//			case LEVEL_TWO:
//				moveObj = new TwoUnit(p);
//				break;
//			case LEVEL_THREE:
//				moveObj = new ThreeUnit(p);
//				break;
//			default:
//				break;
//			}
//			return moveObj;
//		}
//
//		public virtual string UnitSaveObjectList
//		{
//			get
//			{
//				StringBuilder sb = new StringBuilder();
//				sb.Append(this.LIST_START);
//				sb.Append(this.CURRENT_PLAYER + ":" + this.currentPlayer.Name);
//				sb.Append(this.LIST_END + this.LIST_START);
//    
//				foreach (UnitSaveObject elem in saveParts)
//				{
//					sb.Append(elem.Character.toSaveString() + this.MEM_SEP + elem.Coordinate.toSaveString() + this.UNIT_SEP);
//				}
//				string tmp = sb.Substring(0, sb.Length - 1);
//				tmp += this.LIST_END;
//				return tmp;
//			}
//		}
//
//		public virtual Coordinate getCoordinateOfElementWithId(int id)
//		{
//			return getElementById(id).Coordinate;
//		}
//		public virtual MovableObject getCharacterOfElementWithId(int id)
//		{
//			return getElementById(id).Character;
//		}
//		private UnitSaveObject getElementById(int id)
//		{
//			UnitSaveObject elem = null;
//			for (int i = 0 ; i < saveParts.Count ; i++)
//			{
//				if (id == saveParts.get(i).Coordinate.Id)
//				{
//					elem = saveParts.get(i);
//				}
//			}
//			return elem;
//		}
//
//		public virtual bool isIndexOfSavedElement(int idx)
//		{
//			bool present = false;
//			for (int i = 0 ; i < saveParts.Count ; i++)
//			{
//				if (idx == saveParts.get(i).Coordinate.Id)
//				{
//					present = true;
//				}
//			}
//			return present;
//		}
//
//		public virtual bool isSavedElementAtCoordinate(Coordinate coor)
//		{
//			bool exists = false;
//			for (int i = 0 ; i < saveParts.Count ; i++)
//			{
//				if (saveParts.get(i).Coordinate.Equals(coor))
//				{
//					exists = true;
//				}
//			}
//			return exists;
//		}
//
//		public virtual Player CurrentPlayer
//		{
//			get
//			{
//				return currentPlayer;
//			}
//		}
//
//		public class UnitSaveObject
//		{
//			private readonly SaveGameObject outerInstance;
//
//			internal readonly Coordinate coordinate;
//			internal MovableObject character; //        private boolean highlight;
//			public UnitSaveObject(SaveGameObject outerInstance, BoardComponent bc)
//			{
//				this.outerInstance = outerInstance;
//				this.coordinate = bc.Coordinate;
//				this.character = bc.Character;
//			}
//
//			public virtual MovableObject Character
//			{
//				get
//				{
//					return character;
//				}
//				set
//				{
//					this.character = value;
//				}
//			}
//			public virtual Coordinate Coordinate
//			{
//				get
//				{
//					return this.coordinate;
//				}
//			}
//		}
//	}
//
//}