// ----------------------------------------------------------------------------
// <copyright file="CustomTypes.cs" company="Exit Games GmbH">
//   PhotonNetwork Framework for Unity - Copyright (C) 2011 Exit Games GmbH
// </copyright>
// <summary>
//   
// </summary>
// <author>developer@exitgames.com</author>
// ----------------------------------------------------------------------------
using System;
using System.IO;
using ExitGames.Client.Photon;
using UnityEngine;
using com.cosmichorizons.basecomponents;
using com.cosmichorizons.enums;
using com.cosmichorizons.characters;

using Hashtable = ExitGames.Client.Photon.Hashtable;

public static class CustomTypes
{
	
	public static void Register()
	{
		PhotonPeer.RegisterType(typeof(Vector2), (byte)'A', SerializeVector2, DeserializeVector2);
		PhotonPeer.RegisterType(typeof(Vector3), (byte)'B', SerializeVector3, DeserializeVector3);
		PhotonPeer.RegisterType(typeof(Transform), (byte)'C', SerializeTransform, DeserializeTransform);
		PhotonPeer.RegisterType(typeof(Quaternion), (byte)'D', SerializeQuaternion, DeserializeQuaternion);
		PhotonPeer.RegisterType(typeof(BoardComponent), (byte)'E', SerializeBoardComponent, DeserializeBoardComponent);
		PhotonPeer.RegisterType(typeof(Coordinate), (byte)'F', SerializeCoordinate, DeserializeCoordinate);
		PhotonPeer.RegisterType(typeof(MovableObject), (byte)'G', SerializeMovableObject, DeserializeMovableObject);

		//TODO find out why i had to do this
		PhotonPeer.RegisterType(typeof(EmptyObject), (byte)'H', SerializeMovableObject, DeserializeMovableObject);
		PhotonPeer.RegisterType(typeof(OneUnit), (byte)'I', SerializeMovableObject, DeserializeMovableObject);
		PhotonPeer.RegisterType(typeof(TwoUnit), (byte)'J', SerializeMovableObject, DeserializeMovableObject);
		PhotonPeer.RegisterType(typeof(ThreeUnit), (byte)'K', SerializeMovableObject, DeserializeMovableObject);
		PhotonPeer.RegisterType(typeof(TaichoUnit), (byte)'L', SerializeMovableObject, DeserializeMovableObject);
	}
	
	#region Custom De/Serializer Methods

	private static byte[] SerializeCoordinate(object o) {
		Coordinate coor = (Coordinate) o;

		byte [] bytes = new byte[12];
		int index = 0;
		byte [] xBytes = BitConverter.GetBytes(coor.PosX);
		System.Array.Copy(xBytes, 0, bytes, index, xBytes.Length);
		index = xBytes.Length;
		byte [] yBytes = BitConverter.GetBytes(coor.PosY);
		System.Array.Copy(yBytes, 0, bytes, index, yBytes.Length);
		index += yBytes.Length;
		byte [] idBytes = BitConverter.GetBytes(coor.Id);
		System.Array.Copy(idBytes, 0, bytes, index, idBytes.Length);
		return bytes;
	}

	static byte[] SliceMe(byte[] source, int startIndex, int length) {
		byte[] destfoo = new byte[length];
		Array.Copy(source, startIndex, destfoo, 0, length);
		return destfoo;
	}

	private static object DeserializeCoordinate(byte[] bytes)
	{
		Coordinate coor = new Coordinate();
		coor.PosX = BitConverter.ToInt32(bytes, 0);
		coor.PosY = BitConverter.ToInt32(bytes, 4);
		coor.Id = BitConverter.ToInt32(bytes, 8);
//		Debug.Log("Deserialized Coor :::::: coor.PosX - " + coor.PosX + " -- coor.PosY - " + coor.PosY + coor.Id );
		
		return coor;
	}

	private static int getPlayerIntValue(Player p) {
//		Debug.Log("get Val of Player -- ["+p+"]");
		switch(p) {
		case Player.NONE:
			return 0;
		case Player.PLAYER_ONE:
			return 1;
		case Player.PLAYER_TWO:
			return 2;
		default:
			return 0;
		}
	}

	private static int getRankIntValue(Ranks r) {
//		Debug.Log("get Val of Rank -- ["+r+"]");
		switch(r) {
		case Ranks.NONE:
			return -1;
		case Ranks.LEVEL_ONE:
			return 1;
		case Ranks.LEVEL_TWO:
			return 2;
		case Ranks.LEVEL_THREE:
			return 3;
		case Ranks.TAICHO:
			return 4;
		default:
			return -1;
		}
	}

	private static Player getPlayerFromFloatValue(int val) {
//		Debug.Log("getPlayerFromFloatValue -- ["+val+"]");
		if (val == 0)
			return Player.NONE;
		else if (val == 1)
			return Player.PLAYER_ONE;
		else if (val == 2)
			return Player.PLAYER_TWO;
		else
			return Player.NONE;

	}
	
	private static Ranks getRankFromFloatValue(int val) {
//		Debug.Log("getRankFromFloatValue -- ["+val+"]");
		if (val == 1)
			return Ranks.LEVEL_ONE;
		else if (val == 2)
			return Ranks.LEVEL_TWO;
		else if (val == 3)
			return Ranks.LEVEL_THREE;
		else if (val == 4)
			return Ranks.TAICHO;
		else
			return Ranks.NONE;
	}

