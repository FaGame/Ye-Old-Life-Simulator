using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class Item : MonoBehaviour 
{
    public abstract void UseItem(PlayerData playerData);

    public int m_Cost = 0;
}
