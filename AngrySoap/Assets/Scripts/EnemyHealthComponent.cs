using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class EnemyHealthComponent : MonoBehaviour
{
    [SerializeField] private float maxHealth = 120.0f;
    private float _currentHealth;

    [SerializeField] private ProgressBar healthBar;

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void OnTakeDamage(float damage)
    {
        _currentHealth -= damage;
        healthBar.value = _currentHealth / maxHealth;
    }

    public void ResetHealth()
    {
        _currentHealth = maxHealth;
    }
}