using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

//Come back when item list exists
public class CircusComesToTown : RandomEventManager 
{
	private string m_TicketCostMessage;
	public string m_EventMessage;

	public int m_TicketCost = 100;

	public void PlayEvent()
	{
		//pop up window
		//Check the player's current Shilling count
		if(GetComponentInChildren<PlayerData>().m_Shillings <= 100)
		{
			m_TicketCostMessage = "Not enough money eh? That's fine, we'll lend ya the cash.";
		}
		else
		{
			m_TicketCostMessage = "100 Shillings for your ticket.";
		}

		GetComponentInChildren<PlayerData>().m_Shillings -= 100; //If the player doesn't have enough money then they go into debt and have a negative amount of money
		m_EventText.text = "The circus has come to town, lose " + m_TicketCost.ToString() + " shillings. No ifs ands or buts. " + m_TicketCostMessage;

		//give player a random item
		
		//changes the player speed talk to Brandon
	}
}
