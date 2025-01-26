using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

public class EnemyPoolManager : MonoBehaviour
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
    [SerializeField] private GameObject gameWonObject;
    [SerializeField] private float gameWonDelay = 3.0f;
    [SerializeField] private UIAbilities uiAbilities;
    [SerializeField] private PlayerInput playerInput;
    private float _remainingSpawnDelay;
    private GameObject _playerObject;
    private bool _gameWon = false;

    [SerializeField] private List<TrashComponent> trashComponents = new List<TrashComponent>();

    void Start()
    {
        for (int i = 0; i < numberOfEnemies; i++)
        {
            enemyComponents.Add(Instantiate(enemyType, new Vector3(0, 0, 0), Quaternion.identity)
                .GetComponent<EnemyComponent>());
        }

        List<GameObject> trashObjects = new List<GameObject>(GameObject.FindGameObjectsWithTag("Trash"));

        foreach (GameObject trashObject in trashObjects)
        {
            TrashComponent trashComponent = trashObject.GetComponent<TrashComponent>();
            if (trashComponent != null)
            {
                trashComponents.Add(trashComponent);
                trashComponent.LinkEnemyPoolManager(this);
            }
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
        if (!_gameWon)
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
        else
        {
            if (gameWonDelay > 0.0f)
            {
                gameWonDelay -= Time.fixedDeltaTime;
            }
            else
            {
                if (gameWonObject.activeSelf == false)
                {
                    gameWonObject.SetActive(true);
                    playerInput.enabled = false;
                    uiAbilities.enabled = false;
                    Time.timeScale = 0f;
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

    public void DetonateBubbles()
    {
        foreach (var enemyComponent in enemyComponents)
        {
            enemyComponent.DetonateBubbles();
        }
    }

    public void TrashDestroyed(TrashComponent trashComponent)
    {
        trashComponent.gameObject.SetActive(false);
        int remainingTrash = 0;
        foreach (var trashComp in trashComponents)
        {
            if (trashComp.gameObject.activeSelf)
            {
                remainingTrash++;
            }
        }

        if (remainingTrash <= 0)
        {
            Debug.LogWarning("Game Won");
            _gameWon = true;
            foreach (var enemyComponent in enemyComponents)
            {
                if (enemyComponent.GetCurrentState() != EnemyState.Inactive)
                {
                    enemyComponent.UpdateState(EnemyState.Dying);
                }
            }
        }
    }

    public void IncreaseEnemyCount(int increaseAmount)
    {
        numberOfEnemiesToHaveSpawned += increaseAmount;
        while (increaseAmount > 0)
        {
            for (int i = enemyComponents.Count - 1; i >= 0; i--)
            {
                if (enemyComponents[i].GetCurrentState() == EnemyState.Inactive)
                {
                    enemyComponents[i].UpdateState(EnemyState.Initialize);
                    increaseAmount--;
                    if (increaseAmount == 0)
                    {
                        break;
                    }
                }
            }
        }
    }
}