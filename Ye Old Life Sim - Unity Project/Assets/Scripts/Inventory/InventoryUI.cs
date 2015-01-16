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
    private List<GameObject> inventoryChildren_ = new List<GameObject>();
    private Image itemImage_;
    private UseableItemInventory usableInventory_;
    private bool inventoryDisplayed_;
    private float subMenuYOffset_ = 100.0f;
    private int numChildren_;
    private int childIterator_ = 0;
    private int textIterator_ = 0;

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
            usableInventory_ = inventoryScript;
            //disable player movement
            m_PlayerController.enabled = false;
            //display inventory
            m_InventoryPanel.SetActive(true);

            m_InvTransitionDisplay.PrepareForFadeIn();

            int i = 0;
            if(inventoryScript.m_UseableItemInventory.Count != 0)
            {
                float startYPos = 110.0f;

                //loop through the inventory and display a button for each item.
                foreach (KeyValuePair<string, Item.ItemInventoryEntry> currentItem in inventoryScript.m_UseableItemInventory)
                {
                    GameObject go = (GameObject)Instantiate(m_InventoryButtonPrefab, new Vector3(0.0f, startYPos, 0.0f), Quaternion.identity);
                    go.gameObject.transform.SetParent(m_InventoryScrollMask.transform, false);
                    go.name = currentItem.Key;
                    Button button = go.GetComponentInChildren<Button>();
                    button.onClick.AddListener(delegate { inventoryScript.UseItem(m_PlayerController.m_PlayerData, go.name); });
                    startYPos -= subMenuYOffset_;
                    numChildren_++;
                    inventoryChildren_.Add(go);

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
    
    void Update()
    {
        //UpdateInventory();
    }

    void UpdateInventory()
    {
        if (usableInventory_ != null)
        {
            foreach(KeyValuePair<string, Item.ItemInventoryEntry> currItem in usableInventory_.m_UseableItemInventory)
            {
                if (currItem.Value.count <= 0)
                {
                    inventoryChildren_.Remove(inventoryChildren_[childIterator_]);
                    childIterator_--;
                }
                if (currItem.Value.count > 0)
                {
                    Text[] texts = inventoryChildren_[childIterator_].GetComponentsInChildren<Text>();
                    texts[0].text = currItem.Key;
                    texts[1].text = currItem.Value.count.ToString();
                }
                childIterator_++;
            }
            if(childIterator_ >= inventoryChildren_.Count)
            {
                childIterator_ = 0;
            }
        }
    }

    public void CloseInventory()
    {
        if (!m_InvTransitionDisplay.IsTransitioning())
        {
            m_InventoryPanel.SetActive(false);
            inventoryDisplayed_ = false;
            m_InvTransitionDisplay.FadeOut(delegate { cleanupInvMenu(); });
        }
    }

    void cleanupInvMenu()
    {
        foreach (RectTransform child in m_InventoryScrollMask.transform)
        {
            GameObject.Destroy(child.gameObject);
        }
    }
}
