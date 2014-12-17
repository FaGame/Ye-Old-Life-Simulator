using UnityEngine;
using System.Collections;

public class WitnessAWitchBurning : RandomEventManager
{
	public void PlayEvent()
	{
		//The player loses 10 seconds on the clock but gain 5 - 20 happiness seeing justice being carried out.
		m_EventText.text = ".";
	}
}
