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

		pData.m_Shillings -= m_MoneyLost;
		tData = "You go to see a tournament, the ticket was " + m_MoneyLost.ToString() + " shillings. But you had a great time.";
		return tData;
	}
	public override string UpdateEvent(PlayerData m_Player, string m_EventDesc)
	{
		throw new System.NotImplementedException();
	}
	//
}
