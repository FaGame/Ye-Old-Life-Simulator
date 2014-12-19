using UnityEngine;
using System.Collections;

public class BitByRats : RandomEventManager 
{
	public override string PlayEvent(PlayerData pData, string tData)
	{
		m_TimeChange = ValueConstants.TIME_CHANGE_FROM_RAT_BITES;
		//pop up window
		tData = "Woken up in the middle of the night by rats biting you. Looks like a late start when you get up in the morning.";
		GetComponentInChildren<PlayerData>().m_CurrTime += m_TimeChange;
		return tData;
		//
	}

	public override string UpdateEvent(PlayerData m_Player, string m_EventDesc)
	{
		throw new System.NotImplementedException();
	}
}
