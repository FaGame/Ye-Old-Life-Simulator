using UnityEngine;
using System.Collections;

public class FoundShillings : RandomEventManager 
{
	private int MoneyFound_;

    public void PlayEvent()
    {
		MoneyFound_ = Random.Range(5, 1001); //Amount of money the player finds

		//pop up window
		m_EventText.text = "You find a dead beggar and search his person for belongings. You found " + MoneyFound_.ToString() + 
							" shillings! You shake the man's hand and go off on your business.";
		//get current Shillings and then give the player the amount of money found
		GetComponent<PlayerData>().m_Shillings += MoneyFound_;

    }

}
