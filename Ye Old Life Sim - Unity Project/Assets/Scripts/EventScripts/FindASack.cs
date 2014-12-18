using UnityEngine;
using System.Collections;

public class FindASack : RandomEventManager 
{
	public GameObject[] m_ItemsToBeFound;
	public GameObject[] m_Elixirs;
	public GameObject[] m_Food;
	public GameObject[] m_Drink;
	public GameObject[] m_ReputationItems;

	public Item m_ItemType;

	private int RandomNumber_;
	public GameObject m_FoundItem;

//	private string ItemName_;

	public void PlayEvent()
	{
		//pop up window
		//You found a sack, after checking inside you find...
		RandomNumber_ = Random.Range(1, m_ItemsToBeFound.Length-1); //Select a random category of items, and then an item from that list
		if(RandomNumber_ > 11)
		{
			int ElixirGiven = Random.Range(1, m_Elixirs.Length -1 );
			m_FoundItem = m_Elixirs[ElixirGiven];
//			m_FoundItem as Elixir;
//			m_ItemType as Elixir;
			
		}
		else if(RandomNumber_ >= 50)
		{
			int FoodGiven = Random.Range(1, m_Food.Length -1);
			m_FoundItem = m_Food[FoodGiven];
		}
		else if(RandomNumber_ >= 60)
		{
			int DrinkGiven = Random.Range(1, m_Drink.Length-1);
			m_FoundItem = m_Drink[DrinkGiven];
		}
		else if(RandomNumber_ >=100)
		{
			int ReputationItemGiven = Random.Range(1, m_ReputationItems.Length-1);
			m_FoundItem = m_ReputationItems[ReputationItemGiven];
		}

		//public void AddToInventory(string name, ItemInventoryEntry item)
//		GetComponent<PossessionInventory>().AddToInventory(m_FoundItem.name, m_FoundItem as Item); //add the found item into the player's inventory
		//If someone else wants to make the above line work, be my guest.

		m_EventText.text = "You found a sack lying on the ground. Searching inside reveals... A " + m_FoundItem.name.ToString() + "!";

	}
	//
}
