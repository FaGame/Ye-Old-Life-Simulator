﻿using UnityEngine;
using System.Collections;

public class Robbed : RandomEventManager
{

	public override string PlayEvent(PlayerData pData, string tData)
	{
		m_MoneyLost = Random.Range(ValueConstants.MIN_SHILLINGS_ROBBER_TAKES, ValueConstants.MAX_SHILLINGS_ROBBER_TAKES);

		if (pData.m_Shillings >= ValueConstants.MAX_SHILLINGS_ROBBER_TAKES)
		{
			pData.m_Shillings -= m_MoneyLost;
		}
		else
		{
			m_MoneyLost = pData.m_Shillings;
			pData.m_Shillings -= m_MoneyLost;
		}
		tData = "You are mugged on your way out today, the robber takes " + m_MoneyLost.ToString() + " shillings and tips his hat to you as he prances away.";
		return tData;
	}

	public override string UpdateEvent(PlayerData m_Player, string m_EventDesc)
	{
		throw new System.NotImplementedException();
	}
	//
	
}
