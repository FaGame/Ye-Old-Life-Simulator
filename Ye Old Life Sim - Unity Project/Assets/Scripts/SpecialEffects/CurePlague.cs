using UnityEngine;
using System.Collections;

public class CurePlague : SpecialEffect 
{
    public override ResultValue DoSpecialEffect(PlayerData pData)
    {
        ResultValue ret = new ResultValue(ResultCode.Codes.SUCCESS, 0);

        if(ValueConstants.CURE_PLAGUE_COST > pData.m_Shillings)
        {
            ret.m_Code = ResultCode.Codes.NOT_ENOUGH_SCHILLINGS;
            ret.m_Value = ValueConstants.CURE_PLAGUE_COST - pData.m_Shillings;
            return ret;
        }

        pData.m_Shillings -= ValueConstants.CURE_PLAGUE_COST;
        pData.m_IsInfected = false;

        return ret;
    }

    public override void TurnEnded()
    {
        Debug.Log("Cure Plague End Turn");
    }
}
