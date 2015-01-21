using UnityEngine;
using System.Collections;

public class Tournament : RandomEventBaseClass
{
	private int MaxLostMoney_;

	public override string PlayEvent(PlayerData pData, string tData)
	{
		if (pData.m_Shillings < ValueConstants.TOURNAMENT_COST_MAX)
		{
			MaxLostMoney_ = pData.m_Shillings;
		}

		m_MoneyLost = Random.Range(ValueConstants.TOURNAMENT_COST_MIN, MaxLostMoney_);

		if(m_MoneyLost > pData.m_Shillings)
		{
			if (pData.m_Shillings <= 0)
			{
				tData = "You go to see a tournament, but with no money you decide to sneak in. It was alright. Except for the mud splattered all over your clothes.";
				return tData;
			}
			m_MoneyLost = pData.m_Shillings;
			tData = "You go to see a tournament, the ticket was " + m_MoneyLost.ToString() + " shillings. But you had a good time.";
			return tData;
		}

		pData.m_Shillings -= m_MoneyLost;
		tData = "You go to see a tournament, the ticket was " + m_MoneyLost.ToString() + " shillings. But you had a great time.";
		return tData;
	}

	//
}
