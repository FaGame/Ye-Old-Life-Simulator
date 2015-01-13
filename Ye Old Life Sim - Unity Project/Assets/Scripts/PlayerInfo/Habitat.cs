using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Habitat : MonoBehaviour 
{
    public float m_Rent = 0.0f;
    public string[] m_Description;
    public GameManager m_GameManager;

    public HabitatUI m_HabitatUI;

    public PlayerData m_PlayerData;

    private ProjectM projM_;
    private bool isHighlighted_;
    private Renderer[] renderers_;
    private List<List<Color>> originalColours_ = new List<List<Color>>();

    void Start()
    {
        CalculateHomePenalty();
        renderers_ = GetComponentsInChildren<Renderer>();

        for (int i = 0; i < renderers_.Length; ++i)
        {
            originalColours_.Add(new List<Color>());
            for (int j = 0; j < renderers_[i].renderer.materials.Length; ++j)
            {
                originalColours_[i].Add(renderers_[i].renderer.materials[j].color);
            }
            //originalColours_.Add(renderers_[i].renderer.material.color);        
        }
    }

    void Update()
    {
        if (m_GameManager.AITurn)
        {
            m_PlayerData = m_GameManager.m_AIData;
        }
        else if (m_GameManager.PlayerTurn)
        {
            m_PlayerData = m_GameManager.m_PlayerData;
        }
        else if (m_GameManager.PlayerTwoTurn)
        {
            m_PlayerData = m_GameManager.m_PlayerTwoData;
        } 
    }

    public enum BuildingRating
    {
        NOSTAR = 0,
        ONESTAR = 1,
        TWOSTAR = 2,
        THREESTAR = 3,
        FOURSTAR = 4,
        FIVESTAR = 5
    };
    public BuildingRating m_Rating = BuildingRating.NOSTAR;
    public BuildingRating m_CollectionRating;

    public float CalculateHomePenalty()
    {
        switch (m_Rating)
        {
            case BuildingRating.NOSTAR:
                m_Rent = 0.0f;
                return ValueConstants.ZERO_STAR_HABITAT_PENALTY;

            case BuildingRating.ONESTAR:
                m_Rent = 5.0f;
                return ValueConstants.ONE_STAR_HABITAT_PENALTY;

            case BuildingRating.TWOSTAR:
                m_Rent = 10.0f;
                return ValueConstants.TWO_STAR_HABITAT_PENALTY;

            case BuildingRating.THREESTAR:
                m_Rent = 25.0f;
                return ValueConstants.THREE_STAR_HABITAT_PENALTY;

            case BuildingRating.FOURSTAR:
                m_Rent = 50.0f;
                return ValueConstants.FOUR_STAR_HABITAT_PENALTY;

            case BuildingRating.FIVESTAR:
                m_Rent = 100.0f;
                return ValueConstants.FIVE_STAR_HABITAT_PENALTY;
        }
        return 0;
    }

    public string GetDescription()
    {
        return m_Description[Random.Range(0, m_Description.Length)];
    }

    public void SetHome()
    {
        if(m_PlayerData.m_Home.m_Rating == null)
        {
            Debug.Log("No Habitat set, This should not happen");
        }

        if(m_PlayerData.m_Home.m_Rating != null)
        {
            m_HabitatUI.SetHabitat();
        }
    }


    void OnMouseEnter()
    {
        //projM_.TurnOn(gameObject);

        /*for (int i = 0; i < GetComponentsInChildren<Renderer>().Length; ++i)
        {
            for (int j = 0; j < GetComponentsInChildren<Renderer>()[i].renderer.materials.Length; ++j)
            {
                GetComponentsInChildren<Renderer>()[i].renderer.materials[j].color = Color.yellow;
            }
            //m_DisplayedBuilding.GetComponentsInChildren<Renderer>()[i].renderer.material.color = Color.yellow;
        }*/
    }

    void OnMouseExit()
    {
        //projM_.TurnOff(gameObject);

        /*
        for (int i = 0; i < GetComponentsInChildren<Renderer>().Length; ++i)
        {
           for (int j = 0; j < renderers_[i].renderer.materials.Length; ++j)
            {
                GetComponentsInChildren<Renderer>()[i].renderer.materials[j].color = originalColours_[i][j];
                //originalColours_[i].Add(renderers_[i].renderer.materials[j].color);
            }
            //m_DisplayedBuilding.GetComponentsInChildren<Renderer>()[i].renderer.material.color = originalColours_[i];
        }*/
    }
}
