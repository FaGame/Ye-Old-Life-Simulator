using UnityEngine;
using System.Collections;

public class StateMachine : MonoBehaviour {

    public State mStartState;

    private State currState_;

    // Use this for initialization
    void Start()
    {
        if (mStartState == null)
        {
            Debug.LogError("Need a start state");
        }
        currState_ = mStartState;
        currState_.OnStateEntered();
    }

    // Update is called once per frame
    void Update()
    {
        currState_.StateUpdate();   //this could be in fixed update
    }

    void OnGui()
    {
        currState_.StateGUI();
    }

    public void ChangeState(State newState)
    {
        currState_.OnStateExit();
        currState_ = newState;
        currState_.OnStateEntered();
    }
}
