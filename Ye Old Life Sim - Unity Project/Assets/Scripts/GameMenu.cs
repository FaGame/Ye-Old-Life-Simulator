using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(Canvas))]
public class GameMenu : MonoBehaviour 
{
    public Canvas m_GameMenu;

    public Slider m_repObjSlider;
    public Slider m_currencyObjSlider;
    public Slider m_happyObjSlider;
    public Slider m_turnsSlider;

    public Toggle m_playersOne;
    public Toggle m_playersTwo;

    public Toggle m_Objectives;
    public Toggle m_Turns;
    public Toggle m_Sandbox;

    public GameObject m_objectivesSliders;
    public GameObject m_turnSlider;

    public Text m_currencyText;
    public Text m_repText;
    public Text m_happyText;
    public Text m_turnsText;

    public static float m_selectedRep;
    public static float m_selectedCurrency;
    public static float m_selectedHappiness;
    public static float m_selectedTurns;
    public static float m_Players;

    private string currTextString_;
    private string repTextString_;
    private string happyTextString_;
    private string turnsTextString_;

    public static bool m_isObjectivesGame = false;
    public static bool m_isTurnGame = false;
    public static bool m_isSandboxGame = false;

    void Update()
    {
        SetValues();
        SetToggleValues();
        ToggleSlidersOnOff();
        SetPanelTexts();
        SetGameType();
    }

    void SetValues()
    {
        m_selectedRep = m_repObjSlider.value;
        m_selectedCurrency = m_currencyObjSlider.value;
        m_selectedHappiness = m_happyObjSlider.value;
        m_selectedTurns = m_turnsSlider.value;
    }

    void SetToggleValues()
    {
        if(m_playersOne.isOn)
        {
            m_Players = 1;
        }
        //--------------------
        if(m_playersTwo.isOn)
        {
            m_Players = 2;
        }
    }

    void ToggleSlidersOnOff()
    {
        if(m_Objectives.isOn)
        {
            m_objectivesSliders.SetActive(true);
        }
        else
        {
            m_objectivesSliders.SetActive(false);
        }
        //---------------------------------------
        if(m_Turns.isOn)
        {
            m_turnSlider.SetActive(true);
        }
        else
        {
            m_turnSlider.SetActive(false);
        }
    }

    void SetPanelTexts()
    {
        currTextString_ = "" + m_currencyObjSlider.value;
        m_currencyText.text = currTextString_;

        repTextString_ = "" + m_repObjSlider.value;
        m_repText.text = repTextString_;

        happyTextString_ = "" + m_happyObjSlider.value;
        m_happyText.text = happyTextString_;

        turnsTextString_ = "" + m_turnsSlider.value;
        m_turnsText.text = turnsTextString_;


    }

    public void Play()
    {
        //Application.LoadLevel(1);
    }

    void SetGameType()
    {
        if(m_Sandbox.isOn == true)
        {
            m_isSandboxGame = true;
        }

        if(m_Turns.isOn == true)
        {
            m_isTurnGame = true;
        }

        if(m_Objectives.isOn == true)
        {
            m_isObjectivesGame = true;
        }
    }
}
