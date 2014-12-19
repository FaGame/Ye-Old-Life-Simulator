using UnityEngine;
using System.Collections;

public class ThePlague : RandomEventManager
{
	public string PlayEvent(PlayerData pData, string tData)
	{
		GetComponent<PlayerData>().m_IsInfected = true;

		tData = "You've caught the plague.";
		return tData;
	}
	//
}
