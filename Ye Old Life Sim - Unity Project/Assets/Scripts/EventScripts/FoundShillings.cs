using UnityEngine;
using System.Collections;

public class FoundShillings : RandomEventManager 
{
    public void PlayEvent()
    {
		m_MoneyGained = Random.Range(5, 1001); //Amount of money the player finds

		//pop up window
		m_EventText.text = "You find a dead beggar and search his person for belongings. You found " + m_MoneyGained.ToString() + 
							" shillings! You shake the man's hand and go off on your business.";
		//get current Shillings and then give the player the amount of money found
		GetComponent<PlayerData>().m_Shillings += m_MoneyGained;

    }

}
