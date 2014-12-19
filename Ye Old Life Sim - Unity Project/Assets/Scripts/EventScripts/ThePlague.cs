using UnityEngine;
using System.Collections;

public class ThePlague : RandomEventManager
{
	public override string PlayEvent(PlayerData pData, string tData)
	{
		GetComponent<PlayerData>().m_IsInfected = true;

		tData = "You've caught the plague.";
		return tData;
	}
	public override string UpdateEvent(PlayerData m_Player, string m_EventDesc)
	{
		throw new System.NotImplementedException();
	}
	//
}
