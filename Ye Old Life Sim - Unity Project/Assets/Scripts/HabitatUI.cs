using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HabitatUI : MonoBehaviour 
{
    public PlayerData m_Habitat;
    public Habitat m_RentVal;

    public Text m_RentValue;
    public Text m_TimePenalty;

    public float timePenalty_;
    public float rentValue_;

    public void FixedUpdate()
    {
        m_TimePenalty.text = m_Habitat.m_Home.m_Rating.ToString();
        m_RentValue.text = m_RentVal.m_Rent.ToString();
    }
}
