using UnityEngine;
using System.Collections;

public class FindASack : RandomEventManager 
{
	public GameObject[] m_ItemsToBeFound;
	public GameObject[] m_Elixirs;
	public GameObject[] m_Food;
	public GameObject[] m_Drink;
	public GameObject[] m_ReputationItems;

	private int RandomNumber_;
	public GameObject m_FoundItem;

	private string ItemName_;

	public void PlayEvent()
	{
		//pop up window
		//You found a sack, after checking inside you find...
		RandomNumber_ = Random.Range(1, 101); //replace 1001 with the max number of items
		if(RandomNumber_ > 11)
		{
			int ElixirGiven = Random.Range(1,5);
			m_FoundItem = m_Elixirs[ElixirGiven];
			
		}
		else if(RandomNumber_ >= 50)
		{
			int FoodGiven = Random.Range(1, 11);
			m_FoundItem = m_Food[FoodGiven];
		}
		else if(RandomNumber_ >= 60)
		{
			int DrinkGiven = Random.Range(1, 4);
			m_FoundItem = m_Drink[DrinkGiven];
		}
		else if(RandomNumber_ >=100)
		{
			int ReputationItemGiven = Random.Range(1, 11);
			m_FoundItem = m_ReputationItems[ReputationItemGiven];
		}
		ItemName_ = m_FoundItem.ToString();

		//GetComponent<PossessionInventory>().AddToInventory(ItemName_, m_FoundItem);
		//public void AddToInventory(string name, ItemInventoryEntry item)
		//m_FoundItem
		//!
		//add m_FoundItem to inventory
	}
}
