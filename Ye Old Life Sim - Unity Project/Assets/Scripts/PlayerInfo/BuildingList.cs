using UnityEngine;
using System.Collections;

public class BuildingList : MonoBehaviour 
{
    public GameObject[] m_Buildings;

    public int GetBuildingNumber(string buildingName)
    {
        for(int i = 0; i < m_Buildings.Length; ++i)
        {
            if(buildingName == m_Buildings[i].name)
            {
                return i;
            }
        }

        return -1;
    }

    public GameObject GetBuildingGameObject(int buildingNum)
    {
        return m_Buildings[buildingNum];
    }
}
