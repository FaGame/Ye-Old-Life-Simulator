using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[RequireComponent(typeof(Canvas))]
public class HUDScript : MonoBehaviour 
{
    public Canvas m_PlayerHUD;
    public GameObject m_Stats;
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

public float m_Currency = 0.0f;
public float m_Happiness = 0.0f;
public float m_HabitatRating = 0.0f;
public float m_Reputation = 0.0f;
    //-------------END TEMP CODE-------------

    private float timer_; //Turn timer
    private Slider[] sliderArray_; //Array of sliders since the HUD has numerous
    private Slider timeSlider_; //Slider for the time left in your turn
    private Slider hungerSlider_; //Slider for the amount of player's hunger

	// Use this for initialization
	void Start () 
    {
        sliderArray_ = m_PlayerHUD.GetComponentsInChildren<Slider>();
        timeSlider_ = sliderArray_[0];
        hungerSlider_ = sliderArray_[1];

        timer_ = m_MaxTime;

        SetUpSliders();
	}

    void SetUpSliders()
    {
        timeSlider_.maxValue = m_MaxTime;
        timeSlider_.minValue = m_MinTime;
        timeSlider_.value = timer_;

        hungerSlider_.maxValue = m_MaxHunger;
        hungerSlider_.minValue = m_MinHunger;
        hungerSlider_.value = m_DefaultStartHunger;
    }
	
	// Update is called once per frame
	void Update ()
    {
        timer_--;
        timeSlider_.value = timer_;
        hungerSlider_.value = m_CurrHunger;
        if(timer_ <= m_MinTime)
        {
            m_CurrHunger += 10.0f;
            timer_ = m_MaxTime;
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
            m_Stats.SetActive(true);
        }
    }
}
