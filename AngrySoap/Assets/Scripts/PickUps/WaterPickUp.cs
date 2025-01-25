using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PickUp/WaterPickUp")]
public class WaterPickUp : PickUpEffect
{
    [SerializeField]
    private int waterAmount = 10;

    public override void ApplyPickUp(GameObject player)
    {
        player.GetComponent<PlayerWater>().AddWater(waterAmount);
    }
}
