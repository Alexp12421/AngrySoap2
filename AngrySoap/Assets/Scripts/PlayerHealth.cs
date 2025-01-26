using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField]
    private int health = 100;

    [SerializeField]
    private bool isAlive = true;

    [SerializeField]
    private AnimatorController animatorController;

    [SerializeField] private GameObject lostBox;
    private float lostTimer = 2;

    private void Start()
    {
        animatorController = GetComponentInChildren<AnimatorController>();
    }

    private void Update()
    {
        if (!isAlive)
        {
            if (lostTimer > 0)
            {
                lostTimer -= Time.deltaTime;
            }

            if (lostTimer <= 0 && !lostBox.activeSelf)
            {
                lostBox.SetActive(true);
                Time.timeScale = 0;
            }
        }
    }

    public void TakeDamage(int damage){
        health -= damage;
        if(health <= 0){
            health = 0;
            isAlive = false;
            animatorController.isDead();
            gameObject.GetComponent<UIAbilities>().enabled = false;
            gameObject.GetComponent<PlayerInput>().enabled = false;
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