	private static byte[] SerializeMovableObject(object o) {
		MovableObject mo = (MovableObject) o;

		byte [] bytes = new byte[8];
		int index = 0;
		byte [] playerBytes = BitConverter.GetBytes(getPlayerIntValue(mo.Player));
		System.Array.Copy(playerBytes, 0, bytes, index, playerBytes.Length);
		index = playerBytes.Length;
		byte [] rankBytes = BitConverter.GetBytes(getRankIntValue(mo.Rank));
		System.Array.Copy(rankBytes, 0, bytes, index, rankBytes.Length);
		return bytes;
	}
	
	private static object DeserializeMovableObject(byte[] bytes)
	{
		EmptyObject eo = new EmptyObject();
		eo.Player = getPlayerFromFloatValue( BitConverter.ToInt32(bytes, 0) );
		eo.Rank = getRankFromFloatValue( BitConverter.ToInt32(bytes, 4) );
//		Debug.Log("Deserialized MovableObject -- Player["+eo.Player+"]  --- Rank["+eo.Rank+"]");

		return eo;
	}

	private static byte[] SerializeBoardComponent(object o) {
		BoardComponent boardComponent = (BoardComponent) o;

		byte [] bytes = new byte [20];
		int index = 0;
		byte [] characterBytes = SerializeMovableObject(boardComponent.Character);//Protocol.Serialize(boardComponent.Character);
//		Debug.Log ("character bytes length :: " + characterBytes.Length + " - " + index);
		System.Array.Copy(characterBytes, 0, bytes, index, characterBytes.Length);
		index = characterBytes.Length;
		byte [] coorBytes = SerializeCoordinate(boardComponent.Coordinate);// Protocol.Serialize(boardComponent.Coordinate);
//		Debug.Log ("coordinates bytes length :: " + coorBytes.Length + " - " + index);
		System.Array.Copy(coorBytes, 0, bytes, index, coorBytes.Length);

		return bytes;
	}

	private static object DeserializeBoardComponent(byte[] serializedcustomobject)
	{
		object characterObj = DeserializeMovableObject(SliceMe(serializedcustomobject, 0, 8));
//		Debug.Log ("character :: " + characterObj + " - ");
		object coordinateObj = DeserializeCoordinate(SliceMe(serializedcustomobject, 8, 12));
//		Debug.Log ("coordinates :: " + coordinateObj + " - ");
		Coordinate c = (Coordinate) coordinateObj;
		MovableObject cc = (MovableObject) characterObj;
		BoardComponent bc = new BoardComponent(cc, Location.GAME_BOARD, c);
		return bc;
	}
	
	private static byte[] SerializeTransform(object customobject)
	{
		Transform t = (Transform)customobject;
		
		Vector3[] parts = new Vector3[2];
		parts[0] = t.position;
		parts[1] = t.eulerAngles;
		
		return Protocol.Serialize(parts);
	}
	
	private static object DeserializeTransform(byte[] serializedcustomobject)
	{
		object x = Protocol.Deserialize(serializedcustomobject);
		return x;
	}
	
	private static byte[] SerializeVector3(object customobject)
	{
		Vector3 vo = (Vector3)customobject;
		MemoryStream ms = new MemoryStream(3 * 4);
		
		ms.Write(BitConverter.GetBytes(vo.x), 0, 4);
		ms.Write(BitConverter.GetBytes(vo.y), 0, 4);
		ms.Write(BitConverter.GetBytes(vo.z), 0, 4);
		return ms.ToArray();
	}
	
	private static object DeserializeVector3(byte[] bytes)
	{
		Vector3 vo = new Vector3();
		vo.x = BitConverter.ToSingle(bytes, 0);
		vo.y = BitConverter.ToSingle(bytes, 4);
		vo.z = BitConverter.ToSingle(bytes, 8);
		
		return vo;
	}
	
	private static byte[] SerializeVector2(object customobject)
	{
		Vector2 vo = (Vector2)customobject;
		MemoryStream ms = new MemoryStream(2 * 4);
		
		ms.Write(BitConverter.GetBytes(vo.x), 0, 4);
		ms.Write(BitConverter.GetBytes(vo.y), 0, 4);
		return ms.ToArray();
	}
	
	private static object DeserializeVector2(byte[] bytes)
	{
		Vector2 vo = new Vector2();
		vo.x = BitConverter.ToSingle(bytes, 0);
		vo.y = BitConverter.ToSingle(bytes, 4);
		return vo;
	}
	public static byte[] SerializeQuaternion(object obj)
	{
		Quaternion o = (Quaternion)obj;
		MemoryStream ms = new MemoryStream(3 * 4);
		
		ms.Write(BitConverter.GetBytes(o.w), 0, 4);
		ms.Write(BitConverter.GetBytes(o.x), 0, 4);
		ms.Write(BitConverter.GetBytes(o.y), 0, 4);
		ms.Write(BitConverter.GetBytes(o.z), 0, 4);
		return ms.ToArray();
	}
	
	public static object DeserializeQuaternion(byte[] bytes)
	{
		Quaternion o = new Quaternion();
		o.w = BitConverter.ToSingle(bytes, 0);
		o.x = BitConverter.ToSingle(bytes, 4);
		o.y = BitConverter.ToSingle(bytes, 8);
		o.z = BitConverter.ToSingle(bytes, 12);
		
		return o;
	}
	
	#endregion
}