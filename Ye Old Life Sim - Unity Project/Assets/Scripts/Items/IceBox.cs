using UnityEngine;
using System.Collections;

public class IceBox : MonoBehaviour 
{
    Food[] m_PlayerFood;

    void Start()
    {
        KeepFoodFresh();
    }

    void KeepFoodFresh()
    {
        //set the food to be not perishable
        for(int i = 0; i < m_PlayerFood.Length; ++i)
        {
            m_PlayerFood[i].m_IsPerishable = false;
        }
    }
}
