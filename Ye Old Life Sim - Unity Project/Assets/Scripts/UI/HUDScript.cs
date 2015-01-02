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
    public PlayerData m_PlayerData; //The player's data
    public Text m_CurrJobText; //The player's current job text
    public Text m_ShillingText; //The player's current shillings
    public Text m_BuildingHovered; //The building the player is currently hovering
    public TransitionDisplay m_StatsTransitionDisplay;

    private bool statsActive_ = false; //This bool determines whether or not the the stats window is open
    private bool inventoryActive_ = false; //This bool determines whether or not the inventory is currently open
    private bool HUDActive_ = false; //This bool determines whether any HUD elements are active
    private bool wasHighlighted_ = false;
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

    public bool HUDActive
    {
        get { return HUDActive_; }
    }

    //All values in the following temp code should be set from the player rather than in here
    //This will be all plugged in once the player is in a state where this can happen
    //-------------TEMP CODE-------------
public float m_ReputationObjective;
public float m_CurrencyObjective;
public float m_HappinessObjective;
    //-------------END TEMP CODE------------- 

	// Use this for initialization
	void Start () 
    {
        //Initialize and set up the slider arrays
        sliderArray_ = m_PlayerHUD.GetComponentsInChildren<Slider>();
        SetUpHUDSliders();

        objSliderArray_ = m_Goals.GetComponentsInChildren<Slider>();
        SetUpObjectiveSliders();

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

        repObjSlider_.maxValue = m_ReputationObjective;
        repObjSlider_.minValue = 0.0f;

        currObjSlider_.maxValue = m_CurrencyObjective;
        currObjSlider_.minValue = 0.0f;

        happyObjSlider_.maxValue = m_HappinessObjective;
        happyObjSlider_.minValue = 0.0f;
    }
	
	// Update is called once per frame
	void Update ()
    {
        if(m_GameManager.AITurn == true)
        {
            m_PlayerData = m_GameManager.m_AIData;
        }
        else if(m_GameManager.PlayerTurn == true)
        {
            m_PlayerData = m_GameManager.m_PlayerData;
        }

        if (statsActive_ || inventoryActive_)
        {
            HUDActive_ = true;
        }
        else
        {
            HUDActive_ = false;
        }

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

        buildingHoverRay_ = Camera.main.ScreenPointToRay(Input.mousePosition);

        if(Physics.Raycast(buildingHoverRay_, out buildingRayHit_))
        {
            highlightedObject_ = buildingRayHit_.collider.gameObject;
            if (highlightedObject_.GetComponent<Building>() || highlightedObject_.GetComponent<Habitat>())
            {
                m_BuildingHovered.text = highlightedObject_.name;
                /*for (int i = 0; i < highlightedObject_.GetComponentsInChildren<Renderer>().Length; ++i)
                {
                    originalColours_.Add(highlightedObject_.GetComponentsInChildren<Renderer>()[i].renderer.material.color);
                    highlightedObject_.GetComponentsInChildren<Renderer>()[i].renderer.material.color = Color.yellow;
                }
                wasHighlighted_ = true;*/
            }
            else if(wasHighlighted_)
            {
                /*for(int i = 0; i < originalColours_.Count; ++i)
                {

                }*/
                m_BuildingHovered.text = "";
            }
        }
	}

    //Button function - Opens the stats screen on press
    public void OpenStatsMenu()
    {
        m_JournalSound.Play();
        statsActive_ = true;
        //m_StatsScreen.SetActive(statsActive_);
        m_StatsTransitionDisplay.FadeIn();
        PopulateStats();
        PopulateSkills();
        UpdateSliders();
    }

    //Button function - Closes the stats screen on press
    public void CloseMenu()
    {
        m_CloseMenu.Play();
        statsActive_ = false;
        //m_StatsScreen.SetActive(statsActive_);
        m_StatsTransitionDisplay.FadeOut(null);
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

    void OnMouseOver()
    {

    }
}
