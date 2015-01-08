using UnityEngine;
using System.Collections;

public class FindASack : RandomEventBaseClass
{
	public GameObject[] m_ItemCategories;
	public GameObject[] m_Elixirs;
	public GameObject[] m_Food;
	public GameObject[] m_Drink;
	public GameObject[] m_ReputationItems;
	public AnItem m_AnItemScript;

	public Item m_ItemType;
//	public PlayerData m_PlayerData;

	public GameObject m_FoundItem;

	public override string PlayEvent(PlayerData pData, string tData)
	{
		m_ItemCategories = new GameObject[4];
		int randomNumber = Random.Range(1, m_ItemCategories.Length-1); //Select a random category of items, and then an item from that list

		if(randomNumber == ValueConstants.ELIXIR_INDEX)
		{
			int ElixirGiven = Random.Range(1, m_Elixirs.Length -1 );
			m_FoundItem = m_Elixirs[ElixirGiven];
			
		}
		else if(randomNumber == ValueConstants.FOOD_INDEX)
		{
			int FoodGiven = Random.Range(1, m_Food.Length -1);
			m_FoundItem = m_Food[FoodGiven];
		}
		else if(randomNumber == ValueConstants.DRINK_INDEX)
		{
			int DrinkGiven = Random.Range(1, m_Drink.Length-1);
			m_FoundItem = m_Drink[DrinkGiven];
		}
		else if(randomNumber == ValueConstants.REPUTATION_INDEX)
		{
			int ReputationItemGiven = Random.Range(1, m_ReputationItems.Length-1);
			m_FoundItem = m_ReputationItems[ReputationItemGiven];
		}
		else
		{
			Debug.Log("Something went wrong, no Category of Item found.");
		}

		//pData.m_UseableInventory.AddToInventory(m_FoundItem.name, m_AnItemScript.GetComponent<AnItem>().m_SingleItem);
//		GetComponent<PossessionInventory>().AddToInventory(m_FoundItem.name, m_FoundItem as Item); //add the found item into the player's inventory

		tData = "You found a sack lying on the ground. Searching inside reveals... A " + m_FoundItem.name.ToString() + "!";
		return tData;
	}

	public override string UpdateEvent(PlayerData m_Player, string m_EventDesc)
	{
		throw new System.NotImplementedException();
	}
	//
}
