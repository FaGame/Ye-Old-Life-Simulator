﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class Item : MonoBehaviour 
{
    public abstract void UseItem(PlayerData playerData);
    public string GetDescription()
    {
        return m_Description;
    }

    public int m_Cost = 0;

    public int m_UseCount = 1; //this is how many times an item can be used

    public string m_Description = "Test string for now";
}
