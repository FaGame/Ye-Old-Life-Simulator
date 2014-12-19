using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class ExperimentGoneWrong : RandomEventManager 
{
	private string NameChecker_;

	private Item ItemToBeRemoved_;

	public int[] m_ItemsInInventory;
	public UseableItemInventory m_UseableInventory;

	public override string PlayEvent(PlayerData pData, string tData)
	{
		//Check player's current career
		NameChecker_ = GetComponent<PlayerData>().m_Job.ToString();
		if(NameChecker_ != "Alchemist") //if Alchemist then nothing happens
		{
			int randomNumber = Random.Range(1, m_ItemsInInventory.Length - 1);

			int itemIndex = m_ItemsInInventory[randomNumber];

			if (m_UseableInventory != null)
			{
				foreach (KeyValuePair<string, UseableItemInventory.ItemInventoryEntry> entry in m_UseableInventory.m_UseableItemInventory)
				{
					if (entry.Value.item is Item)
					{
						GetComponent<UseableItemInventory>().RemoveFromInventory(ItemToBeRemoved_.name.ToString());
					}
					tData = "As the result of a terrible experiment you've lost " + ItemToBeRemoved_.name + ", such a shame.";
					
				}
			}
		}
		else//if not an alchemist, remove random item from player's inventory
		{
			tData = "One of your experiments has gone horribly wrong! It could have gone a lot worse if you didn't make that antidote already.";
		}
		return tData;
	}

	public override string UpdateEvent(PlayerData m_Player, string m_EventDesc)
	{
		throw new System.NotImplementedException();
	}
	//

}
