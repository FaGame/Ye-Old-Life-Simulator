using UnityEngine;
using System.Collections;

public class FoundShillings : RandomEventManager 
{
    public override string PlayEvent(PlayerData pData, string tData)
    {
		m_MoneyGained = Random.Range(ValueConstants.MIN_SHILLINGS_FOUND, ValueConstants.MAX_SHILLINGS_FOUND); //Amount of money the player finds

		pData.m_Shillings += m_MoneyGained; //give the player the money they found

		//could add if statements in here to change the message depending on the amount of money found

		//pop up window
		tData = "You find a dead beggar and search his person for belongings. You found " + m_MoneyGained.ToString() +
							" shillings! You shake the man's hand and go off on your business.";
		//return tData;
		return	tData;
    }

	public override string UpdateEvent(PlayerData m_Player, string m_EventDesc)
	{
		throw new System.NotImplementedException();
	}
	//

}
