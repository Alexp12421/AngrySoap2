using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bubble : MonoBehaviour
{
    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.CompareTag("Enemy"))
        {
            EnemyComponent enemyComponent = other.transform.parent.GetComponent<EnemyComponent>();
            if (enemyComponent && !enemyComponent.SkipEnemy())
            {
                print("Enemy hit");
                Destroy(gameObject);
            }
            // Destroy(other.gameObject);
        }   
    }
}
