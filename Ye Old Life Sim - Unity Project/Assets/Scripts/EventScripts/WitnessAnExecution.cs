using UnityEngine;
using System.Collections;

public class WitnessAnExecution : RandomEventManager
{
	private string FeelingTowardHead_;

	public override string PlayEvent(PlayerData pData, string tData)
	{
		//The player loses 5 seconds on the clock but gain -5 - 10 happiness based upon how much you liked the person.
		m_TimeChange = ValueConstants.TIME_CHANGE_FROM_EXECUTION;
		m_HappinesChange = Random.Range(-5, 11);

		if(m_HappinesChange > ValueConstants.EXECUTION_MESSAGE_HAPPY)
		{
			FeelingTowardHead_ = "You're really pleased that guy is dead.";
		}
		else if(m_HappinesChange > ValueConstants.EXECUTION_MESSAGE_ALRIGHT)
		{
			FeelingTowardHead_ = "That guy was akind of a dick anyway.";
		}
		else
		{
			FeelingTowardHead_ = "No! Not THAT guy! You really liked that guy! Right? ... Yeah. You're less happy now.";
		}

		GetComponent<PlayerData>().m_Happiness += m_HappinesChange;
		GetComponent<PlayerData>().m_CurrTime += m_TimeChange;
		tData = "There's an execution today, after watching the head roll you think " + FeelingTowardHead_;
		return tData;
	}

	public override string Update(PlayerData m_Player, string m_EventDesc)
	{
		throw new System.NotImplementedException();
	}
	//
}
