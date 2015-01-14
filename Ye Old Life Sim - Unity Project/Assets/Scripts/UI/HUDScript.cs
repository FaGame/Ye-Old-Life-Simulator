using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(Canvas))]
public class HUDScript : MonoBehaviour 
{
    public AudioSource m_JournalSound;
    public AudioSource m_CloseMenu;

    public Canvas m_PlayerHUD; //Player's HUD canvas
    public GameObject m_StatsScreen; //Game object containing the stats panels
    public GameObject m_Stats; //The stat's panel
    public GameObject m_Goals; //The goals panel
    public GameObject m_Skills; //The skills panel
    public GameManager m_GameManager; //The game manager
    public GameObject m_InventoryPanel; // The inventory UI
    public PlayerData m_PlayerData; //The player's data
    public PlayerController m_PlayerController; //The player's controller
    public Text m_CurrJobText; //The player's current job text
    public Text m_ShillingText; //The player's current shillings
    public Text m_BuildingHovered; //The building the player is currently hovering
    public TransitionDisplay m_StatsTransitionDisplay;
    public TransitionDisplay m_InventoryTransitionDisplay;
    public GameObject m_BuildingButtonPrefab; //Prefab for the Building buttons to Instantiate later
    public GameObject m_BuildingMenuScrollMask; //Gameobject that is to have the item information as a parent to allow for scrolling
    public TransitionOut m_TransOutBuilding;

    private bool statsActive_ = false; //This bool determines whether or not the the stats window is open
    private bool inventoryActive_ = false; //This bool determines whether or not the inventory is currently open
    private bool consumableActive_ = false; //This bool determines whether or not the consumable inventory is currently open
    private bool possessionActive_ = false; //This bool determines whether or not the possession inventory is currently open
    private bool HUDActive_ = false; //This bool determines whether any HUD elements are active
    private bool wasHighlighted_ = false;
    private bool objectivesSetUp_ = false;
    private bool escapePressed_ = false;
    private float timer_; //Turn timer
    private Slider[] sliderArray_; //Array containing the HUD sliders
    private Slider[] objSliderArray_; //Array of objective sliders
    private Slider timeSlider_; //Slider for the time left in your turn
    private Slider hungerSlider_; //Slider for the amount of player's hunger
    private Slider repObjSlider_; //Slider for the reputation objective
    private Slider currObjSlider_; //Slider for the currency/shillings objective
    private Slider happyObjSlider_; //Slider for the happiness objective
    private Text[] statsText_; //Array of text for the stats screen
    private Text[] skillsText_; //Array of text for the skills screen
    private string skillString_; //String that will contain and display the skill names
    private List<float> playerStats_ = new List<float>(); //List of the player stats
    private Ray buildingHoverRay_;
    private RaycastHit buildingRayHit_;
    private Color startColour_;
    private GameObject highlightedObject_;
    private List<Color> originalColours_ = new List<Color>();
    private GameObject[] buildingObjects_;
    private float subMenuYOffset_ = 23.0f; //Offset Y position for each element in the sub menus
    private bool isBuildingMenuOpen_;

    public bool HUDActive
    {
        get { return HUDActive_; }
    }

	// Use this for initialization
	void Start () 
    {
        //Initialize and set up the slider arrays
        sliderArray_ = m_PlayerHUD.GetComponentsInChildren<Slider>();
        SetUpHUDSliders();

        objSliderArray_ = m_Goals.GetComponentsInChildren<Slider>();
        //SetUpObjectiveSliders();

        m_BuildingHovered.text = "";
        m_CurrJobText.text = "Unemployed";
        

        //Load the player stats list with the player's stats
        playerStats_.Add((float)m_PlayerData.m_Home.m_Rating);
        playerStats_.Add(m_PlayerData.m_HungerMeter);
        playerStats_.Add(m_PlayerData.m_Reputation);
        playerStats_.Add(m_PlayerData.m_Shillings);
        playerStats_.Add(m_PlayerData.m_Happiness);
        
        //Initialize the text arrays
        statsText_ = m_Stats.GetComponentsInChildren<Text>();
        skillsText_ = m_Skills.GetComponentsInChildren<Text>();

        //Turn off the stats, goal and skills panels after initializing all stats
        m_StatsScreen.SetActive(false);

        //Set the current turn's timer
        timer_ = ValueConstants.PLAYER_MAX_TIME;

        buildingObjects_ = GameObject.FindGameObjectsWithTag("Building");
        isBuildingMenuOpen_ = false;
	}

    //This function initializes the player HUD sliders, and then sets their min, max and current values
    void SetUpHUDSliders()
    {
        timeSlider_ = sliderArray_[0];
        hungerSlider_ = sliderArray_[1];

        timeSlider_.maxValue = ValueConstants.PLAYER_MAX_TIME;
        timeSlider_.minValue = 0.0f;
        timeSlider_.value = timer_;

        hungerSlider_.maxValue = ValueConstants.PLAYER_MAX_HUNGER;
        hungerSlider_.minValue = 0.0f;
        hungerSlider_.value = 0.0f;
    }

