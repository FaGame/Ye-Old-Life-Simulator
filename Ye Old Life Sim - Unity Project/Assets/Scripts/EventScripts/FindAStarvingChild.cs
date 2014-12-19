using UnityEngine;
using System.Collections;

public class FindAStarvingChild : RandomEventManager 
{
	public string PlayEvent(PlayerData pData, string tData)
	{
		
		if(GetComponent<PlayerData>().m_Shillings >= 50)
		{
			m_MoneyLost = 50;
		}
		else
		{
			m_MoneyLost = GetComponent<PlayerData>().m_Shillings;
		}
		GetComponent<PlayerData>().m_Shillings -= m_MoneyLost;
		tData = "You come accross a starving child. Its teary eyes are too sad for you to ignore and you give them " + m_MoneyLost.ToString() + " shillings.";
		return tData;
		//pop up window
	}
	//
}
