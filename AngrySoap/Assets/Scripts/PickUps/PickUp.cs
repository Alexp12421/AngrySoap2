using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    public PickUpEffect pickUpEffect;

    private void OnTriggerEnter(Collider other) {
        print("Triggered");
        if(other.CompareTag("Player"))
        {
            pickUpEffect.ApplyPickUp(other.gameObject);
            gameObject.SetActive(false);
        }
    }
}
