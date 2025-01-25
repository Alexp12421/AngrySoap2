using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySlime_03 : MonoBehaviour
{
    // Start is called before the first frame update
    
    [SerializeField] private BoxCollider attackBoxCollider;
    
    [SerializeField] EnemyComponent _enemyComponent;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void EnableAttack()
    {
        attackBoxCollider.enabled = true;
    }

    public void DisableAttack()
    {
        attackBoxCollider.enabled = false;
    }
    
    
    public void DeactivateEnemy()
    {
        _enemyComponent.UpdateState(EnemyState.Inactive);
    }
}
