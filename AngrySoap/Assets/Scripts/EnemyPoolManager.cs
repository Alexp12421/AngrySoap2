using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;

public class NewBehaviourScript : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private int numberOfEnemies = 10;
    [SerializeField] private int numberOfEnemiesToHaveSpawned = 6;
    [SerializeField] private GameObject enemyType;
    [SerializeField] private List<EnemyComponent> enemyComponents = new List<EnemyComponent>();

    [SerializeField] private float maxSpawnRangeDistance = 10.0f;

    [SerializeField] private float MinSpawnDelay = 2.5f;
    [SerializeField] private float MaxSpawnDelay = 4.0f;
    [SerializeField] private float _curentSpawnDelay = 5.0f;
    private float _remainingSpawnDelay;
    private GameObject _playerObject;

    void Start()
    {
        for (int i = 0; i < numberOfEnemies; i++)
        {
            enemyComponents.Add(Instantiate(enemyType, new Vector3(0, 0, 0), Quaternion.identity)
                .GetComponent<EnemyComponent>());
        }

        _remainingSpawnDelay = _curentSpawnDelay;
        
        _playerObject = GameObject.Find("Player");

        foreach (var enemyComponent in enemyComponents)
        {
            enemyComponent.SetPlayer(_playerObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
    }

    void FixedUpdate()
    {
        _remainingSpawnDelay -= Time.fixedDeltaTime;
        if (_remainingSpawnDelay <= 0)
        {
            _remainingSpawnDelay = Random.Range(MinSpawnDelay, MaxSpawnDelay);
            int enemiesSpawned = 0;
            foreach (var enemyComponent in enemyComponents)
            {
                if (enemyComponent.GetCurrentState() != EnemyState.Inactive)
                {
                    enemiesSpawned++;
                }
            }

            if (enemiesSpawned < numberOfEnemiesToHaveSpawned)
            {
                foreach (var enemyComponent in enemyComponents)
                {
                    if (enemyComponent.GetCurrentState() == EnemyState.Inactive)
                    {
                        SpawnAtRandomLocation(enemyComponent);
                        break;
                    }
                }
            }
        }
    }

    private void SpawnAtRandomLocation(EnemyComponent enemyComponent)
    {
        float randomX = Random.Range(transform.position.x - maxSpawnRangeDistance,
            transform.position.x + maxSpawnRangeDistance);
        float randomZ = Random.Range(transform.position.z - maxSpawnRangeDistance,
            transform.position.z + maxSpawnRangeDistance);
        enemyComponent.gameObject.transform.SetPositionAndRotation(new Vector3(randomX, 0, randomZ),
            Quaternion.identity);
        enemyComponent.UpdateState(EnemyState.Initialize);
    }
}