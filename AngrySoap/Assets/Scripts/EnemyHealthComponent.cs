using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthComponent : MonoBehaviour
{
    [SerializeField] private GameObject healthBar; 
    private Slider _healthBarSlider;
    [SerializeField] private float maxHealth = 120.0f;
    private float _currentHealth;


    void Start()
    {
        _healthBarSlider = healthBar.GetComponent<Slider>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void OnTakeDamage(float damage)
    {
        _currentHealth -= damage;
        _healthBarSlider.value = _currentHealth / maxHealth;
    }

    public void ResetHealth()
    {
        _currentHealth = maxHealth;
        if (_healthBarSlider == null)
        {
            _healthBarSlider = healthBar.GetComponent<Slider>();
        }
        _healthBarSlider.value = 1.0f;
    }
    
    private void SetupCanvas(Canvas canvas, Camera camera)
    {
       
    }
}