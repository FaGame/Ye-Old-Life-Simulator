﻿using UnityEngine;
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
    public Button m_WorkButton; //Building UI "work" button
    public Button m_InteractButton; //Building UI "interact" button
    public Button m_BuyButton; //Building UI "buy items" button
    public PlayerData m_PlayerData;
    public GameManager m_GameManager;
    public TransitionDisplay m_TransitionDisplay;
    public CanvasRenderer m_CanvasRenderer;

    private bool buildingsActive_ = false; //Flag to turn on and off the building UI
    private GameObject selectedBuilding_; //Selected building GameObject
    private Text[] buildingMenuText_; //Array of text on the building UI element (Buy Items, Interact, etc)..
    private Text[] applyMenuText_; //Array of text for the Apply For Job Menu
    private Text[] buyMenuText_; //Array of text for the Buy Items Menu
    private Text[] interactText_;
    private Text descriptionText_; //Building description text - the funny quip at the top of the building UI
    private Text resultsText_; //Results description text - results of your work
    private SkillAndAmount jobGainedData_;
    private PlayerController playerController_; // Reenable the player after X'ing
    private bool transitionToVisible_;
    private bool isIransitioning_;
    private float transitionAlpha_;

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
        transitionToVisible_ = false;
        isIransitioning_ = false;
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

        //-------------TEMP CODE-------------
        /*if (Input.GetMouseButtonDown(0) && !buildingsActive_)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit rayHit;
            if (Physics.Raycast(ray, out rayHit))
            {
                if (rayHit.collider.tag == "Building")
                {
                    Debug.Log("Name: " + rayHit.collider.name);
                    LoadBuildingData(rayHit.collider.name);
                }
            }
        }*/
        //-------------END TEMP CODE-------------


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
        }
	}

    //This function loads the building data based on which building was clicked
    public void LoadBuildingData(/*string name*/PlayerController pController, GameObject gObj)
    {
       
        buildingsActive_ = true;
        //kickoffTransitionGUI(true);
        //m_BuildingGUI.SetActive(buildingsActive_);
        m_TransitionDisplay.FadeIn();
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
        float yPosOffset = 70.0f;

        m_BuyMenu.SetActive(true);

        for(int i = 0; i < selectedBuilding_.GetComponent<Building>().m_Items.Length; ++i)
        {
            GameObject go = (GameObject)Instantiate(m_BuyButtonPrefab, new Vector3(0, startYPos, 0), Quaternion.identity);
            go.gameObject.transform.SetParent(m_BuyMenuScrollMask.transform, false);
            go.GetComponent<AnItem>().m_ItemName = selectedBuilding_.GetComponent<Building>().m_Items[i].name;
            go.GetComponent<AnItem>().m_SingleItem.item = selectedBuilding_.GetComponent<Building>().m_Items[i];
            go.GetComponent<AnItem>().m_SingleItem.count = 1;
            go.GetComponentInChildren<Button>().onClick.AddListener(delegate { BuyItems(go); });
            startYPos -= yPosOffset;
        }

        buyMenuText_ = m_BuyMenu.GetComponentsInChildren<Text>();

        int j = 0;
        for(int i = 0; i < selectedBuilding_.GetComponent<Building>().m_Items.Length * 2; i += 2)
        {
            buyMenuText_[i].text = selectedBuilding_.GetComponent<Building>().m_Items[j].name;
            buyMenuText_[i + 1].text = selectedBuilding_.GetComponent<Building>().m_Items[j].GetDescription();
            j++;
        }
    }

    //Button function - This function is called by the "Interact" button
    //If pressed, it will populate and show the Interact menu, from which you can choose any of the special interactions the building has to offer
    public void InteractionMenu()
    {
        float startYPos = 180.0f;
        float yPosOffset = 70.0f;

        m_InteractMenu.SetActive(true);

        for(int i = 0; i < selectedBuilding_.GetComponent<Building>().m_SpecialEffects.Length; ++i)
        {
            GameObject go = (GameObject)Instantiate(m_BuyButtonPrefab, new Vector3(0, startYPos, 0), Quaternion.identity);
            go.gameObject.transform.SetParent(m_InteractMenuScrollMask.transform, false);
            go.GetComponentInChildren<Button>().onClick.AddListener(delegate { Interact(go); });
            startYPos -= yPosOffset;
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
    }

    //Button Function - This function is called by the "Apply for Job" button
    //If pressed, it will populate and show the Apply for Job menu, in which you can choose a job to apply for
    public void ApplyMenu()
    {
        float startYPos = 180.0f;
        float yPosOffset = 70.0f;
        m_ApplyMenu.SetActive(true);

        //Create the necessary amount of buttons to display on screen
        Debug.Log(selectedBuilding_.GetComponent<Building>().m_JobData.Length.ToString());
        for (int i = 0; i < selectedBuilding_.GetComponent<Building>().m_JobData.Length; ++i)
        {
            GameObject go = (GameObject)Instantiate(m_ApplyMenuButtonPrefab, new Vector3(0, startYPos, 0), Quaternion.identity);
            go.gameObject.transform.SetParent(m_ApplyMenuScrollMask.transform, false);
            go.GetComponentInChildren<Button>().onClick.AddListener(delegate { ApplyForJob(go); });
            startYPos -= yPosOffset;
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
            m_PlayerData.m_UseableInventory.AddToInventory(go.GetComponent<AnItem>().m_ItemName, go.GetComponent<AnItem>().m_SingleItem);
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
            foreach (RectTransform child in m_ApplyMenuScrollMask.transform)
            {
                GameObject.Destroy(child.gameObject);
            }

            m_ApplyMenu.SetActive(false);
        }
        else if(m_BuyMenu.activeSelf)
        {
            foreach(RectTransform child in m_BuyMenuScrollMask.transform)
            {
                GameObject.Destroy(child.gameObject);
            }

            m_BuyMenu.SetActive(false);
        }
        else if(m_InteractMenu.activeSelf)
        {
            foreach(RectTransform child in m_InteractMenuScrollMask.transform)
            {
                GameObject.Destroy(child.gameObject);
            }

            m_InteractMenu.SetActive(false);
        }
        else if (buildingsActive_)
        {
            resultsText_.text = "";
            buildingsActive_ = false;
            //kickoffTransitionGUI(false);
            //m_BuildingGUI.SetActive(buildingsActive_);
            m_TransitionDisplay.FadeOut();
            playerController_.enabled = true;
        }
    }

    void kickoffTransitionGUI(bool toVisible)
    {
        if(!isIransitioning_)
        {
            transitionToVisible_ = toVisible;
            transitionAlpha_ = transitionToVisible_ ? 0.0f : 1.0f;
            m_CanvasRenderer.SetAlpha(transitionAlpha_);
            isIransitioning_ = true;
        }
    }

    void transitionGUI()
    {
        if(isIransitioning_)
        {
            transitionAlpha_ += transitionToVisible_ ? Time.deltaTime : -Time.deltaTime;
            m_CanvasRenderer.SetAlpha(transitionAlpha_);
            if((transitionAlpha_ <= 0.0f) || (transitionAlpha_ >= 1.0f))
            {
                isIransitioning_ = false;
                if(!transitionToVisible_)
                {
                    m_BuildingGUI.SetActive(buildingsActive_);
                }
            }
        }
    }
}