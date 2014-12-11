using UnityEngine;
using System.Collections;

public class FlirtWithWench : SpecialEffect 
{

    private bool hasFlirted_ = false;

    public override void DoSpecialEffect(PlayerData pData)
    {
        int wenchSuccess = ValueConstants.WENCH_FLIRT_BASE_SUCCESS;
        bool isFlirtSuccess = false;

        if(!hasFlirted_)
        {
            hasFlirted_ = true;
            if(pData.m_Reputation <= ValueConstants.WENCH_FLIRT_BASE_REPUTATION_PIVOT)
            {
                wenchSuccess -= pData.m_Reputation / ValueConstants.WENCH_FLIRT_SUCCESS_ADJUST;
            }
            else
            {
                wenchSuccess += pData.m_Reputation / ValueConstants.WENCH_FLIRT_SUCCESS_ADJUST;
            }
            isFlirtSuccess = Random.Range(0, 100) < wenchSuccess;
        }
    }

    public override void TurnEnded()
    {
        hasFlirted_ = true;
    }
}
