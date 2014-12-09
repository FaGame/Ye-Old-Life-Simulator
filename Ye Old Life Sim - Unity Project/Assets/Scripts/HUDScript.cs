using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(Canvas))]
public class HUDScript : MonoBehaviour 
{
    public Canvas m_PlayerHUD;
    public GameObject m_Stats;
    public GameObject m_Goals;
    public GameObject m_BuildingGUI;
    //public Player m_Player;

    //All values in the following temp code should be set from the player rather than in here
    //This will be all plugged in once the player is in a state where this can happen
    //-------------TEMP CODE-------------
public float m_MaxTime; //Max time in a turn                        
public float m_MinTime = 0.0f; //Time that denotes end of turn
public float m_MaxHunger = 100.0f; //The hungriest the player can be
public float m_MinHunger = 0.0f; //When the player is full
public float m_DefaultStartHunger = 0.0f; //Where the hunger starts at the beginning of the game
public float m_CurrHunger = 0.0f; //Current level of hunger

public float m_Currency = 10000.0f;
public float m_Happiness = 40.0f;
public float m_HabitatRating = 1.0f;
public float m_Reputation = 10.0f;

public float m_HabitatObjective;
public float m_HungerObjective;
public float m_ReputationObjective;
public float m_CurrencyObjective;
public float m_HappinessObjective;

private bool statsActive_ = false;
private bool buildingsActive_ = false;

private float timer_; //Turn timer
private Slider[] sliderArray_; //Array of sliders since the HUD has numerous
private Slider timeSlider_; //Slider for the time left in your turn
private Slider hungerSlider_; //Slider for the amount of player's hunger

private Slider[] objSliderArray_;
private Slider habitatObjSlider_;
private Slider hungerObjSlider_;
private Slider repObjSlider_;
private Slider currObjSlider_;
private Slider happyObjSlider_;

private Text[] textArray_;
private List<float> playerStats_ = new List<float>();
    //-------------END TEMP CODE-------------

   

	// Use this for initialization
	void Start () 
    {
        sliderArray_ = m_PlayerHUD.GetComponentsInChildren<Slider>();
        SetUpHUDSliders();

        objSliderArray_ = m_Goals.GetComponentsInChildren<Slider>();
        SetUpObjectiveSliders();

        playerStats_.Add(m_HabitatRating);
        playerStats_.Add(m_CurrHunger);
        playerStats_.Add(m_Reputation);
        playerStats_.Add(m_Currency);
        playerStats_.Add(m_Happiness);
        
        textArray_ = m_Stats.GetComponentsInChildren<Text>();
        m_Stats.SetActive(false);
        m_Goals.SetActive(false);
        m_BuildingGUI.SetActive(false);
        timer_ = m_MaxTime;
	}

    void SetUpHUDSliders()
    {
        timeSlider_ = sliderArray_[0];
        hungerSlider_ = sliderArray_[1];

        timeSlider_.maxValue = m_MaxTime;
        timeSlider_.minValue = m_MinTime;
        timeSlider_.value = timer_;

        hungerSlider_.maxValue = m_MaxHunger;
        hungerSlider_.minValue = m_MinHunger;
        hungerSlider_.value = m_DefaultStartHunger;
    }

    void SetUpObjectiveSliders()
    {
        habitatObjSlider_ = objSliderArray_[0];
        hungerObjSlider_ = objSliderArray_[1];
        repObjSlider_ = objSliderArray_[2];
        currObjSlider_ = objSliderArray_[3];
        happyObjSlider_ = objSliderArray_[4];

        habitatObjSlider_.maxValue = m_HabitatObjective;
        habitatObjSlider_.minValue = 0.0f;

        hungerObjSlider_.maxValue = m_HungerObjective;
        hungerObjSlider_.minValue = 0.0f;

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
        timer_--;
        timeSlider_.value = timer_;
        hungerSlider_.value = m_CurrHunger;
        if (timer_ <= m_MinTime)
        {
            m_CurrHunger += 10.0f;
            timer_ = m_MaxTime;
        }

        if (m_CurrHunger >= m_MaxHunger)
        {
            m_CurrHunger = m_MaxHunger;
        }
	}

    void FixedUpdate()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            m_CurrHunger -= 10.0f;
        }

        if(Input.GetKeyDown(KeyCode.E))
        {
            statsActive_ = !statsActive_;
            m_Stats.SetActive(statsActive_);
            m_Goals.SetActive(statsActive_);
            PopulateStats();
            UpdateSliders();
        }
    }

    void PopulateStats()
    {
        //Clear the list and re-add the variables to make sure the information is completely up to date
        playerStats_.Clear();
        playerStats_.Add(m_HabitatRating);
        playerStats_.Add(m_CurrHunger);
        playerStats_.Add(m_Reputation);
        playerStats_.Add(m_Currency);
        playerStats_.Add(m_Happiness);

        //Loop through the pair of arrays to get the appropriate values next to the proper text
        for (int i = 0; i < textArray_.Length - 1; ++i)
        {
            textArray_[i].text = textArray_[i].name + ": " + playerStats_[i].ToString();
        }
    }

    void UpdateSliders()
    {
        habitatObjSlider_.value = m_HabitatRating;
        hungerObjSlider_.value = m_CurrHunger;
        repObjSlider_.value = m_Reputation;
        currObjSlider_.value = m_Currency;
        happyObjSlider_.value = m_Happiness;
    }

    public void BuildingGUI()
    {
        buildingsActive_ = !buildingsActive_;
        m_BuildingGUI.SetActive(buildingsActive_);
    }
}
