using UnityEngine;
using System.Collections;

public class ClockAnimator : MonoBehaviour 
{
	public Transform m_ClockHand; //The "bottom" of the clock hand
	public PlayerData m_TurnTimer; //The current Time the player has left in their turn
	private const float m_Time = ValueConstants.CLOCK_DEGREES_AND_OFFSET / ValueConstants.PLAYER_MAX_TIME; //45.0f; //degrees that the hand can be rotated by the maxTime the player has per turn

	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		float clockTime = m_TurnTimer.GetComponent<PlayerData>().m_CurrTime; //get temp variable for the clock to use
		m_ClockHand.localRotation = Quaternion.Euler(ValueConstants.CLOCK_DEGREES_AND_OFFSET, 0f, (clockTime * -m_Time) - ValueConstants.CLOCK_DEGREES_AND_OFFSET); //rotate the clock hand according to the amount of time left in the turn with an offset to position it to the correct start
	}
}
