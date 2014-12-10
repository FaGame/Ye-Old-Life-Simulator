using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Habitat : MonoBehaviour 
{ 
    public float m_Penalty = 0.0f;
    public float m_Rent = 0.0f;
    void Start()
    {
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

    public void CalculateHomePenalty()
    {
        switch (m_Rating)
        {
            case BuildingRating.NOSTAR:
                m_Penalty = 10.0f;
                break;

            case BuildingRating.ONESTAR:
                m_Penalty = 8.0f;
                break;

            case BuildingRating.TWOSTAR:
                m_Penalty = 6.0f;
                break;

            case BuildingRating.THREESTAR:
                m_Penalty = 4.0f;
                break;

            case BuildingRating.FOURSTAR:
                m_Penalty = 2.0f;
                break;

            case BuildingRating.FIVESTAR:
                m_Penalty = 0.0f;
                break;
        }
    }
}
