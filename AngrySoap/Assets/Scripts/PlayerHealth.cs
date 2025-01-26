using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private float maxHealth = 100;
        
    [SerializeField]
    private float health = 100;

    [SerializeField]
    private bool isAlive = true;

    [SerializeField]
    private AnimatorController animatorController;

    [SerializeField] private GameObject lostBox;
    private float lostTimer = 2;
    
    [SerializeField] private GameObject healthBox;
    private Slider _healthSlider;

    private void Start()
    {
        health = maxHealth;
        animatorController = GetComponentInChildren<AnimatorController>();
        _healthSlider = healthBox.GetComponent<Slider>();
        UpdateHealthSlider();
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
        UpdateHealthSlider();
    }

    public void Heal(int amount){
        health += amount;
        if(health > maxHealth){
            health = maxHealth;
        }
        UpdateHealthSlider();
    }

    public float GetHealth(){
        return health;
    }

    public bool IsAlive(){
        return isAlive;
    }

    private void UpdateHealthSlider()
    {
        if (health == 0)
        {
            _healthSlider.gameObject.SetActive(false);
        }
        else
        {
            _healthSlider.value = health/maxHealth;
        }
    }
}
