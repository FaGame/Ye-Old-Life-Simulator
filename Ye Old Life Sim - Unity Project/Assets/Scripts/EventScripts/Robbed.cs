using UnityEngine;
using System.Collections;

public class Robbed : RandomEventManager
{

	public string PlayEvent(PlayerData pData, string tData)
	{
		m_MoneyLost = Random.Range(5, 501);

		if (pData.m_Shillings >= 500)
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
	//
	
}
