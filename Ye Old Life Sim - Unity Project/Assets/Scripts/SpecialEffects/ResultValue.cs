using UnityEngine;
using System.Collections;

public class ResultValue
{
    public ResultCode.Codes m_Code;
    public int m_Value;

    public ResultValue(ResultCode.Codes c, int v)
    {
        m_Code = c;
        m_Value = v;
    }
}
