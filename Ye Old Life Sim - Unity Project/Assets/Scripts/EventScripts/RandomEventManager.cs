using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class RandomEventManager : MonoBehaviour
{
    public int m_RndEvent;
    public int m_EventIndex;

    public GameObject[] m_EventList;

	public Canvas m_EventFlyer; //Canvas for the text to appear
	public Text m_EventText; //Text that displays when a radom event occurs
	public string m_EventDesc;
	//public GameObject[] m_EventList;

	public float m_EventTextTimer; //Time until the event text disapears on it's own

//	public REvntScript[] eventsText_;

    private int RndEventChance_;
    private int D100_;

    // Use this for initialization
    void Start()
    {
        //First check to see if a Random event will even happen 
        RndEventChance_ = Random.Range(1, 101);
        if (D100_ >= 50)
        {
            //die roll to see which event happens
            D100_ = Random.Range(1, 101);
            if(D100_ <= 6)
            {
                GetComponentInChildren<FoundShillings>().PlayEvent();
				//m_EventText.text = GetComponent<FoundShillings>().m_EventText.text;
				
            }
            else if (D100_ <= 11)
            {
                //Bit By Rats
                GetComponentInChildren<BitByRats>().PlayEvent();
				m_EventText.text = GetComponent<BitByRats>().m_EventText.text;
            }
            else if (D100_ <= 21)
            {
                //Find a sack
                GetComponentInChildren<FindASack>().PlayEvent();
				m_EventText.text = GetComponent<FindASack>().m_EventText.text;
            }
            else if (D100_ <= 26)
            {
				//Robbed
                GetComponentInChildren<Robbed>().PlayEvent();
				m_EventText.text = GetComponent<Robbed>().m_EventText.text;
            }
            else if (D100_ <= 31)
            {
				//Tournament
                GetComponentInChildren<Tournament>().PlayEvent();
				m_EventText.text = GetComponent<Tournament>().m_EventText.text;
            }
			else if (D100_ <= 41)
			{
				//Weekend Job
				GetComponentInChildren<WeekendJob>().PlayEvent();
				m_EventText.text = GetComponent<WeekendJob>().m_EventText.text;
			}
			else if (D100_ <= 46)
			{
				//Kingdom at War
				GetComponentInChildren<KingdomAtWar>().PlayEvent();
				m_EventText.text = GetComponent<KingdomAtWar>().m_EventText.text;
			}
			else if (D100_ <= 51)
			{
				//Stampede
				GetComponentInChildren<Stampede>().PlayEvent();
				m_EventText.text = GetComponent<Stampede>().m_EventText.text;
			}
			else if (D100_ <= 56)
			{
				//Find a starving Child
				GetComponentInChildren<FindAStarvingChild>().PlayEvent();
				m_EventText.text = GetComponent<FindAStarvingChild>().m_EventText.text;
			}
			else if (D100_ <= 61)
			{
				//The Plague!!
				GetComponentInChildren<ThePlague>().PlayEvent();
				m_EventText.text = GetComponent<ThePlague>().m_EventText.text;
			}
			else if (D100_ <= 66)
			{
				//Foot and Mouth/Mad Cow Disease Disease
				GetComponentInChildren<FootAndMouth>().PlayEvent();
				m_EventText.text = GetComponent<FootAndMouth>().m_EventText.text;
			}
			else if (D100_ <= 71)
			{
				//Experiment Gone Wrong
				GetComponentInChildren<ExperimentGoneWrong>().PlayEvent();
				m_EventText.text = GetComponent<ExperimentGoneWrong>().m_EventText.text;
			}
			else if (D100_ <= 76)
			{
				//Circus Comes to town
				GetComponentInChildren<CircusComesToTown>().PlayEvent();
				m_EventText.text = GetComponent<CircusComesToTown>().m_EventText.text;
			}
			else if (D100_ <= 86)
			{
				//Witness an Execution
				GetComponentInChildren<WitnessAnExecution>().PlayEvent();
				m_EventText.text = GetComponent<WitnessAnExecution>().m_EventText.text;
			}
			else if (D100_ <= 91)
			{
				//Witness a witch Burning
				GetComponentInChildren<WitnessAWitchBurning>().PlayEvent();
				m_EventText.text = GetComponent<WitnessAWitchBurning>().m_EventText.text;
			}
			else if (D100_ <= 96)
			{
				//Fire
				GetComponentInChildren<Fire>().PlayEvent();
				m_EventText.text = GetComponent<Fire>().m_EventText.text;
			}
			else if (D100_ <= 101)
			{
				//Barrel of Mead falls of the wagon
				GetComponentInChildren<BarrelOfMead>().PlayEvent();
				m_EventText.text = GetComponent<BarrelOfMead>().m_EventText.text;
			}
			m_EventText.text = m_EventDesc;
			//pop up the pop up window

        }
        
        //The event will pop up in a dialogue box with a "OK" button that will close the box when clicked
     
        //pass name into script and have a case statement and call it


    }

    void PlayEvent()
    {
        //GetComponentsInChildren<RandomEventList>().GetValue
    }

    // Update is called once per frame
    void Update()
    {

    }

}
