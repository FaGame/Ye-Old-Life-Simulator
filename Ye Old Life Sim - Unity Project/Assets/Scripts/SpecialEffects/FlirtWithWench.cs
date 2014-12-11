using UnityEngine;
using System.Collections;

public class FlirtWithWench : SpecialEffect 
{

    private bool hasFlirted_ = false;

    public override void DoSpecialEffect(PlayerData pData)
    {
        float wenchSuccess = 10.0f;

        if(!hasFlirted_)
        {
            hasFlirted_ = true;
            if(pData.m_Reputation <= 100)
            {
                wenchSuccess -= pData.m_Reputation / 10;
            }
            else
            {
                wenchSuccess += pData.m_Reputation / 10;
            }
        }
    }

    public override void TurnEnded()
    {
        hasFlirted_ = true;
    }
}
