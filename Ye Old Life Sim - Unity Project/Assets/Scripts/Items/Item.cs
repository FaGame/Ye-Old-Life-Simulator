using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class Item : MonoBehaviour 
{
    public abstract void UseItem(PlayerData playerData, PlayerController playerController);
    public abstract void UpdateTimer();
}
