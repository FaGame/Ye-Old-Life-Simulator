using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class InventoryUI : MonoBehaviour 
{
    //temp code------------------------------------------
    public UseableItemInventory m_InventoryScript;
    //---------------------------------------------------
    public float m_yScale = .25f;
    public float m_buttonYSeperationDistance = 25.0f;
    public float m_buttonYMovement = -50.0f;

    private GameObject inventoryPanel;
    private List<GameObject> currentButtons;
    private bool inventoryDisplayed_;

    void Start()
    {
        currentButtons = new List<GameObject>();
        inventoryDisplayed_ = false;
        inventoryPanel = GameObject.Find("Inventory");
        inventoryPanel.SetActive(false);
    }

	void Update () 
    {
        //temp code-------------------------------------
	    if(Input.GetKeyDown(KeyCode.I))
        {
            DisplayInventry(m_InventoryScript);
        }
        //---------------------------------------------
	}

    public void DisplayInventry(UseableItemInventory inventoryScript)
    {
        if(!inventoryDisplayed_)
        {
            //display inventory
            inventoryPanel.SetActive(true);

            Image backgroundImage = GameObject.Find("BackgroundImage").GetComponent<Image>();
            Vector3 imageScale = backgroundImage.rectTransform.localScale;
            imageScale = new Vector3(1.0f, 1.0f, 1.0f);
            GameObject inventoryButton = GameObject.Find("InventoryItemButton");

            float buttonYMovement = m_buttonYMovement;
            int i = 0;
            if(inventoryScript.m_UseableItemInventory.Count != 0)
            {
                //loop through the inventory and display a button for each item.
                foreach(KeyValuePair<string, UseableItemInventory.ItemInventoryEntry> currentItem in inventoryScript.m_UseableItemInventory)
                {
                    buttonYMovement += (1) * m_buttonYSeperationDistance;
                    GameObject tempButton = Instantiate(inventoryButton, new Vector3(inventoryButton.transform.localPosition.x, inventoryButton.transform.localPosition.y + buttonYMovement, inventoryButton.transform.localPosition.z), Quaternion.identity) as GameObject;
                    tempButton.GetComponentInChildren<Text>().text = currentItem.Key;
                    tempButton.transform.SetParent(backgroundImage.transform, false);
                    tempButton.name = currentItem.Key;
                    currentButtons.Add(tempButton);
                    imageScale.y += m_yScale;
                    ++i;
                }
                backgroundImage.rectTransform.localScale = imageScale;
            }
            inventoryDisplayed_ = true;
        }
        else
        {
            //remove inventory
            for (int i = 0; i < currentButtons.Count; ++i)
            {
                Destroy(currentButtons[i]);
            }
            inventoryPanel.SetActive(false);
            inventoryDisplayed_ = false;
        }
    }
}
