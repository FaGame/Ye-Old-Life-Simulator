using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

//Come back when item list exists
public class CircusComesToTown : RandomEventManager 
{
	private string m_TicketCostMessage;
	public string m_EventMessage;

	
	private int RandomNumber_;

	public GameObject m_FoundItem;
	public GameObject[] m_Items;

	public void PlayEvent()
	{
		m_MoneyLost = 100;

		//pop up window
		//Check the player's current Shilling count
		if(GetComponentInChildren<PlayerData>().m_Shillings <= m_MoneyLost)
		{
			m_TicketCostMessage = "Hand over yer money, and here's your ticket.";
			m_MoneyLost = GetComponentInChildren<PlayerData>().m_Shillings;
		}
		else
		{
			m_TicketCostMessage = "100 Shillings for your ticket.";
		}

		GetComponentInChildren<PlayerData>().m_Shillings -= m_MoneyLost; //If the player doesn't have enough money then they go into debt and have a negative amount of money
		

		//give player a random item
		RandomNumber_ = Random.Range(1, 28); //Select a random category of items, and then an item from that list
		if (RandomNumber_ > 11)
		{
			int ItemGiven = Random.Range(1, m_Items.Length - 1);
			m_FoundItem = m_Items[ItemGiven];
			//	m_ItemType(Elixir);

		}
		//GetComponent<PossessionInventory>().AddToInventory()
//		GetComponent<PossessionInventory>().AddToInventory(m_FoundItem.name, m_ItemType); //add the found item into the player's inventory
		//If someone else wants to make the above line work, be my guest.

		m_EventText.text = "The circus has come to town, lose " + m_MoneyLost.ToString() + " shillings. No ifs ands or buts. " + m_TicketCostMessage
				+ " On the bright side, you ended up winning " + m_FoundItem.name.ToString() + ".";

	}
}
