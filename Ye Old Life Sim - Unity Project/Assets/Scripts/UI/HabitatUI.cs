using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HabitatUI : MonoBehaviour 
{
    public GameObject m_HabitatGUI;
    public Button m_RentButton;
    public Text m_RentValue;//the cost of current dwelling
    public Text m_TimePenalty;//the time penalty of current dwelling
    public PlayerData m_PlayerData;
    public TransitionDisplay m_TransitionDisplay;
    public DataCollection m_DataCollection;

    private Habitat m_Habitat;
    private GameObject selectedHome_;//current habitat highlighted
    private GameObject CurrHome_;
    private Text[] habitatText_;
    private Text descriptionText_;
    private Text habitatNameText_; //Text component for the name of the habitat
    private PlayerController playerController_;

    private float timePenalty_;//actual value of current dwelling
    private float rentValue_;//actual value of current dwelling

    private bool habitatIsActive_ = false;

    public bool HabitatUIActive
    {
        get { return habitatIsActive_; }
    }

    void Start()
    {
        habitatText_ = m_HabitatGUI.GetComponentsInChildren<Text>();
        habitatNameText_ = habitatText_[0];
        descriptionText_ = habitatText_[1];
        m_HabitatGUI.SetActive(false);
    }

    public void Update()
    {
        //m_HabitatGUI.SetActive(habitatIsActive_);

        if(selectedHome_ != null)
        {
            timePenalty_ = (float)selectedHome_.GetComponent<Habitat>().m_Rating;
            rentValue_ = selectedHome_.GetComponent<Habitat>().m_Rent;

            m_RentValue.text = rentValue_.ToString();
            m_TimePenalty.text = timePenalty_.ToString();
        }
    }

    public int ReturnHabitat()
    {
        switch (m_Habitat.m_CollectionRating)
        {
            case Habitat.BuildingRating.ONESTAR:
                return (int)Habitat.BuildingRating.ONESTAR;
            case Habitat.BuildingRating.TWOSTAR:
                return (int)Habitat.BuildingRating.TWOSTAR;
            case Habitat.BuildingRating.THREESTAR:
                return (int)Habitat.BuildingRating.THREESTAR;
            case Habitat.BuildingRating.FOURSTAR:
                return (int)Habitat.BuildingRating.FOURSTAR;
            case Habitat.BuildingRating.FIVESTAR:
                return (int)Habitat.BuildingRating.FIVESTAR;
        }
        return 0;
    }

    public void LoadHabitatData(PlayerController pController, GameObject gObj)
    {
        //////////////////////////////////////////////////////////////////////////////////////
        m_Habitat = gObj.GetComponent<Habitat>();
        m_Habitat.m_CollectionRating = gObj.GetComponent<Habitat>().m_Rating;
        m_DataCollection.AddHabitats();
        //////////////////////////////////////////////////////////////////////////////////////
        habitatIsActive_ = true;
        m_TransitionDisplay.FadeIn();
        selectedHome_ = gObj;
        habitatNameText_.text = selectedHome_.GetComponent<Habitat>().name;
        descriptionText_.text = selectedHome_.GetComponent<Habitat>().GetDescription();

        if(selectedHome_.GetComponent<Habitat>() != m_PlayerData.GetComponent<Habitat>())
        {
            m_RentButton.interactable = true;
        }
        else
        {
            m_RentButton.interactable = false;
        }

        playerController_ = pController;
    }

    public void DisplayRentValue()
    {
        m_RentValue.text = rentValue_.ToString();
    }

    public void DisplayTimePenalty()
    {
        m_TimePenalty.text = timePenalty_.ToString();
    }

    public void SetHabitat()
    {
        m_PlayerData.GetComponent<PlayerData>().m_Home = selectedHome_.GetComponent<Habitat>();
        Debug.Log("Player is now renting " + selectedHome_);
    }

    public void CloseCurrentUI()
    {
        habitatIsActive_ = false;
        m_TransitionDisplay.FadeOut(null);
        playerController_.enabled = true;
    }
}
