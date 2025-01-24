using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bubble : MonoBehaviour
{
    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.CompareTag("Enemy"))
        {
            // Destroy(other.gameObject);
            print("Enemy hit");
            Destroy(gameObject);
        }   
    }
}
