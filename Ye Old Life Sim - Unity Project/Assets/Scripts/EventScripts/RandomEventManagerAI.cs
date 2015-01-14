using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class RandomEventManagerAI : MonoBehaviour 
{   
    public GameObject[] m_EventList;
	public RandomEventBaseClass m_RandomEventBaseClass;
//	public RandomEventManager[] m_EventArray;  Try implenting this later if there's time 

//	public AIData m_AI
	public PlayerData m_AIData;
	public string m_EventDesc;
    public REvntScript m_REvent;
    public REvntScript.EventType m_CurrREvent;
    public DataCollection m_DataCollection;
	
	//public GameObject[] m_EventList;

	public float m_EventTextTimer; //Time until the event text disapears on it's own

//	public REvntScript[] eventsText_;

    private int RndEventChance_;
    private int D100_;

    // Use this for initialization
	void Start() { }

	public void PlayRandomEvent()
    {
        //First check to see if a Random event will even happen 
        RndEventChance_ = Random.Range(ValueConstants.LOWEST_RANGE_NUMBER, ValueConstants.RANDOM_EVENT_CHANCE_MAX);
        if (RndEventChance_ <= ValueConstants.RANDOM_EVENT_HAPPENS_AT_LESS_THAN_THIS)
        {
			
            //die roll to see which event happens
			D100_ = Random.Range(ValueConstants.LOWEST_RANGE_NUMBER, ValueConstants.D100_MAX);
			D100_ = 1;
			if (D100_ < ValueConstants.MAX_RANDOM_CHANCE_TO_FIND_SHILLINGS)
            {
				//Found Shillings
				m_EventDesc = m_EventList[0].GetComponent<REvntScript>().m_REvent.PlayEvent(m_AIData, m_EventDesc);
                m_CurrREvent = REvntScript.EventType.FOUND_SHILLINGS;
                m_DataCollection.AddRandomEvents();
            }
			else if (D100_ < ValueConstants.MAX_RANDOM_CHANCE_TO_BE_BITTEN_BITTEN_BY_RATS)
            {
                //Bit By Rats
				m_EventDesc = m_EventList[1].GetComponent<REvntScript>().m_REvent.PlayEvent(m_AIData, m_EventDesc);
                m_CurrREvent = REvntScript.EventType.BIT_BY_RATS;
                m_DataCollection.AddRandomEvents();
            }
            else if (D100_ < ValueConstants.MAX_RANDOM_CHANCE_TO_FIND_A_SACK)
            {
                //Find a sack
				m_EventDesc = m_EventList[2].GetComponent<REvntScript>().m_REvent.PlayEvent(m_AIData, m_EventDesc);
                m_CurrREvent = REvntScript.EventType.FIND_A_SACK;
                m_DataCollection.AddRandomEvents();
            }
            else if (D100_ < ValueConstants.MAX_RANDOM_CHANCE_TO_BE_ROBBED)
            {
				//Robbed
				m_EventDesc = m_EventList[3].GetComponent<REvntScript>().m_REvent.PlayEvent(m_AIData, m_EventDesc);
                m_CurrREvent = REvntScript.EventType.ROBBED;
                m_DataCollection.AddRandomEvents();
            }
            else if (D100_ < ValueConstants.MAX_RANDOM_CHANCE_FOR_A_TOURNAMENT)
            {
				//Tournament
				m_EventDesc = m_EventList[4].GetComponent<REvntScript>().m_REvent.PlayEvent(m_AIData, m_EventDesc);
                m_CurrREvent = REvntScript.EventType.TOURNAMENT;
                m_DataCollection.AddRandomEvents();
            }
			else if (D100_ < ValueConstants.MAX_RANDOM_CHANCE_FOR_A_WEEKEND_JOB)
			{
				//Weekend Job
				m_EventDesc = m_EventList[5].GetComponent<REvntScript>().m_REvent.PlayEvent(m_AIData, m_EventDesc);
                m_CurrREvent = REvntScript.EventType.WEEKEND_JOB;
                m_DataCollection.AddRandomEvents();
			}
			else if (D100_ < ValueConstants.MAX_RANDOM_CHANCE_FOR_KINGDOM_AT_WAR)
			{
				//Kingdom at War
				m_EventDesc = m_EventList[6].GetComponent<REvntScript>().m_REvent.PlayEvent(m_AIData, m_EventDesc);
                m_CurrREvent = REvntScript.EventType.KINGDOM_AT_WAR;
                m_DataCollection.AddRandomEvents();
			}
			else if (D100_ < ValueConstants.MAX_RANDOM_CHANCE_FOR_A_STAMPEDE)
			{
				//Stampede
				m_EventDesc = m_EventList[7].GetComponent<REvntScript>().m_REvent.PlayEvent(m_AIData, m_EventDesc);
                m_CurrREvent = REvntScript.EventType.STAMPEDE;
                m_DataCollection.AddRandomEvents();
			}
			else if (D100_ < ValueConstants.MAX_RANDOM_CHANCE_TO_FIND_STARVING_CHILD)
			{
				//Find a starving Child
				m_EventDesc = m_EventList[8].GetComponent<REvntScript>().m_REvent.PlayEvent(m_AIData, m_EventDesc);
                m_CurrREvent = REvntScript.EventType.FIND_A_STARVING_CHILD;
                m_DataCollection.AddRandomEvents();
			}
			else if (D100_ < ValueConstants.MAX_RANDOM_CHANCE_FOR_THE_PLAGUE)
			{
				//The Plague!!
				m_EventDesc = m_EventList[9].GetComponent<REvntScript>().m_REvent.PlayEvent(m_AIData, m_EventDesc);
                m_CurrREvent = REvntScript.EventType.THE_PLAGUE;
                m_DataCollection.AddRandomEvents();
			}
			else if (D100_ < ValueConstants.MAX_RANDOM_CHANCE_FOR_FOOT_AND_MOUTH_DISEASE)
			{
				//Foot and Mouth/Mad Cow Disease Disease
				m_EventDesc = m_EventList[10].GetComponent<REvntScript>().m_REvent.PlayEvent(m_AIData, m_EventDesc);
                m_CurrREvent = REvntScript.EventType.FOOT_AND_MOUTH_DISEASE;
                m_DataCollection.AddRandomEvents();
			}
			else if (D100_ < ValueConstants.MAX_RANDOM_CHANCE_FOR_EXPERIMENT_GONE_WRONG)
			{
				//Experiment Gone Wrong
				m_EventDesc = m_EventList[11].GetComponent<REvntScript>().m_REvent.PlayEvent(m_AIData, m_EventDesc);
                m_CurrREvent = REvntScript.EventType.EXPERIMENT_GONE_WRONG;
                m_DataCollection.AddRandomEvents();
			}
			else if (D100_ < ValueConstants.MAX_RANDOM_CHANCE_FOR_THE_CIRCUS)
			{
				//Circus Comes to town
				m_EventDesc = m_EventList[12].GetComponent<REvntScript>().m_REvent.PlayEvent(m_AIData, m_EventDesc);
                m_CurrREvent = REvntScript.EventType.CIRCUS_COMES_TO_TOWN;
                m_DataCollection.AddRandomEvents();
			}
			else if (D100_ <= ValueConstants.MAX_RANDOM_CHANCE_TO_WITNESS_AN_EXECUTION)
			{
				//Witness an Execution
				m_EventDesc = m_EventList[13].GetComponent<REvntScript>().m_REvent.PlayEvent(m_AIData, m_EventDesc);
                m_CurrREvent = REvntScript.EventType.WITNESS_AN_EXECUTION;
                m_DataCollection.AddRandomEvents();
			}
			else if (D100_ <= ValueConstants.MAX_RANDOM_CHANCE_TO_WITNESS_A_WITCH_BURNING)
			{
				//Witness a witch Burning
				m_EventDesc = m_EventList[14].GetComponent<REvntScript>().m_REvent.PlayEvent(m_AIData, m_EventDesc);
                m_CurrREvent = REvntScript.EventType.WITNESS_A_WITCH_BURNING;
                m_DataCollection.AddRandomEvents();
			}
			else if (D100_ <= ValueConstants.MAX_RANDOM_CHANCE_FOR_A_FIRE)
			{
				//Fire
				m_EventDesc = m_EventList[15].GetComponent<REvntScript>().m_REvent.PlayEvent(m_AIData, m_EventDesc);
                m_CurrREvent = REvntScript.EventType.FIRE;
                m_DataCollection.AddRandomEvents();
			}
			else if (D100_ <= ValueConstants.MAX_RANDOM_CHANCE_FOR_BARREL_OF_MEAD)
			{
				//Barrel of Mead falls of the wagon
				m_EventDesc = m_EventList[16].GetComponent<REvntScript>().m_REvent.PlayEvent(m_AIData, m_EventDesc);
                m_CurrREvent = REvntScript.EventType.BARREL_OF_MEAD;
                m_DataCollection.AddRandomEvents();
			}
			//pop up the pop up window

        }
		else
		{
			return;
		}
        
        //The event will pop up in a dialogue box with a button that will close the box when clicked
     
        //pass name into script and have a case statement and call it


    }
	//
}


