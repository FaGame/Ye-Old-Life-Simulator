using UnityEngine;
using System.Collections;

public class WitnessAWitchBurning : RandomEventManager
{
	private string FeelingTowardWitch_;

	public override string PlayEvent(PlayerData pData, string tData)
	{
		//The player loses 10 seconds on the clock but gain 5 - 20 happiness seeing justice being carried out.
		m_TimeChange = -10.0f;
		m_HappinesChange = Random.Range(ValueConstants.WITCH_BURNING_HAPPINESS_HAPPY_CHANGE_MIN, ValueConstants.WITCH_BURNING_HAPPINESS_HAPPY_CHANGE_MAX);

		if (m_HappinesChange > ValueConstants.BURNING_MESSAGE_HAPPY)
		{
			FeelingTowardWitch_ = "BURN B-WITCH BURN.";
		}
		else if (m_HappinesChange > ValueConstants.BURNING_MESSAGE_ALRIGHT)
		{
			FeelingTowardWitch_ = "It's nice to see the government getting justice done right.";
		}
		else
		{
			FeelingTowardWitch_ = "Burn baby burn.";
		}

		GetComponent<PlayerData>().m_Happiness += m_HappinesChange;
		GetComponent<PlayerData>().m_CurrTime += m_TimeChange;
		tData = "A witch has been found! They're burning her and all you can say is: " + FeelingTowardWitch_;
		return tData;
	}
	public override string Update(PlayerData m_Player, string m_EventDesc)
	{
		throw new System.NotImplementedException();
	}
	//
}
