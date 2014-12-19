using UnityEngine;
using System.Collections;

public class FoundShillings : RandomEventManager 
{
    public string PlayEvent(PlayerData pData, string tData)
    {
		m_MoneyGained = Random.Range(5, 1001); //Amount of money the player finds
	
		//get current Shillings and then give the player the amount of money found
//		GetComponent<PlayerData>().m_Shillings += m_MoneyGained;
		pData.m_Shillings += m_MoneyGained;

		//could add if statements in here to change the message depending on the amount of money found

		//pop up window
		tData = "You find a dead beggar and search his person for belongings. You found " + m_MoneyGained.ToString() +
							" shillings! You shake the man's hand and go off on your business.";
		//return tData;
		return	tData;
    }
	//

}
