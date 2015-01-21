using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class ExperimentGoneWrong : RandomEventBaseClass
{
	private string NameChecker_;

	private Item ItemToBeRemoved_;

	public int[] m_ItemsInInventory;
	public UseableItemInventory m_UseableInventory;
	public GameObject m_InventoryData;

	public bool m_P1Turn;
	public bool m_P2Turn;
	public bool m_AITurn;


	private string NotAlchemist(string tempTData)
	{
		m_P1Turn = GameObject.Find("GameManager Holder").GetComponent<GameManager>().m_RandomEventP1Play;
		m_P2Turn = GameObject.Find("GameManager Holder").GetComponent<GameManager>().m_RandomEventP2Play;
		m_AITurn = GameObject.Find("GameManager Holder").GetComponent<GameManager>().m_RandomEventAIPlay;

		if (m_P1Turn == true)
		{
//			m_InventoryData = GameObject.Find("Char_2").GetComponent<PlayerData>().m_UseableInventory[0];
			if (m_UseableInventory != null)
			{
				foreach (KeyValuePair<string, Item.ItemInventoryEntry> entry in m_UseableInventory.m_UseableItemInventory)
				{
					if (entry.Value.item is Item)
					{
						m_InventoryData.GetComponent<UseableItemInventory>().RemoveFromInventory(ItemToBeRemoved_.name.ToString());
					}
					tempTData = "As the result of a terrible experiment you've lost " + ItemToBeRemoved_.name + ", such a shame.";
				}
			}
		}
		else if(m_P2Turn)
		{
			m_InventoryData = GameObject.Find("Player Two");
		}
		else if(m_AITurn)
		{
			m_InventoryData = GameObject.Find("Player Two"); //Should be the AI
		}
		else
		{
			tempTData = "Something is wrong, the current turn's 'player' could not be found";
		}

		int randomNumber = Random.Range(1, m_ItemsInInventory.Length - 1);
		if(randomNumber <= 0)
		{
			tempTData = "No items to lose from the bad experiment.";
		}

		int itemIndex = m_ItemsInInventory[randomNumber];


		return tempTData;
	}
	public override string PlayEvent(PlayerData pData, string tData)
	{
		//Check player's current career
		if (pData.m_Job != null)
		{
			NameChecker_ = pData.m_Job.ToString();
			if (NameChecker_ != "Alchemist") //if Alchemist then nothing happens
			{
//				NotAlchemist(tData);
				return tData;
			}
			else//if not an alchemist, remove random item from player's inventory
			{
				tData = "One of your experiments has gone horribly wrong! It could have gone a lot worse if you didn't make that antidote already.";
			}			
		}
//		NotAlchemist(tData);

		tData = "Currently No experiment can ruin your items. This is because it can't get access to your inventory.";

		return tData;
	}


	//

}
