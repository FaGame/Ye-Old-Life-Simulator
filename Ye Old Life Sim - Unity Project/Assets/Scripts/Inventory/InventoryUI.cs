using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class InventoryUI : MonoBehaviour 
{
    //temp code------------------------------------------
    public UseableItemInventory m_InventoryScript;
    //---------------------------------------------------
    public PlayerController m_PlayerController;
    public float m_yScale = .25f;
    public float m_buttonYSeperationDistance = 25.0f;
    public float m_buttonYMovement = -50.0f;

    private Text[] buttonTexts_;
    private GameObject inventoryPanel;
    private List<GameObject> currentButtons;
    private bool inventoryDisplayed_;

    void Start()
    {
        buttonTexts_ = new Text[2];
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
            //disable player movement
            m_PlayerController.enabled = false;
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
                    //create the new button
                    GameObject tempButton = Instantiate(inventoryButton, new Vector3(inventoryButton.transform.localPosition.x, inventoryButton.transform.localPosition.y + buttonYMovement, inventoryButton.transform.localPosition.z), Quaternion.identity) as GameObject;
                    //tempButton.GetComponentInChildren<Text>().text = currentItem.Key;
                    //set the name and count of the item
                    buttonTexts_ = tempButton.GetComponentsInChildren<Text>();
                    for (int l = 0; l < buttonTexts_.Length; ++l)
                    {
                        if(buttonTexts_[l].name == "InventoryItemButtonText")
                        {
                            buttonTexts_[l].text = currentItem.Key;
                        }
                        else if(buttonTexts_[l].name == "ItemCount")
                        {
                            buttonTexts_[l].text = currentItem.Value.count.ToString();
                        }
                    }
                    //set the parent of the button
                    tempButton.transform.SetParent(backgroundImage.transform, false);
                    //set the name of the button
                    tempButton.name = currentItem.Key;
                    //add the button the the button list
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
            //enable player movement
            m_PlayerController.enabled = true;
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
