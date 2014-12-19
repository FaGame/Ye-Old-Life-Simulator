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
    private GameObject inventoryPanel_;
    private List<GameObject> currentButtons_;
    private Image[] imagesInButton_;
    private bool inventoryDisplayed_;

    void Start()
    {
        buttonTexts_ = new Text[2];
        imagesInButton_ = new Image[5];
        currentButtons_ = new List<GameObject>();
        inventoryDisplayed_ = false;
        inventoryPanel_ = GameObject.Find("Inventory");
        inventoryPanel_.SetActive(false);
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
            inventoryPanel_.SetActive(true);

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
                    //set the button image
                    imagesInButton_ = tempButton.GetComponentsInChildren<Image>();
                    for (int l = 0; l < imagesInButton_.Length; ++l)
                    {
                        if (imagesInButton_[l].name == "ItemImage")
                        {
                            GameObject currentObject = GameObject.Find(currentItem.Key);
                            imagesInButton_[l].sprite = currentObject.GetComponent<Image>().sprite;
                        }
                    }
                    //set the parent of the button
                    tempButton.transform.SetParent(backgroundImage.transform, false);
                    //set the name of the button
                    tempButton.name = currentItem.Key;
                    //add the button the the button list
                    currentButtons_.Add(tempButton);
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
            for (int i = 0; i < currentButtons_.Count; ++i)
            {
                Destroy(currentButtons_[i]);
            }
            inventoryPanel_.SetActive(false);
            inventoryDisplayed_ = false;
        }
    }
}
