using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BuildingUI : MonoBehaviour 
{
    public GameObject m_BuildingGUI; //Building UI element
    public GameObject m_ApplyMenu; //Menu that appears after pressing "Apply for Job"
    public GameObject m_ApplyMenuButtonPrefab; //Prefab for the Apply Menu Button to Instantiate later
    public GameObject m_BuyMenu; //Menu that appears after pressing "Buy Items"
    public GameObject m_BuyButtonPrefab; //Prefab for the Buy Items buttons to Instantiate later
    public GameObject m_InteractMenu; //Menu that appears after pressing "Interact"
    public GameObject m_ApplyMenuScrollMask; //Gameobject that is to have the job information as a parent to allow for scrolling
    public GameObject m_BuyMenuScrollMask; //Gameobject that is to have the item information as a parent to allow for scrolling
    public GameObject m_InteractMenuScrollMask; //Gameobject that is to have the item information as a parent to allow for scrolling
    public ScrollRect m_BuyMenuScrollRect;
    public ScrollRect m_ApplyMenuScrollRect;
    public Button m_WorkButton; //Building UI "work" button
    public Button m_InteractButton; //Building UI "interact" button
    public Button m_BuyButton; //Building UI "buy items" button
    public PlayerData m_PlayerData;
    public GameManager m_GameManager;
    public TransitionDisplay m_BuildingTransitionDisplay;
    public TransitionDisplay m_BuyItemsTransitionDisplay;
    public TransitionDisplay m_InteractTransitionDisplay;
    public TransitionDisplay m_ApplyJobTransitionDisplay;
    //public CanvasRenderer m_CanvasRenderer;

    private bool buildingsActive_ = false; //Flag to turn on and off the building UI
    private int buyMenuNumChildren_ = 0; //Buy menu's children (number of items for sale) -- this is set to 1 as the for loop it is used in is 0 based
    private int applyMenuNumChildren_ = 0; //Apply for Job menu's children (number of jobs to apply to) -- this is set to 1 as the for loop it is used in is 0 based
    private int maxNumChildrenOnScreen_ = 6; //Maximum number of children that are fully visible on screen at one time
    private float applyMenuYClamp_; //Clamping the apply for job menu's Y position for scrolling
    private float buyMenuYClamp_; //Clamping the buy menu's Y position for scrolling
    private float subMenuYOffset_ = 70.0f; //Offset Y position for each element in the sub menus
    private GameObject selectedBuilding_; //Selected building GameObject
    private Text[] buildingMenuText_; //Array of text on the building UI element (Buy Items, Interact, etc)..
    private Text[] applyMenuText_; //Array of text for the Apply For Job Menu
    private Text[] buyMenuText_; //Array of text for the Buy Items Menu
    private Text[] interactText_;
    private Text descriptionText_; //Building description text - the funny quip at the top of the building UI
    private Text resultsText_; //Results description text - results of your work
    private SkillAndAmount jobGainedData_;
    private PlayerController playerController_; // Reenable the player after X'ing
    private Vector3 applyMenuInitialPos_;
    private Vector3 buyMenuInitialPos_;
    //private bool transitionToVisible_;
    //private bool isIransitioning_;
    //private float transitionAlpha_;

    public bool BuildingUIActive
    {
        get { return buildingsActive_; }
    }

	// Use this for initialization
	void Start () 
    {
        buildingMenuText_ = m_BuildingGUI.GetComponentsInChildren<Text>();

        descriptionText_ = buildingMenuText_[0];
        resultsText_ = buildingMenuText_[1];
        m_BuildingGUI.SetActive(false);
        //transitionToVisible_ = false;
        //isIransitioning_ = false;
	}
	
	// Update is called once per frame
	void Update ()
    {
        /*if (isIransitioning_ && !buildingsActive_)
        {
            m_BuildingGUI.SetActive(buildingsActive_);
        }*/
        //transitionGUI();

        if(m_GameManager.AITurn)
        {
            m_PlayerData = m_GameManager.m_AIData;
        }
        else if(m_GameManager.PlayerTurn)
        {
            m_PlayerData = m_GameManager.m_PlayerData;
        }

        if(selectedBuilding_ != null)
        {
            CheckForEmployment();
            if(selectedBuilding_.GetComponent<Building>().m_Items.Length == 0)
            {
                m_BuyButton.interactable = false;
            }
            if(selectedBuilding_.GetComponent<Building>().m_SpecialEffects.Length == 0)
            {
                m_InteractButton.interactable = false;
            }

            if(m_BuyMenu != null)
            {
                if (buyMenuNumChildren_ > maxNumChildrenOnScreen_)
                {
                    buyMenuYClamp_ = subMenuYOffset_ * Mathf.Abs(buyMenuNumChildren_ - maxNumChildrenOnScreen_) + applyMenuInitialPos_.y + 10.0f;
                    m_BuyMenuScrollRect.vertical = true;

                    if (m_BuyMenuScrollMask.transform.position.y >= buyMenuYClamp_)
                    {
                        m_BuyMenuScrollMask.transform.position = new Vector3(m_BuyMenuScrollMask.transform.position.x, buyMenuYClamp_, m_BuyMenuScrollMask.transform.position.z);
                    }
                    if(m_BuyMenuScrollMask.transform.position.y <= buyMenuInitialPos_.y)
                    {
                        m_BuyMenuScrollMask.transform.position = new Vector3(m_BuyMenuScrollMask.transform.position.x, buyMenuInitialPos_.y, m_BuyMenuScrollMask.transform.position.z);
                    }
                }
                else
                {
                    m_BuyMenuScrollRect.vertical = false;
                }
            }

            if(m_ApplyMenu != null)
            {
                m_ApplyMenuScrollRect.vertical = true;

                //Get the number of children beyond the max there are and clamp based on how many there are
                applyMenuYClamp_ = subMenuYOffset_ * Mathf.Abs(applyMenuNumChildren_ - maxNumChildrenOnScreen_) + applyMenuInitialPos_.y + 10.0f;

                if (applyMenuNumChildren_ > maxNumChildrenOnScreen_)
                {
                    if (m_ApplyMenuScrollMask.transform.position.y >= applyMenuYClamp_)
                    {
                        m_ApplyMenuScrollMask.transform.position = new Vector3(m_ApplyMenuScrollMask.transform.position.x, applyMenuYClamp_, m_ApplyMenuScrollMask.transform.position.z);
                    }
                    if (m_ApplyMenuScrollMask.transform.position.y <= applyMenuInitialPos_.y)
                    {
                        m_ApplyMenuScrollMask.transform.position = new Vector3(m_ApplyMenuScrollMask.transform.position.x, applyMenuInitialPos_.y, m_ApplyMenuScrollMask.transform.position.z);
                    }
                }
                else
                {
                    m_ApplyMenuScrollRect.vertical = false;
                }
            }
        }
	}

    //This function loads the building data based on which building was clicked
    public void LoadBuildingData(PlayerController pController, GameObject gObj)
    {
        buildingsActive_ = true;
        //kickoffTransitionGUI(true);
        //m_BuildingGUI.SetActive(buildingsActive_);
        m_BuildingTransitionDisplay.FadeIn();
        selectedBuilding_ = gObj;

        //selectedBuilding_ = GameObject.Find(name);
        descriptionText_.text = selectedBuilding_.GetComponent<Building>().GetDescription();

        if(selectedBuilding_.GetComponent<Building>().m_PlayerWorksHere)
        {
            m_WorkButton.interactable = true;
        }
        else
        {
            m_WorkButton.interactable = false;
        }

        playerController_ = pController;
    }

    //This function is called in the update, but only if the player has selected a building
    //The purpose of the function is to check if the player works at that building, and changes whether or not the work button
    //Is interactable or not
    void CheckForEmployment()
    {
        if (selectedBuilding_.GetComponent<Building>().m_PlayerWorksHere)
        {
            m_WorkButton.interactable = true;
        }
        else
        {
            m_WorkButton.interactable = false;
        }
    }

    //Button function - This function is called by the "Buy Items" button
    //If pressed, it will populate and show the Items Menu, from which you can purchase items
    public void BuyItemsMenu()
    {
        float startYPos = 180.0f;

        buyMenuInitialPos_ = m_BuyMenuScrollMask.transform.position;

        //m_BuyMenu.SetActive(true);
        m_BuyItemsTransitionDisplay.PrepareForFadeIn();

        for(int i = 0; i < selectedBuilding_.GetComponent<Building>().m_Items.Length; ++i)
        {
            GameObject go = (GameObject)Instantiate(m_BuyButtonPrefab, new Vector3(0, startYPos, 0), Quaternion.identity);
            go.gameObject.transform.SetParent(m_BuyMenuScrollMask.transform, false);
            go.GetComponent<AnItem>().m_ItemName = selectedBuilding_.GetComponent<Building>().m_Items[i].name;
            go.GetComponent<AnItem>().m_SingleItem.item = selectedBuilding_.GetComponent<Building>().m_Items[i];
            go.GetComponent<AnItem>().m_SingleItem.count = 1;
            go.GetComponentInChildren<Button>().onClick.AddListener(delegate { BuyItems(go); });
            startYPos -= subMenuYOffset_;
            buyMenuNumChildren_++;
        }

        buyMenuText_ = m_BuyMenu.GetComponentsInChildren<Text>();

        int j = 0;
        for(int i = 0; i < selectedBuilding_.GetComponent<Building>().m_Items.Length * 2; i += 2)
        {
            buyMenuText_[i].text = selectedBuilding_.GetComponent<Building>().m_Items[j].name;
            buyMenuText_[i + 1].text = selectedBuilding_.GetComponent<Building>().m_Items[j].GetDescription();
            j++;
        }
        m_BuyItemsTransitionDisplay.FadeIn();
    }

    //Button function - This function is called by the "Interact" button
    //If pressed, it will populate and show the Interact menu, from which you can choose any of the special interactions the building has to offer
    public void InteractionMenu()
    {
        float startYPos = 180.0f;

        //m_InteractMenu.SetActive(true);
        m_InteractTransitionDisplay.PrepareForFadeIn();

        for(int i = 0; i < selectedBuilding_.GetComponent<Building>().m_SpecialEffects.Length; ++i)
        {
            GameObject go = (GameObject)Instantiate(m_BuyButtonPrefab, new Vector3(0, startYPos, 0), Quaternion.identity);
            Button bton = go.GetComponentInChildren<Button>();
            bton.onClick.AddListener(delegate { Interact(go); });
            go.gameObject.transform.SetParent(m_InteractMenuScrollMask.transform, false);
            //go.GetComponentInChildren<Button>().onClick.AddListener(delegate { Interact(go); });
            startYPos -= subMenuYOffset_;
        }

        interactText_ = m_InteractMenu.GetComponentsInChildren<Text>();

        int j = 0;
        for(int i = 0; i < selectedBuilding_.GetComponent<Building>().m_SpecialEffects.Length * 2; i += 2)
        {
            interactText_[i].text = selectedBuilding_.GetComponent<Building>().m_SpecialEffects[j].name;
            //m_InteractMenuText[i + 1].text = selectedBuilding_.GetComponent<Building>().m_SpecialEffects[j].
            interactText_[i + 1].text = "Temp string";
            j++;
        }
        m_InteractTransitionDisplay.FadeIn();
    }

    //Button Function - This function is called by the "Apply for Job" button
    //If pressed, it will populate and show the Apply for Job menu, in which you can choose a job to apply for
    public void ApplyMenu()
    {
        float startYPos = 180.0f;
        
        applyMenuInitialPos_ = m_ApplyMenuScrollMask.transform.position;
        //m_ApplyMenu.SetActive(true);
        m_ApplyJobTransitionDisplay.PrepareForFadeIn();

        //Create the necessary amount of buttons to display on screen
        for (int i = 0; i < selectedBuilding_.GetComponent<Building>().m_JobData.Length; ++i)
        {
            GameObject go = (GameObject)Instantiate(m_ApplyMenuButtonPrefab, new Vector3(0, startYPos, 0), Quaternion.identity);
            go.gameObject.transform.SetParent(m_ApplyMenuScrollMask.transform, false);
            go.GetComponentInChildren<Button>().onClick.AddListener(delegate { ApplyForJob(go); });
            startYPos -= subMenuYOffset_;
            applyMenuNumChildren_++;
        }

        applyMenuText_ = m_ApplyMenu.GetComponentsInChildren<Text>();

        /*
         * i -- Used to get and populate the text elements on the Apply for Job Menu
         *   -- It is incremented by 3 each iteration because there are 3 text elements in each button prefab, so in order to move on to the next prefab without altering other text elements
         *   -- we need to jump over them each iteration
         *   
         * j -- Used to get the job data
         *   -- This is incremented at the end of each iteration of the for loop so it can get the appropriate job data
         * 
         * k -- Used to get the skill gain data
         *  NOTE: To remove the IF statements try using a for loop
         */
        int j = 0;
        int k = 0;
        for (int i = 0; i < selectedBuilding_.GetComponent<Building>().m_JobData.Length * 3; i += 3)
        {
            applyMenuText_[i].text = selectedBuilding_.GetComponent<Building>().m_JobData[j].name;
            applyMenuText_[i + 1].text = selectedBuilding_.GetComponent<Building>().m_JobData[j].m_JobDescription;
            
            if (selectedBuilding_.GetComponent<Building>().m_JobData[j].m_SkillGain.Length == 3)
            {
                applyMenuText_[i + 2].text = selectedBuilding_.GetComponent<Building>().m_JobData[j].m_Wage.ToString() + " shilling(s) and " +
                selectedBuilding_.GetComponent<Building>().m_JobData[j].m_SkillGain[k].m_Amount.ToString() + " point(s) in " + selectedBuilding_.GetComponent<Building>().m_JobData[j].m_SkillGain[k].m_Skill.ToString() + ", " +
                selectedBuilding_.GetComponent<Building>().m_JobData[j].m_SkillGain[k + 1].m_Amount.ToString() + " point(s) in " + selectedBuilding_.GetComponent<Building>().m_JobData[j].m_SkillGain[k + 1].m_Skill.ToString() + "\nand " +
                selectedBuilding_.GetComponent<Building>().m_JobData[j].m_SkillGain[k + 2].m_Amount.ToString() + " point(s) in " + selectedBuilding_.GetComponent<Building>().m_JobData[j].m_SkillGain[k + 2].m_Skill.ToString() + ".";
            }
            else if (selectedBuilding_.GetComponent<Building>().m_JobData[j].m_SkillGain.Length == 2)
            {
                applyMenuText_[i + 2].text = selectedBuilding_.GetComponent<Building>().m_JobData[j].m_Wage.ToString() + " shilling(s) and " +
                selectedBuilding_.GetComponent<Building>().m_JobData[j].m_SkillGain[k].m_Amount.ToString() + " point(s) in " + selectedBuilding_.GetComponent<Building>().m_JobData[j].m_SkillGain[k].m_Skill.ToString() + " and " +
                selectedBuilding_.GetComponent<Building>().m_JobData[j].m_SkillGain[k + 1].m_Amount.ToString() + " point(s) in " + selectedBuilding_.GetComponent<Building>().m_JobData[j].m_SkillGain[k + 1].m_Skill.ToString() + ".";
            }
            else
            {
                applyMenuText_[i + 2].text = selectedBuilding_.GetComponent<Building>().m_JobData[j].m_Wage.ToString() + " shilling(s) and " +
                selectedBuilding_.GetComponent<Building>().m_JobData[j].m_SkillGain[k].m_Amount.ToString() + " point(s) in " + selectedBuilding_.GetComponent<Building>().m_JobData[j].m_SkillGain[k].m_Skill.ToString() + ".";
            }
            j++;
        }

        m_ApplyJobTransitionDisplay.FadeIn();
    }

    //Button function - The object passed into the function is the button itself to get the appropriate information
    //                - The function first finds the name of the job you selected based off of the text of the button the player pressed
    //                - It then will check to see if you were successful in your job application, if you are, the player has gotten a job!
    public void ApplyForJob(GameObject go)
    {
        for(int i = 0; i < selectedBuilding_.GetComponent<Building>().m_JobData.Length; ++i)
        {
            if(selectedBuilding_.GetComponent<Building>().m_JobData[i].name == go.GetComponentInChildren<Button>().GetComponentInChildren<Text>().text)
            {
                jobGainedData_ = selectedBuilding_.GetComponent<Building>().ApplyForJob(m_PlayerData, selectedBuilding_.GetComponent<Building>().m_JobData[i]);

                if(jobGainedData_ == null)
                {
                    m_PlayerData.m_Job = selectedBuilding_.GetComponent<Building>().m_JobData[i];
                }
            }
        }
    }

    public void BuyItems(GameObject go)
    {
        if (m_PlayerData.RemoveSchillings(go.GetComponent<AnItem>().m_SingleItem.item.m_Cost))
        {
            if (go.GetComponent<AnItem>().m_ItemName == "Horse")
            {
                go.GetComponent<AnItem>().m_SingleItem.item.UseItem(m_PlayerData);
            }
            else
            {
                m_PlayerData.m_UseableInventory.AddToInventory(go.GetComponent<AnItem>().m_ItemName, go.GetComponent<AnItem>().m_SingleItem);
            }
        }
        else
        {
            Debug.Log("You are strangely ill-equipped to deal with this..");
        }
    }

    public void Interact(GameObject go)
    {

    }

    //Button function - This function is called by the "Work" button in the Building Menu, it called the Work function in the Building's script.
    public void Work()
    {

        float actualWorkTime = selectedBuilding_.GetComponent<Building>().Work(m_PlayerData, m_PlayerData.m_Job);

        if (m_PlayerData.m_Job.m_SkillGain.Length == 3)
        {
            resultsText_.text = "You earned " + (m_PlayerData.m_Job.m_Wage * actualWorkTime).ToString("F0") + " shillings, " + m_PlayerData.m_Job.m_SkillGain[0].m_Amount * actualWorkTime + " point(s) in " + m_PlayerData.m_Job.m_SkillGain[0].m_Skill + ", " +
                                m_PlayerData.m_Job.m_SkillGain[1].m_Amount * actualWorkTime + " point(s) in " + m_PlayerData.m_Job.m_SkillGain[1].m_Skill + ", and " + m_PlayerData.m_Job.m_SkillGain[2].m_Amount * actualWorkTime + " point(s) in " + 
                                m_PlayerData.m_Job.m_SkillGain[2].m_Skill + ".";
        }
        else if (m_PlayerData.m_Job.m_SkillGain.Length == 2)
        {
            resultsText_.text = "You earned " + (m_PlayerData.m_Job.m_Wage * actualWorkTime).ToString("F0") + " shillings, " + m_PlayerData.m_Job.m_SkillGain[0].m_Amount * actualWorkTime + " point(s) in " + m_PlayerData.m_Job.m_SkillGain[0].m_Skill + " and " +
                                m_PlayerData.m_Job.m_SkillGain[1].m_Amount * actualWorkTime  + " point(s) in " + m_PlayerData.m_Job.m_SkillGain[1].m_Skill + ".";
        }
        else
        {
            resultsText_.text = "You earned " + (m_PlayerData.m_Job.m_Wage * actualWorkTime).ToString("F0") + " shillings, " + m_PlayerData.m_Job.m_SkillGain[0].m_Amount * ValueConstants.WORK_TIME + " point(s) in " + m_PlayerData.m_Job.m_SkillGain[0].m_Skill + ".";
        }

            
    }

    //Button function - This function is called by the giant "X" in the top right corner of the building UI.
    //If the Apply for Job menu is active it will destroy all of it's children -- Pretty dark, right?
    //Otherwise it will just disable the menu.
    public void CloseCurrentMenu()
    {
        if (m_ApplyMenu.activeSelf)
        {
            //m_ApplyMenu.SetActive(false);
            m_ApplyJobTransitionDisplay.FadeOut(delegate { cleanupApplyMenu(); });
        }
        else if(m_BuyMenu.activeSelf)
        {
            //m_BuyMenu.SetActive(false);
            m_BuyItemsTransitionDisplay.FadeOut(delegate { cleanupBuyMenu(); });
        }
        else if(m_InteractMenu.activeSelf)
        {
            //m_InteractMenu.SetActive(false);
            m_InteractTransitionDisplay.FadeOut(delegate { cleanupInteractMenu(); });
        }
        else if (buildingsActive_)
        {
            resultsText_.text = "";
            buildingsActive_ = false;
            //kickoffTransitionGUI(false);
            //m_BuildingGUI.SetActive(buildingsActive_);
            m_BuildingTransitionDisplay.FadeOut(null);
            playerController_.enabled = true;
        }
    }

    void cleanupApplyMenu()
    {
        foreach (RectTransform child in m_ApplyMenuScrollMask.transform)
        {
            GameObject.Destroy(child.gameObject);
        }
    }
    void cleanupBuyMenu()
    {
        foreach (RectTransform child in m_BuyMenuScrollMask.transform)
        {
            GameObject.Destroy(child.gameObject);
        }
    }
    void cleanupInteractMenu()
    {
        foreach (RectTransform child in m_InteractMenuScrollMask.transform)
        {
            GameObject.Destroy(child.gameObject);
        }
    }
}