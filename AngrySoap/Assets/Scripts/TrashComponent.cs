using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashComponent : MonoBehaviour
{
    private EnemyHealthComponent _enemyHealthComponent;
    private EnemyPoolManager _enemyPoolManager;
    private readonly float[] _healthThresholds = { 0.75f, 0.5f, 0.25f, 0f };
    private float _lastThresholdReached = 1.0f;
    [SerializeField] private int increaseMonstersByThreshold = 1;
    [SerializeField] private GameObject healthPickup;
    [SerializeField] private GameObject waterPickup;
    [SerializeField] private float xRange = 10.0f;
    [SerializeField] private float zRange = 10.0f;

    // Start is called before the first frame update
    void Start()
    {
        _enemyHealthComponent = GetComponent<EnemyHealthComponent>();
        _enemyHealthComponent.ResetHealth();
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void ApplyDamage(int damage)
    {
        _enemyHealthComponent.OnTakeDamage(damage);
        CheckHealthThresholds();
        if (_enemyHealthComponent.GetCurrentHealth() <= 0.0f && _enemyPoolManager)
        {
            _enemyPoolManager.TrashDestroyed(this);
        }
    }

    public void LinkEnemyPoolManager(EnemyPoolManager poolManager)
    {
        _enemyPoolManager = poolManager;
    }

    private void CheckHealthThresholds()
    {
        float currentPercentage = _enemyHealthComponent.GetHealthPercentage();
        foreach (var threshold in _healthThresholds)
        {
            if (currentPercentage <= threshold && _lastThresholdReached > threshold)
            {
                Debug.LogError("THRESHOLD REACHED");
                SpawnObjectAtRandomPosition(healthPickup);
                SpawnObjectAtRandomPosition(waterPickup);
                _lastThresholdReached = threshold;
                _enemyPoolManager.IncreaseEnemyCount(increaseMonstersByThreshold);
                break;
            }
        }
    }

    private void SpawnObjectAtRandomPosition(GameObject prefab)
    {
        float xPos = Random.Range(-xRange, xRange);
        float zPos = Random.Range(-zRange, zRange);
        Instantiate(prefab, new Vector3(xPos, 0, zPos), Quaternion.identity);
        
    }
}