using UnityEngine;
using System.Collections;

public class FindAStarvingChild : RandomEventBaseClass
{
	public override string PlayEvent(PlayerData pData, string tData)
	{
		if (pData.m_Shillings <= 0) //if the player has no money at all
		{
			tData = "You come across a starving child. With no money to spare (a.k.a no dough at all), the child kicks you in the shin and runs away crying before falling over and dying of starvation.";
		}
		else if (pData.m_Shillings >= ValueConstants.MONEY_GIVEN_TO_STARVING_CHILD) //If the player has the required amount of money 
		{
			m_MoneyLost = ValueConstants.MONEY_GIVEN_TO_STARVING_CHILD; //Player loses money equal to the amount given to the starving child
			tData = "You come across a starving child. Its teary eyes are too sad for you to ignore and you give them " + m_MoneyLost.ToString() + " shillings.";
		}
		else //if the player has money, but is less than what should be given away
		{
			m_MoneyLost = pData.m_Shillings; //Player loses all their money
			tData = "You come across a starving child. Its teary eyes are too sad for you to ignore and you give them all your shillings.";
		}

		pData.m_Shillings -= m_MoneyLost;
		return tData;
		//pop up window
	}


	//
}
