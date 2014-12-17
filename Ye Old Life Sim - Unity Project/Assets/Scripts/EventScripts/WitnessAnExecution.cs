using UnityEngine;
using System.Collections;

public class WitnessAnExecution : RandomEventManager
{
	public void PlayEvent()
	{
		//The player loses 5 seconds on the clock but gain -5 - 10 happiness based upon how much you liked the person.
		m_EventText.text = ".";
	}
}
