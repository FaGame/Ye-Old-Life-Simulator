using UnityEngine;
using System.Collections;

public class ChapelDonation : SpecialEffect 
{
    public override ResultValue DoSpecialEffect(PlayerData pData)
    {
        ResultValue ret = new ResultValue(ResultCode.Codes.SUCCESS, 0);

        if (ValueConstants.CHAPEL_DONATION_COST > pData.m_Shillings)
        {
            ret.m_Code = ResultCode.Codes.NOT_ENOUGH_SCHILLINGS;
            ret.m_Value = ValueConstants.CHAPEL_DONATION_COST - pData.m_Shillings;
            return ret;
        }

        pData.m_Shillings -= ValueConstants.CHAPEL_DONATION_COST;
        pData.m_Reputation += ValueConstants.CHAPEL_REPUTATION_GAIN;

        return ret;
    }

    public override void TurnEnded()
    {
        Debug.Log("Chapel Donation End Turn");
    }
}
