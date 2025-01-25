using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
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
        math.clamp(_currentHealth, 0, maxHealth);
        _healthBarSlider.value = _currentHealth / maxHealth;
        if (_currentHealth <= 0)
        {
            _healthBarSlider.transform.gameObject.SetActive(false);
        }
    }

    public void ResetHealth()
    {
        _currentHealth = maxHealth;
        if (_healthBarSlider == null)
        {
            _healthBarSlider = healthBar.GetComponent<Slider>();
        }
        _healthBarSlider.transform.gameObject.SetActive(true);
        _healthBarSlider.value = 1.0f;
    }

    public float GetCurrentHealth()
    {
        return _currentHealth;
    }

    public float GetHealthPercentage()
    {
        return _healthBarSlider.value;
    }
}