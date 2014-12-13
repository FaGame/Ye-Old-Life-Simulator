using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class Item : MonoBehaviour 
{
    public abstract void UseItem(PlayerData playerData);

    public int m_Cost = 0;

    public int m_UseCount = 1; //this is how many times an item can be used
}
