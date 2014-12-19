using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class RandomEventManager : MonoBehaviour
{
    public int m_RndEvent;
    public int m_EventIndex;
	public int m_MoneyLost;
	public int m_MoneyGained;
	public float m_ExperienceGained;
	public float m_HappinesChange;
	public float m_TimeChange;

	public PlayerData m_Player;
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
        if (RndEventChance_ <= 51)
        {
			
            //die roll to see which event happens
            D100_ = Random.Range(1, 101);

            if(D100_ <= 6)
            {
				//Found Shillings
				m_EventDesc = m_EventList[0].GetComponent<FoundShillings>().PlayEvent(m_Player, m_EventDesc);
            }
            else if (D100_ <= 11)
            {
                //Bit By Rats
				m_EventDesc = m_EventList[1].GetComponent<BitByRats>().PlayEvent(m_Player, m_EventDesc);
	//			m_EventText.text = GetComponent<BitByRats>().m_EventText.text;
            }
            else if (D100_ <= 21)
            {
                //Find a sack
				m_EventDesc = m_EventList[2].GetComponent<FindASack>().PlayEvent(m_Player, m_EventDesc);
            }
            else if (D100_ <= 26)
            {
				//Robbed
				m_EventDesc = m_EventList[3].GetComponent<Robbed>().PlayEvent(m_Player, m_EventDesc);
            }
            else if (D100_ <= 31)
            {
				//Tournament
				m_EventDesc = m_EventList[4].GetComponent<Tournament>().PlayEvent(m_Player, m_EventDesc);
            }
			else if (D100_ <= 41)
			{
				//Weekend Job
				m_EventDesc = m_EventList[5].GetComponent<WeekendJob>().PlayEvent(m_Player, m_EventDesc);
			}
			else if (D100_ <= 46)
			{
				//Kingdom at War
				m_EventDesc = m_EventList[6].GetComponent<KingdomAtWar>().PlayEvent(m_Player, m_EventDesc);
			}
			else if (D100_ <= 51)
			{
				//Stampede
				m_EventDesc = m_EventList[7].GetComponent<Stampede>().PlayEvent(m_Player, m_EventDesc);
			}
			else if (D100_ <= 56)
			{
				//Find a starving Child
				m_EventDesc = m_EventList[8].GetComponent<FindAStarvingChild>().PlayEvent(m_Player, m_EventDesc);
			}
			else if (D100_ <= 61)
			{
				//The Plague!!
				m_EventDesc = m_EventList[9].GetComponent<ThePlague>().PlayEvent(m_Player, m_EventDesc);
			}
			else if (D100_ <= 66)
			{
				//Foot and Mouth/Mad Cow Disease Disease
				m_EventDesc = m_EventList[10].GetComponent<FootAndMouth>().PlayEvent(m_Player, m_EventDesc);
			}
			else if (D100_ <= 71)
			{
				//Experiment Gone Wrong
				m_EventDesc = m_EventList[11].GetComponent<ExperimentGoneWrong>().PlayEvent(m_Player, m_EventDesc);
			}
			else if (D100_ <= 76)
			{
				//Circus Comes to town
				m_EventDesc = m_EventList[12].GetComponent<CircusComesToTown>().PlayEvent(m_Player, m_EventDesc);
			}
			else if (D100_ <= 86)
			{
				//Witness an Execution
				m_EventDesc = m_EventList[13].GetComponent<WitnessAnExecution>().PlayEvent(m_Player, m_EventDesc);
			}
			else if (D100_ <= 91)
			{
				//Witness a witch Burning
				m_EventDesc = m_EventList[14].GetComponent<WitnessAWitchBurning>().PlayEvent(m_Player, m_EventDesc);
			}
			else if (D100_ <= 96)
			{
				//Fire
				m_EventDesc = m_EventList[15].GetComponent<Fire>().PlayEvent(m_Player, m_EventDesc);
			}
			else if (D100_ <= 101)
			{
				//Barrel of Mead falls of the wagon
				m_EventDesc = m_EventList[16].GetComponent<BarrelOfMead>().PlayEvent(m_Player, m_EventDesc);
			}
			
			m_EventText.text = m_EventDesc;
			//pop up the pop up window

        }
		else
		{
			m_EventText.text = "Nothing Interesting Today.";
			return;
		}
        
        //The event will pop up in a dialogue box with a "OK" button that will close the box when clicked
     
        //pass name into script and have a case statement and call it


    }
	//
}