    //This function initializes the objective sliders in the stats menu, then sets their min, max and current values
    void SetUpObjectiveSliders()
    {
        repObjSlider_ = objSliderArray_[0];
        currObjSlider_ = objSliderArray_[1];
        happyObjSlider_ = objSliderArray_[2];

        repObjSlider_.maxValue = m_PlayerData.m_MaxReputation;
        repObjSlider_.minValue = 0.0f;

        currObjSlider_.maxValue = m_PlayerData.m_MaxShillings;
        currObjSlider_.minValue = 0.0f;

        happyObjSlider_.maxValue = m_PlayerData.m_MaxHappiness;
        happyObjSlider_.minValue = 0.0f;
    }
	
	// Update is called once per frame
	void Update ()
    {
        SetUpObjectiveSliders();

        if(m_GameManager.AITurn == true)
        {
            m_PlayerData = m_GameManager.m_AIData;
        }
        else if(m_GameManager.PlayerTurn == true)
        {
            m_PlayerData = m_GameManager.m_PlayerData;
        }
        else if (m_GameManager.PlayerTwoTurn)
        {
            m_PlayerData = m_GameManager.m_PlayerTwoData;
        } 

        if (statsActive_ || inventoryActive_)
        {
            HUDActive_ = true;
        }
        else
        {
            HUDActive_ = false;
        }

        consumableActive_ = GetComponent<InventoryUI>().InventoryActive;
        possessionActive_ = GetComponent<PossessioninventoryUI>().InventoryActive;

        timeSlider_.value = m_PlayerData.m_CurrTime;
        hungerSlider_.value = m_PlayerData.m_HungerMeter;

        m_ShillingText.text = m_PlayerData.m_Shillings.ToString();

        if (m_PlayerData.m_HungerMeter >= ValueConstants.PLAYER_MAX_HUNGER)
        {
            m_PlayerData.m_HungerMeter = ValueConstants.PLAYER_MAX_HUNGER;
        }

        if(m_PlayerData.m_Job != null)
        {
            m_CurrJobText.text = m_PlayerData.m_Job.name;
        }

        if (Input.GetKeyDown(KeyCode.Escape) && !escapePressed_)
        {
            escapePressed_ = true;
            CloseCurrentMenu();
        }
        else if (Input.GetKey(KeyCode.B) || Input.GetKey(KeyCode.I))
        {
            OpenInventoryMenu();
        }
        else if (Input.GetKey(KeyCode.C))
        {
            OpenStatsMenu();
        }

        if(Input.GetKeyDown(KeyCode.Escape) && escapePressed_)
        {
            escapePressed_ = false;
        }

        buildingHoverRay_ = Camera.main.ScreenPointToRay(Input.mousePosition);

        if(Physics.Raycast(buildingHoverRay_, out buildingRayHit_))
        {
            highlightedObject_ = buildingRayHit_.collider.gameObject;
            if (highlightedObject_.GetComponent<Building>() || highlightedObject_.GetComponent<Habitat>())
            {
                m_BuildingHovered.text = highlightedObject_.name;
            }
            else if(wasHighlighted_)
            {
                m_BuildingHovered.text = "";
            }
        }
	}

    //Button function - Opens the stats screen on press
    public void OpenStatsMenu()
    {
        if (!m_GameManager.m_BuildingUI.BuildingUIActive)
        {
            m_PlayerController.enabled = false;
            m_JournalSound.Play();
            statsActive_ = true;
            //m_StatsScreen.SetActive(statsActive_);
            m_StatsTransitionDisplay.FadeIn();
            PopulateStats();
            PopulateSkills();
            UpdateSliders();
        }
    }

    public void OpenInventoryMenu()
    {
        m_InventoryPanel.SetActive(true);
        inventoryActive_ = true;
        m_InventoryTransitionDisplay.FadeIn();
    }

    public void CloseInventoryMenu()
    {
        m_InventoryPanel.SetActive(false);
    }

    //This function gets the latest stats when the player opens the stats menu
    void PopulateStats()
    {
        //Clear the list and re-add the variables to make sure the information is completely up to date
        playerStats_.Clear();
        playerStats_.Add((float)m_PlayerData.m_Home.m_Rating);
        playerStats_.Add(m_PlayerData.m_HungerMeter);
        playerStats_.Add(m_PlayerData.m_Reputation);
        playerStats_.Add(m_PlayerData.m_Shillings);
        playerStats_.Add(m_PlayerData.m_Happiness);

        //Loop through the pair of arrays to get the appropriate values next to the proper text
        for (int i = 0; i < statsText_.Length - 1; ++i)
        {
            statsText_[i].text = statsText_[i].name + ": " + playerStats_[i].ToString();
        }
    }

