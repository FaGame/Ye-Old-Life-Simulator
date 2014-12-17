using UnityEngine;
using System.Collections;

public class Tournament : RandomEventManager
{
	private int MaxLostMoney_;

	public void PlayEvent()
	{
		if(GetComponent<PlayerData>().m_Shillings < 150)
		{
			MaxLostMoney_ = GetComponent<PlayerData>().m_Shillings;
		}

		m_MoneyLost = Random.Range(50,MaxLostMoney_ );

		GetComponent<PlayerData>().m_Shillings -= m_MoneyLost;
		m_EventText.text = "You go to see a tournament, the ticket was " + m_MoneyLost.ToString() + " shillings. But you had a great time.";
	}
}
