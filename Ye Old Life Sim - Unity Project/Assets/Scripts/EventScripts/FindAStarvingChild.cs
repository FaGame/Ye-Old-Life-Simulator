using UnityEngine;
using System.Collections;

public class FindAStarvingChild : RandomEventManager 
{
	public override string PlayEvent(PlayerData pData, string tData)
	{
		
		if(GetComponent<PlayerData>().m_Shillings >= ValueConstants.MONEY_GIVEN_TO_STARVING_CHILD)
		{
			m_MoneyLost = ValueConstants.MONEY_GIVEN_TO_STARVING_CHILD;
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

	public override string UpdateEvent(PlayerData m_Player, string m_EventDesc)
	{
		throw new System.NotImplementedException();
	}
	//
}
