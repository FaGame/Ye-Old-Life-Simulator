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
    public ScrollRect m_InvScrollRect;
    public TransitionDisplay m_InvTransitionDisplay;
    public Font m_CustomFont;

    private Text[] buttonTexts_;
    private Image itemImage_;
    private bool inventoryDisplayed_;
    private float subMenuYOffset_ = 100.0f;
    private int numChildren_;

    public bool InventoryActive
    {
        get { return inventoryDisplayed_; }
    }

    void Start()
    {
        buttonTexts_ = new Text[2];
        inventoryDisplayed_ = false;
    }

    public void DisplayInventory(UseableItemInventory inventoryScript)
    {
        if (!inventoryDisplayed_ && !m_GameManager.m_BuildingUI.BuildingUIActive)
        {
            //disable player movement
            m_PlayerController.enabled = false;
            //display inventory
            m_InventoryPanel.SetActive(true);

            m_InvTransitionDisplay.PrepareForFadeIn();

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

                    buttonTexts_[0].font = m_CustomFont;
                    buttonTexts_[1].font = m_CustomFont;
                    buttonTexts_[0].text = currentItem.Key;
                    buttonTexts_[1].text = currentItem.Value.count.ToString();

                    //set the button image
                    itemImage_ = go.GetComponentInChildren<Image>();

                    itemImage_.sprite = GameObject.Find(currentItem.Key).GetComponent<Image>().sprite;

                    Debug.Log(currentItem.Value.count.ToString());
                    Debug.Log(currentItem.Key);
                    ++i;
                }
            }
            inventoryDisplayed_ = true;

            m_InvTransitionDisplay.FadeIn();
        }
    }

    public void CloseInventory()
    {
        m_InventoryPanel.SetActive(false);
        m_InvTransitionDisplay.FadeOut(delegate { cleanupInvMenu(); });
    }

    void cleanupInvMenu()
    {
        foreach (RectTransform child in m_InvTransitionDisplay.transform)
        {
            GameObject.Destroy(child.gameObject);
        }
    }
}
