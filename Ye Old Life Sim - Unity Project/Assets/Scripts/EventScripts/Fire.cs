using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Fire : RandomEventManager
{
	public UseableItemInventory m_UseableInventory;
	private Food playerFood_;

	public void PlayEvent()
	{

		if (GetComponent<PlayerData>().m_Shillings >= 500)
		{
			m_MoneyLost = 500;
		}
		else
		{
			m_MoneyLost = GetComponent<PlayerData>().m_Shillings;
		}

		if (m_UseableInventory != null)
		{
			foreach (KeyValuePair<string, UseableItemInventory.ItemInventoryEntry> entry in m_UseableInventory.m_UseableItemInventory)
			{
				if (entry.Value.item is Food)
				{
					//set the food variable if the entry is a food type
					playerFood_ = (Food)entry.Value.item;
					playerFood_.RemoveFood();   //removes perishable food item
				}
			}
		}

		m_EventText.text = "YOUR HOUSE CAUGHT FIRE. You lost all your food and  " + m_MoneyLost.ToString() + " shillings.";
		//pop up window
	}
}
