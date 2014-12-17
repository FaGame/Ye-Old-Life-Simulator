using UnityEngine;
using System.Collections;

public class Fire : RandomEventManager
{
	public void PlayEvent()
	{

		if (GetComponent<PlayerData>().m_Shillings >= 500)
		{
			m_MoneyLost = 500;
		}
		else
		{
			m_MoneyLost = GetComponent<PlayerData>().m_Shillings;
		}
		m_EventText.text = "YOUR HOUSE CAUGHT FIRE. You lost all you food and  " + m_MoneyLost.ToString() + " shillings.";
		//pop up window
	}
}
