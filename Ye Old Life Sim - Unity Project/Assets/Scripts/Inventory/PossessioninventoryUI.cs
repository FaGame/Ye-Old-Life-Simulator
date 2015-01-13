using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class PossessioninventoryUI : MonoBehaviour 
{
    public PlayerData m_PlayerData;
    public PlayerController m_PlayerController;
    public GameObject m_PossInvPrefab; //Prefab that contains the text elements for the inventory descriptions
    public GameObject m_PossInvScrollMask;
    public GameObject m_PossInv;
    public TransitionDisplay m_PossInvTransitionDisplay;
    public ScrollRect m_InvScrollRect;
    public Font m_CustomFont;

    private float subMenuYOffset_ = 70.0f;
    private float menuYClamp_;
    private int numChildren_ = 0;
    private int maxNumChildrenOnScreen_ = 6;
    private bool inventoryActive_;
    private Vector3 initialPos_;

    public bool InventoryActive
    {
        get { return inventoryActive_; }
    }

	// Use this for initialization
	void Start ()
    {
        initialPos_ = m_PossInvScrollMask.transform.position;

        m_PossInv.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () 
    {
        if (numChildren_ > maxNumChildrenOnScreen_)
        {
            menuYClamp_ = subMenuYOffset_ * Mathf.Abs(numChildren_ - maxNumChildrenOnScreen_) + initialPos_.y + 10.0f;
            m_InvScrollRect.vertical = true;

            if (m_PossInvScrollMask.transform.position.y >= menuYClamp_)
            {
                m_PossInvScrollMask.transform.position = new Vector3(m_PossInvScrollMask.transform.position.x, menuYClamp_, m_PossInvScrollMask.transform.position.z);
            }
            if (m_PossInvScrollMask.transform.position.y <= initialPos_.y)
            {
                m_PossInvScrollMask.transform.position = new Vector3(m_PossInvScrollMask.transform.position.x, initialPos_.y, m_PossInvScrollMask.transform.position.z);
            }
        }
        else
        {
            m_InvScrollRect.vertical = false;
        }
	}

    public void OpenInventory(PossessionInventory inventory)
    {
        m_PossInv.SetActive(true);
        inventoryActive_ = true;
        float startYPos = 110.0f;

        m_PlayerController.enabled = false;

        m_PossInvTransitionDisplay.PrepareForFadeIn();

        foreach (KeyValuePair<string, Item.ItemInventoryEntry> currItem in inventory.m_PossessionItemInventory)
        {
            GameObject go = (GameObject)Instantiate(m_PossInvPrefab, new Vector3(0.0f, startYPos, 0.0f), Quaternion.identity);
            go.gameObject.transform.SetParent(m_PossInvScrollMask.transform, false);
            startYPos -= subMenuYOffset_;
            numChildren_++;

            Text[] temp = go.GetComponentsInChildren<Text>();
            if (temp.Length > 0)
            {
                temp[0].font = m_CustomFont;
                temp[1].font = m_CustomFont;
                temp[0].text = currItem.Key;
                temp[1].text = "Test String";
            }
        }

        m_PossInvTransitionDisplay.FadeIn();
    }

    public void CloseInventory()
    {
        if (!m_PossInvTransitionDisplay.IsTransitioning())
        {
            inventoryActive_ = false;
            m_PossInv.SetActive(false);
            m_PossInvTransitionDisplay.FadeOut(delegate { cleanupInvMenu(); });
        }
    }

    void cleanupInvMenu()
    {
        foreach (RectTransform child in m_PossInvScrollMask.transform)
        {
            GameObject.Destroy(child.gameObject);
        }
    }
}
