using ExitGames.Client.Photon;
using ExitGames.Client.Photon.LoadBalancing;

using UnityEngine;
using System.Collections;
using Hashtable = ExitGames.Client.Photon.Hashtable;

public class NetworkTaichoPlayer : Player {

	protected internal NetworkTaichoPlayer(string name, int actorID, bool isLocal, Hashtable actorProperties) : base(name, actorID, isLocal, actorProperties)
	{
	}
	
	public override string ToString()
	{
		return base.ToString() + ((this.IsInactive) ? " (inactive)" : "");
	}
}
