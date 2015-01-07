using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class REvntScript : MonoBehaviour 
{
//	public PlayerData m_Player;
//	public string m_EventDesc;

	public enum EventType
	{
		FOUND_SHILLINGS = 0,
		BIT_BY_RATS,
		FIND_A_SACK,
		ROBBED,
		TOURNAMENT,
		WEEKEND_JOB,
		KINGDOM_AT_WAR,
		STAMPEDE,
		FIND_A_STARVING_CHILD,
		THE_PLAGUE,
		FOOT_AND_MOUTH_DISEASE,
		EXPERIMENT_GONE_WRONG,
		CIRCUS_COMES_TO_TOWN,
		WITNESS_AN_EXECUTION,
		WITNESS_A_WITCH_BURNING,
		FIRE,
		BARREL_OF_MEAD,
	}

	public RandomEventBaseClass m_REvent;
	public EventType m_EventType;

	public void Start()
	{
		switch (m_EventType)
		{
			case EventType.FOUND_SHILLINGS:
				m_REvent = new FoundShillings();
					//.PlayEvent(m_Player, m_EventDesc);
				break;

			case EventType.BIT_BY_RATS:
				m_REvent = new BitByRats();
				break;

			case EventType.FIND_A_SACK:
				m_REvent = new FindASack();
				break;

			case EventType.ROBBED:
				m_REvent = new Robbed();
				break;

			case EventType.TOURNAMENT:
				m_REvent = new Tournament();
				break;

			case EventType.WEEKEND_JOB:
				m_REvent = new WeekendJob();
				break;

			case EventType.KINGDOM_AT_WAR:
				m_REvent = new KingdomAtWar();
				break;

			case EventType.STAMPEDE:
				m_REvent = new Stampede();
				break;

			case EventType.FIND_A_STARVING_CHILD:
				m_REvent = new FindAStarvingChild();
				break;

			case EventType.THE_PLAGUE:
				m_REvent = new ThePlague();
				break;

			case EventType.FOOT_AND_MOUTH_DISEASE:
				m_REvent = new FootAndMouth();
				break;

			case EventType.EXPERIMENT_GONE_WRONG:
				m_REvent = new ExperimentGoneWrong();
				break;

			case EventType.CIRCUS_COMES_TO_TOWN:
				m_REvent = new CircusComesToTown();
				break;

			case EventType.WITNESS_AN_EXECUTION:
				m_REvent = new WitnessAnExecution();
				break;

			case EventType.WITNESS_A_WITCH_BURNING:
				m_REvent = new WitnessAWitchBurning();
				break;

			case EventType.FIRE:
				m_REvent = new Fire();
				break;

			case EventType.BARREL_OF_MEAD:
				m_REvent = new BarrelOfMead();
				break;
		}
	}
	//
}
