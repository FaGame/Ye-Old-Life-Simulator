using UnityEngine;
using System.Collections;

public abstract class SpecialEffect : MonoBehaviour 
{

    public abstract void DoSpecialEffect(PlayerData pData);

    // This must always be called when the player's turn ends.
    public abstract void TurnEnded();
}
