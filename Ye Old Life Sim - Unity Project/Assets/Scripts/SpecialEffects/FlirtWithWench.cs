using UnityEngine;
using System.Collections;

public class FlirtWithWench : SpecialEffect 
{

    private bool hasFlirted_ = false;

    public override ResultValue DoSpecialEffect(PlayerData pData)
    {
        ResultValue ret = new ResultValue(ResultCode.Codes.SUCCESS, 0);
        int wenchSuccess = ValueConstants.WENCH_FLIRT_BASE_SUCCESS;

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
            if(Random.Range(0, ValueConstants.WENCH_FLIRT_ONE_HUNDRED_PERCENT) < wenchSuccess)
            {
                ret.m_Value = (int)ValueConstants.WENCH_FLIRT_WENCH_LEVEL.Evaluate((float)pData.m_Reputation / (float)ValueConstants.WENCH_FLIRT_MAX_REPUTATION);
            }
            else
            {
                ret.m_Code = ResultCode.Codes.FAILED_REPUTATION_CHECK;
                ret.m_Value = wenchSuccess;
            }
        }

        return ret;
    }

    public override void TurnEnded()
    {
        hasFlirted_ = true;
    }
}
