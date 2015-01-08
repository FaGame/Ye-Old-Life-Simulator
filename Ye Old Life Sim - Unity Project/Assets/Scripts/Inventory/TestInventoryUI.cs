using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Collections.Specialized;

public class TestInventoryUI : MonoBehaviour 
{
    public GameObject m_InventoryMenu; // Menu that appears after pressing "Inventory Button"
    public GameObject m_InventoryButtonPrefab; //Prefab for the Buy Items buttons to Instantiate later
    public GameObject m_InventoryMenuScrollMask; //Gameobject that is to have the item information as a parent to allow for scrolling
    public PlayerData m_PlayerData;
    public TransitionDisplay m_TransitionDisplay;

	// Use this for initialization
	void Start () 
    {
	
	}
	
	// Update is called once per frame
	void Update () 
    {
	
	}

    public void DisplayInventory()
    {
        float startYPos = 180.0f;
        float yPosOffset = 70.0f;
        Dictionary<string, Item.ItemInventoryEntry> inventory = m_PlayerData.m_UseableInventory.m_UseableItemInventory;
        Text[] invMenuText;

        //m_InventoryMenu.SetActive(true);
        m_TransitionDisplay.PrepareForFadeIn();
        invMenuText = m_InventoryMenu.GetComponentsInChildren<Text>();

        int i = 0;
        foreach (KeyValuePair<string, Item.ItemInventoryEntry> pair in inventory)
        {
            GameObject go = (GameObject)Instantiate(m_InventoryButtonPrefab, new Vector3(0, startYPos, 0), Quaternion.identity);
            go.gameObject.transform.SetParent(m_InventoryMenuScrollMask.transform, false);
            /*go.GetComponent<AnItem>().m_ItemName = pair.Key;
            go.GetComponent<AnItem>().m_SingleItem.item = pair.Value.item;
            go.GetComponent<AnItem>().m_SingleItem.count = 1;*/
            go.GetComponentInChildren<Button>().onClick.AddListener(delegate { UseItem(go); });
            startYPos -= yPosOffset;

            go.GetComponentInChildren<Text>().text = pair.Key;
            ++i;
        }
        m_TransitionDisplay.FadeIn();
    }

    public void UseItem(GameObject go)
    {
        //go.GetComponent<AnItem>().m_SingleItem.item.UseItem(m_PlayerData);
        go.GetComponent<Item>().m_ItemEntryData.item.UseItem(m_PlayerData);
        m_PlayerData.m_UseableInventory.RemoveFromInventory(go.GetComponent<Item>().m_ItemEntryData.item.name);
        //m_PlayerData.m_UseableInventory.RemoveFromInventory(go.GetComponent<AnItem>().m_ItemName);
    }

    public void CloseCurrentMenu()
    {
        m_TransitionDisplay.FadeOut(cleanupInventoryMenu);
    }

    void cleanupInventoryMenu()
    {
        foreach (RectTransform child in m_InventoryMenuScrollMask.transform)
        {
            GameObject.Destroy(child.gameObject);
        }
    }
}
