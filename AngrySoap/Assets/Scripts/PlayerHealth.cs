using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField]
    private int health = 100;

    [SerializeField]
    private bool isAlive = true;

    [SerializeField]
    private AnimatorController animatorController;

    private void Start()
    {
        animatorController = GetComponentInChildren<AnimatorController>();
    }

    public void TakeDamage(int damage){
        health -= damage;
        if(health <= 0){
            health = 0;
            isAlive = false;
            animatorController.isDead();
        }
    }

    public void Heal(int amount){
        health += amount;
        if(health > 100){
            health = 100;
        }
    }

    public int GetHealth(){
        return health;
    }

    public bool IsAlive(){
        return isAlive;
    }
}
