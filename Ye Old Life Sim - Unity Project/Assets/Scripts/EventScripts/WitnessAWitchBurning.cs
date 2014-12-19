using UnityEngine;
using System.Collections;

public class WitnessAWitchBurning : RandomEventManager
{
	private string FeelingTowardWitch_;

	public string PlayEvent(PlayerData pData, string tData)
	{
		//The player loses 10 seconds on the clock but gain 5 - 20 happiness seeing justice being carried out.
		m_TimeChange = -10.0f;
		m_HappinesChange = Random.Range(5, 21);

		if (m_HappinesChange > 15)
		{
			FeelingTowardWitch_ = "BURN B-WITCH BURN.";
		}
		else if (m_HappinesChange > 5)
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
	//
}
