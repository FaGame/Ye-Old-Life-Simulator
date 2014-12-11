using UnityEngine;
using System.Collections;

public class AnimCurveTest : MonoBehaviour 
{
    public AnimationCurve testCurve;

	// Use this for initialization
	void Start () 
    {
        Debug.Log("AnimCurve: " + testCurve);
        for(int i = 0; i < testCurve.length; ++i)
        {
            Debug.Log("inTangent: " + testCurve[i].inTangent);
            Debug.Log("outTangent: " + testCurve[i].outTangent);
            Debug.Log("tangentMode: " + testCurve[i].tangentMode);
            Debug.Log("time: " + testCurve[i].time);
            Debug.Log("value: " + testCurve[i].value);
            Debug.Log("-------------------------------");
        }
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
