using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HabitatUI : MonoBehaviour 
{
    private PlayerData m_Habitat;
    private Habitat m_RentVal;

    public Text m_RentValue;
    public Text m_TimePenalty;

    private float timePenalty_;
    private float rentValue_;

    public void FixedUpdate()
    {
        m_TimePenalty.text = m_Habitat.m_Home.m_Rating.ToString();
        m_RentValue.text = m_RentVal.m_Rent.ToString();
    }
}
