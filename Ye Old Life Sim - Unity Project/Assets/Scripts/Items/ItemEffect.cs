using UnityEngine;
using System.Collections;

[System.Serializable]
public class ItemEffect 
{ 
    public enum EffectType
    {
        SPEED,
        INCOME_MODIFIER
    };

    public EffectType m_Type;
    public float m_Timer;
    public float m_Value;

    public ItemEffect(EffectType type, float timer, float value)
    {
        m_Type = type;
        m_Timer = timer;
        m_Value = value;
    }

}
