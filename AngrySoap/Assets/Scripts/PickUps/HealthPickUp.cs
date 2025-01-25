using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PickUp/HealthPickUp")]
public class HealthPickUp : PickUpEffect
{
    [SerializeField]
    private int healthAmount = 10;

    public override void ApplyPickUp(GameObject player)
    {
        player.GetComponent<PlayerHealth>().Heal(healthAmount);
    }
}