    //This function gets the skill names and values when the player opens the stats menu
    void PopulateSkills()
    {
        if(m_PlayerData.m_Skills.Count <= 0)
        {
            return;
        }
        else
        {
            skillString_ = "";
            for (int i = 0; i < m_PlayerData.m_Skills.Count; ++i)
            {
                skillString_ += m_PlayerData.m_Skills[i].m_Skill.ToString() + ": " + m_PlayerData.m_Skills[i].m_Amount.ToString() + "\n";
            }

            skillsText_[1].text = skillString_;
        }
    }

    //This function sets the objective sliders to their current values when the player opens the stats menu
    void UpdateSliders()
    {
        repObjSlider_.value = m_PlayerData.m_Reputation;
        currObjSlider_.value = m_PlayerData.m_Shillings;
        happyObjSlider_.value = m_PlayerData.m_Happiness;
    }

    public void CloseCurrentMenu()
    {
        if (statsActive_ || inventoryActive_ || possessionActive_ || consumableActive_)
        {
            m_CloseMenu.Play();
        }
        if(statsActive_ && !m_StatsTransitionDisplay.IsTransitioning())
        {
            statsActive_ = false;
            m_PlayerController.enabled = true;
            m_StatsTransitionDisplay.FadeOut(null);
        }
        if (inventoryActive_ && !consumableActive_ && !possessionActive_)
        {
            inventoryActive_ = false;
            m_PlayerController.enabled = true;
            m_InventoryPanel.SetActive(false);
            m_InventoryTransitionDisplay.FadeOut(null);
        }
        else if (consumableActive_ && inventoryActive_)
        {
            GetComponent<InventoryUI>().CloseInventory();
            consumableActive_ = false;
        }
        else if (possessionActive_ && inventoryActive_)
        {
            GetComponent<PossessioninventoryUI>().CloseInventory();
            possessionActive_ = false;
        }
        else if(GetComponent<BuildingUI>().BuildingUIActive)
        {
            GetComponent<BuildingUI>().CloseCurrentMenu();
        }
        else if(GetComponent<HabitatUI>().HabitatUIActive)
        {
            GetComponent<HabitatUI>().CloseCurrentUI();
        }
    }

    public void OpenBuildingsList()
    {
        if(m_TransOutBuilding.Transitioning())
        {
            return;
        }

        if(isBuildingMenuOpen_)
        {
            m_TransOutBuilding.StartTransition(delegate { CleanUpBuildingList(); });
            //CleanUpBuildingList();
            //m_BuildingMenuScrollMask.transform.parent.gameObject.SetActive(false);
            return;
        }

        isBuildingMenuOpen_ = true;
        m_BuildingMenuScrollMask.transform.parent.gameObject.SetActive(true);
        ScrollRect sRect = m_BuildingMenuScrollMask.transform.parent.gameObject.GetComponent<ScrollRect>();

        //float startYPos = (buildingObjects_.Length * subMenuYOffset_) * -0.5f;
        float startYPos = subMenuYOffset_ * -0.5f;

        RectTransform rTransfrom = m_BuildingMenuScrollMask.GetComponent<RectTransform>();
        SetHeight(rTransfrom, buildingObjects_.Length * subMenuYOffset_);

        for (int i = 0; i < buildingObjects_.Length; ++i)
        {
            GameObject go = (GameObject)Instantiate(m_BuildingButtonPrefab, new Vector3(0, startYPos, 0), Quaternion.identity);
            Button bton = go.GetComponentInChildren<Button>();
            bton.onClick.AddListener(delegate { GotoBuilding(go); });
            Text text = go.GetComponentInChildren<Text>();
            text.text = buildingObjects_[i].name;
            go.gameObject.transform.SetParent(m_BuildingMenuScrollMask.transform, false);
            startYPos -= subMenuYOffset_;
        }
    }

    void GotoBuilding(GameObject goHere)
    {
        ScrollRect sRect = m_BuildingMenuScrollMask.transform.parent.gameObject.GetComponent<ScrollRect>();
        m_TransOutBuilding.StartTransition(delegate { CleanUpBuildingList(); });

        Text text = goHere.GetComponentInChildren<Text>();
        for (int i = 0; i < buildingObjects_.Length; ++i)
        {
            if (buildingObjects_[i].name == text.text)
            {
                m_PlayerController.GotoBuilding(buildingObjects_[i]);
                break;
            }
        }
    }

    void CleanUpBuildingList()
    {
        foreach (RectTransform child in m_BuildingMenuScrollMask.transform)
        {
            GameObject.Destroy(child.gameObject);
        }
        isBuildingMenuOpen_ = false;
    }

    void SetSize(RectTransform trans, Vector2 newSize)
    {
        Vector2 oldSize = trans.rect.size;
        Vector2 deltaSize = newSize - oldSize;
        trans.offsetMin = trans.offsetMin - new Vector2(deltaSize.x * trans.pivot.x, deltaSize.y * trans.pivot.y);
        trans.offsetMax = trans.offsetMax + new Vector2(deltaSize.x * (1f - trans.pivot.x), deltaSize.y * (1f - trans.pivot.y));
    }
    public void SetHeight(RectTransform trans, float newSize)
    {
        SetSize(trans, new Vector2(trans.rect.size.x, newSize));
    }
}
