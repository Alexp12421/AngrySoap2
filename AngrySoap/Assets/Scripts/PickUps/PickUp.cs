using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    public PickUpEffect pickUpEffect;

    public void Update(){
        transform.Rotate(Vector3.up, 45 * Time.deltaTime);
        transform.position = new Vector3(transform.position.x, Mathf.PingPong(Time.time * 0.5f, 0.5f) + 0.5f, transform.position.z);
    }

    private void OnTriggerEnter(Collider other) {
        print("Triggered");
        if(other.CompareTag("Player"))
        {
            pickUpEffect.ApplyPickUp(other.gameObject);
            gameObject.SetActive(false);
        }
    }
}
