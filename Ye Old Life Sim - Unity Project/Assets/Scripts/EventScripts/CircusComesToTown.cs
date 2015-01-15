using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

//Come back when item list exists
public class CircusComesToTown : RandomEventBaseClass
{
	public RandomEventManager1 m_RandomEventManager;

	Item[] m_Items = new Item[0];
	public Item m_ItemType;

	private string m_TicketCostMessage;

	public GameObject m_FoundItem;
//	public GameObject[] m_Items;
	public AnItem m_AnItemScript;
	public PlayerData m_PlayerData;

	public override string PlayEvent(PlayerData pData, string tData)
	{
	/*	m_MoneyLost = ValueConstants.DEFAULT_CIRCUS_TICKET_COST;

		//pop up window
		//Check the player's current Shilling count
		if(pData.m_Shillings <= m_MoneyLost)
		{
			m_TicketCostMessage = "Hand over yer money, and here's your ticket.";
			m_MoneyLost = pData.m_Shillings;
		}
		else
		{
			m_TicketCostMessage = "100 Shillings for your ticket.";
		}

		pData.m_Shillings -= m_MoneyLost; //If the player doesn't have enough money then they go into debt and have a negative amount of money
		

		
		int randomNumber_ = Random.Range(1, m_Items.Length - 1); //Select a random category of items, and then an item from that list

		if (randomNumber_ > 11)
		{
			int ItemGiven = Random.Range(1, m_Items.Length - 1);
//			m_FoundItem = m_Items[ItemGiven];
		}
		//m_PlayerData.m_UseableInventory.AddToInventory(m_FoundItem.name, m_AnItemScript.GetComponent<AnItem>().m_SingleItem); //give player the random item
//		GetComponent<PossessionInventory>().AddToInventory(m_FoundItem.name, m_ItemType); //add the found item into the player's inventory

		tData = "The circus has come to town, lose " + m_MoneyLost.ToString() + " shillings. No ifs ands or buts. " + m_TicketCostMessage
				+ " On the bright side, you ended up winning " + m_FoundItem.name.ToString() + ".";
		*/
		tData = "Not implemented";
		return tData; 
	}

	public override string UpdateEvent(PlayerData m_Player, string m_EventDesc)
	{
		throw new System.NotImplementedException();
	}
}
