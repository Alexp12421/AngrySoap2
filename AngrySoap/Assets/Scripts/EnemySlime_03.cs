using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySlime_03 : MonoBehaviour
{
    // Start is called before the first frame update
    
    [SerializeField] private BoxCollider attackBoxCollider;
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
}
