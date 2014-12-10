using UnityEngine;
using System.Collections;

public class Building : MonoBehaviour 
{
    public JobData[] m_JobData;


	// Use this for initialization
	void Start () 
    {
	
	}
	
	// Update is called once per frame
	void Update () 
    {
	
	}

    void Work(PlayerData pData, JobData jData)
    {

    }

    /*void BuyItem(PlayerData pData, ItemData iData)
    {

    }

    void Interact(PlayerData pData, InteractionData iData)
    {

    }*/

    public bool ApplyForJob(PlayerData pData, JobData jData)
    {
        bool isJobAllowed = false;

        if(jData.m_ReputationRequirement > pData.m_Reputation)
        {
            return false;
        }

        //for(int i = 0; i < jData.m

        return isJobAllowed;
    }
}
