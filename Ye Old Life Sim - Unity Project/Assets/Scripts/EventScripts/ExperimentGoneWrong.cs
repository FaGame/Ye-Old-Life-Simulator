using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class ExperimentGoneWrong : RandomEventManager 
{
	private string NameChecker_;
	private int RandomNumber_;

	private Item ItemToBeRemoved_;
	private int ItemIndex_;

	public int[] m_ItemsInInventory;
	public UseableItemInventory m_UseableInventory;

	public void PlayEvent()
	{
		//Check player's current career
		NameChecker_ = GetComponent<PlayerData>().m_Job.ToString();
		if(NameChecker_ == "Alchemist") //if Alchemist then nothing happens
		{
			return;
		}
		else//if not an alchemist, remove random item from player's inventory
		{
			RandomNumber_ = Random.Range(1, m_ItemsInInventory.Length - 1);

			ItemIndex_ = m_ItemsInInventory[RandomNumber_];

			if (m_UseableInventory != null)
			{
				foreach (KeyValuePair<string, UseableItemInventory.ItemInventoryEntry> entry in m_UseableInventory.m_UseableItemInventory)
				{
					if (entry.Value.item is Item)
					{
						GetComponent<UseableItemInventory>().RemoveFromInventory(ItemToBeRemoved_.name.ToString());
					}
					return;
				}
			}
		}
	}
	//

}
