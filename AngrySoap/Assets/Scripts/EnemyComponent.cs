using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public enum EnemyState
{
    Inactive,
    Initialize,
    Chasing,
    Dying,
    Stunned,
};

public class EnemyComponent : MonoBehaviour
{
    private static readonly int Attack = Animator.StringToHash("Attack");
    private static readonly int Die = Animator.StringToHash("Die");
    private EnemyState _currentState;
    private GameObject _playerGameObject;
    private NavMeshAgent _navMeshAgent;
    private EnemyHealthComponent _enemyHealthComponent;
    [SerializeField] private Animator animator;

    [SerializeField] private LayerMask groundLayer, playerLayer;
    [SerializeField] private float attackRange = 3;
    private bool _enemyInAttackRange;

    [SerializeField] private List<GameObject> bubbleSockets = new List<GameObject>();
    [SerializeField] private int maxBubblesThreshold = 0;
    private int _overlappedBubblesCount;


    // Start is called before the first frame update
    void Start()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _enemyHealthComponent = GetComponent<EnemyHealthComponent>();
        maxBubblesThreshold = bubbleSockets.Count;
        UpdateState(EnemyState.Inactive);
    }

    // Update is called once per frame
    void Update()
    {
        _enemyInAttackRange = Physics.CheckSphere(transform.position, attackRange, playerLayer);
        if (_currentState == EnemyState.Chasing && !_enemyInAttackRange)
        {
            ChasePlayer();
        }

        if (_currentState == EnemyState.Chasing && _enemyInAttackRange)
        {
            AttackPlayer();
        }
    }

    public void UpdateState(EnemyState newState)
    {
        _currentState = newState;
        switch (_currentState)
        {
            case EnemyState.Inactive:
                DeactivateEnemy();
                break;
            case EnemyState.Initialize:
                InitializeEnemy();
                break;
            case EnemyState.Chasing:
                ChasePlayer();
                break;
            case EnemyState.Dying:
                EnemyDie();
                break;
            case EnemyState.Stunned:
                StunEnemy();
                break;
            default:
                break;
        }
    }

    public EnemyState GetCurrentState()
    {
        return _currentState;
    }

    public void DeactivateEnemy()
    {
        Debug.Log("DeactivateEnemy called!");
        gameObject.SetActive(false);
    }

    private void InitializeEnemy()
    {
        gameObject.SetActive(true);
        _enemyHealthComponent.ResetHealth();

        foreach (GameObject bubbleSocket in bubbleSockets)
        {
            bubbleSocket.SetActive(false);
        }

        _overlappedBubblesCount = 0;
        UpdateState(EnemyState.Chasing);
    }

    private void ChasePlayer()
    {
        _navMeshAgent.isStopped = false;
        _navMeshAgent.SetDestination(_playerGameObject.transform.position);
    }

    private void EnemyDie()
    {
        animator.SetTrigger(Die);
        _navMeshAgent.isStopped = true;
    }

    private void StunEnemy()
    {
        Debug.Log("StunEnemy called!");
    }

    private void AttackPlayer()
    {
        if (!animator.GetCurrentAnimatorStateInfo(0).IsName("EnemyAttack"))
        {
            gameObject.transform.LookAt(_playerGameObject.transform);
            animator.SetTrigger(Attack);
            _navMeshAgent.SetDestination(transform.position);
        }
    }

    public void SetPlayer(GameObject player)
    {
        _playerGameObject = player;
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == _playerGameObject)
        {
            Debug.LogError("Player is attacked");
        }
        else if (_currentState == EnemyState.Chasing && other.gameObject.GetComponent<Bubble>() != null)
        {
            if (_overlappedBubblesCount < maxBubblesThreshold)
            {
                _overlappedBubblesCount++;
                foreach (var bubbleSocket in bubbleSockets)
                {
                    if (!bubbleSocket.activeSelf)
                    {
                        bubbleSocket.SetActive(true);

                        break;
                    }
                }
            }
        }
    }

    public bool SkipEnemy()
    {
        return maxBubblesThreshold <= _overlappedBubblesCount || _currentState != EnemyState.Chasing ;
    }

    public void DetonateBubbles()
    {
        if (_currentState is EnemyState.Chasing or EnemyState.Stunned)
        {
            foreach (var bubbleSocket in bubbleSockets)
            {
                // float bubbleDamage = Random.Range(35, 45);
                float bubbleDamage = 40.0f;
                if (bubbleSocket.activeSelf)
                {
                    _enemyHealthComponent.OnTakeDamage(bubbleDamage);
                    bubbleSocket.SetActive(false);
                }
            }
            if (_enemyHealthComponent.GetCurrentHealth() == 0.0f)
            {
                UpdateState(EnemyState.Dying);
            }
        }
    }
}