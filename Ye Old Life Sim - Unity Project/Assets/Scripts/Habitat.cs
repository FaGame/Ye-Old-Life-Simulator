using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Habitat : MonoBehaviour 
{
    public float m_Rent = 0.0f;
    public string[] m_Description;

    public HabitatUI m_HabitatUI;

    public bool m_PlayerLivesHere;


    void Start()
    {
        m_PlayerLivesHere = false;
        CalculateHomePenalty();
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

    public float CalculateHomePenalty()
    {
        switch (m_Rating)
        {
            case BuildingRating.NOSTAR:
                return ValueConstants.ZERO_STAR_HABITAT_PENALTY;

            case BuildingRating.ONESTAR:
                return ValueConstants.ONE_STAR_HABITAT_PENALTY;

            case BuildingRating.TWOSTAR:
                return ValueConstants.TWO_STAR_HABITAT_PENALTY;

            case BuildingRating.THREESTAR:
                return ValueConstants.THREE_STAR_HABITAT_PENALTY;

            case BuildingRating.FOURSTAR:
                return ValueConstants.FOUR_STAR_HABITAT_PENALTY;

            case BuildingRating.FIVESTAR:
                return ValueConstants.FIVE_STAR_HABITAT_PENALTY;
        }
        return 0;
    }

    /*public string GetDescription()
    {
        return m_Description[Random.Range(0, m_Description.Length)];
    }*/


}
