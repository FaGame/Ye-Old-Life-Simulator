using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Fire : RandomEventBaseClass
{
	public UseableItemInventory m_UseableInventory;
	private Food playerFood_;

	public override string PlayEvent(PlayerData pData, string tData)
	{

		if (pData.m_Shillings >= ValueConstants.MONEY_LOST_FROM_FIRE)
		{
			m_MoneyLost = ValueConstants.MONEY_LOST_FROM_FIRE;
		}
		else
		{
			m_MoneyLost = pData.m_Shillings;
		}

		if (m_UseableInventory != null)
		{
			foreach (KeyValuePair<string, Item.ItemInventoryEntry> entry in m_UseableInventory.m_UseableItemInventory)
			{
				if (entry.Value.item is Food)
				{
					//set the food variable if the entry is a food type
					playerFood_ = (Food)entry.Value.item;
					playerFood_.RemoveFood();   //removes perishable food item
				}
			}
		}

		if(m_MoneyLost >= 0)
		{
			tData = "YOUR HOUSE CAUGHT FIRE. You lost all your food and " + m_MoneyLost.ToString() + " shillings (because you had none).";
		}
		else
		{
			tData = "YOUR HOUSE CAUGHT FIRE. You lost all your food and  " + m_MoneyLost.ToString() + " shillings.";
		}

		
		return tData;
		//pop up window
	}

	public override string UpdateEvent(PlayerData m_Player, string m_EventDesc)
	{
		throw new System.NotImplementedException();
	}
	//
}
