using UnityEngine;
using System.Collections;

public class Robbed : RandomEventManager
{

	public void PlayEvent()
	{
		m_MoneyLost = Random.Range(5, 501);
		if (GetComponent<PlayerData>().m_Shillings >= 501)
		{
			return;
		}
		else
		{
			m_MoneyLost = GetComponent<PlayerData>().m_Shillings;
		}
		GetComponent<PlayerData>().m_Shillings -= m_MoneyLost;
		m_EventText.text = "You are muged on your way out today, the robber takes " + m_MoneyLost.ToString() + " shillings and tips his hat to you as he prances away.";
	}
	//
	
}
