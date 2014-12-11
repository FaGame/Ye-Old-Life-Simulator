using UnityEngine;
using System.Collections;

public abstract class SpecialEffect : MonoBehaviour 
{

    public abstract void DoSpecialEffect(PlayerData pData);

    public abstract void TurnEnded();
}
