using UnityEngine;
using System.Collections;

public class CursorEffects : MonoBehaviour {

    public ParticleEmitter mPEmitter;

   
    
    
	// Use this for initialization
	void Start () {
        mPEmitter.emit = false;
	}
	
	// Update is called once per frame
	void Update () {

        //have the particle emitter follow the mouse
        Ray cursor = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        mPEmitter.transform.position = cursor.GetPoint(75);
        
       // Debug.Log(Input.mousePosition);
        if(Input.GetMouseButtonDown(0))
        {
            if (Physics.Raycast(cursor, out hit))
            {
                switch(hit.collider.tag)
                { 
                    case "Ground":
                        mPEmitter.emit = true;
                        break;
                }
            }
        }

        if(Input.GetMouseButtonUp(0))
        {
            mPEmitter.emit = false;
        }
        
	}
}
