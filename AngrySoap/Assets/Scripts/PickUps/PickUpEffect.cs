using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PickUpEffect : ScriptableObject
{
    public abstract void ApplyPickUp(GameObject player);
}
