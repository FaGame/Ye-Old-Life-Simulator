using UnityEngine;
using System.Collections;

public class BitByRats : RandomEventManager 
{
	public string PlayEvent(PlayerData pData, string tData)
	{
		m_TimeChange = -10.0f;
		//pop up window
		tData = "Woken up in the middle of the night by rats biting you. Looks like a late start when you get up in the morning.";
		GetComponentInChildren<PlayerData>().m_CurrTime += m_TimeChange;
		return tData;
		//
	}
}
