using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class InventoryUI : MonoBehaviour 
{
    public PlayerController m_PlayerController;
    public GameManager m_GameManager;
    public GameObject m_InventoryPanel;
    public GameObject m_InventoryScrollMask;
    public GameObject m_InventoryButtonPrefab;

    private Text[] buttonTexts_;
    private GameObject inventoryPanel_;
    private Image itemImage_;
    private bool inventoryDisplayed_;
    private float subMenuYOffset_ = 70.0f;
    private int numChildren_;

    public bool InventoryActive
    {
        get { return inventoryDisplayed_; }
    }

    void Start()
    {
        buttonTexts_ = new Text[2];
        inventoryDisplayed_ = false;
        /*inventoryPanel_ = GameObject.Find("Inventory");
        inventoryPanel_.SetActive(false);*/
    }

    public void DisplayInventory(UseableItemInventory inventoryScript)
    {
        if (!inventoryDisplayed_ && !m_GameManager.m_BuildingUI.BuildingUIActive)
        {
            //disable player movement
            m_PlayerController.enabled = false;
            //display inventory
            m_InventoryPanel.SetActive(true);

            int i = 0;
            if(inventoryScript.m_UseableItemInventory.Count != 0)
            {
                float startYPos = 180.0f;

                //loop through the inventory and display a button for each item.
                foreach (KeyValuePair<string, Item.ItemInventoryEntry> currentItem in inventoryScript.m_UseableItemInventory)
                {
                    GameObject go = (GameObject)Instantiate(m_InventoryButtonPrefab, new Vector3(0.0f, startYPos, 0.0f), Quaternion.identity);
                    go.gameObject.transform.SetParent(m_InventoryScrollMask.transform, false);
                    startYPos -= subMenuYOffset_;
                    numChildren_++;

                    //set the name and count of the item
                    buttonTexts_ = go.GetComponentsInChildren<Text>();

                    buttonTexts_[0].text = currentItem.Key;
                    buttonTexts_[1].text = currentItem.Value.count.ToString();
                    /*for (int l = 0; l < buttonTexts_.Length; ++l)
                    {
                        if (buttonTexts_[l].name == "InventoryItemButtonText")
                        {
                            buttonTexts_[l].text = currentItem.Key;
                        }
                        else if (buttonTexts_[l].name == "ItemCount")
                        {
                            buttonTexts_[l].text = currentItem.Value.count.ToString();
                        }
                    }*/
                    //set the button image
                    itemImage_ = go.GetComponentInChildren<Image>();

                    itemImage_.sprite = GameObject.Find(currentItem.Key).GetComponent<Image>().sprite;

                    //set the name of the button
                    //go.name = currentItem.Key;

                    ++i;
                }
            }
            inventoryDisplayed_ = true;
        }
        else
        {
            //enable player movement
            m_PlayerController.enabled = true;
            m_InventoryPanel.SetActive(false);
            inventoryDisplayed_ = false;
        }
    }
}
