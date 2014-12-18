using UnityEngine;
using System.Collections;

public class FindAStarvingChild : RandomEventManager 
{
	public void PlayEvent()
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
		m_EventText.text = "You come accross a starving child. Its teary eyes are too sad for you to ignore and you give them " + m_MoneyLost.ToString() + " shillings.";
		//pop up window
	}
	//
}
