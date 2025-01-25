using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;

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
    private EnemyState _currentState;
    private GameObject _playerGameObject;
    private NavMeshAgent _navMeshAgent;
    [SerializeField] private Animator animator;
    
    [SerializeField] private LayerMask groundLayer, playerLayer;
    [SerializeField] private float attackRange = 3;
    private bool _enemyInAttackRange;

    // Start is called before the first frame update
    void Start()
    {
        UpdateState(EnemyState.Inactive);
        _navMeshAgent = GetComponent<NavMeshAgent>();
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

    private void DeactivateEnemy()
    {
        Debug.Log("DeactivateEnemy called!");
        gameObject.SetActive(false);
    }

    private void InitializeEnemy()
    {
        Debug.Log("InitializeEnemy called!");
        gameObject.SetActive(true);
        
        UpdateState(EnemyState.Chasing);
    }

    private void ChasePlayer()
    {
        _navMeshAgent.SetDestination(_playerGameObject.transform.position);
    }

    private void EnemyDie()
    {
        Debug.Log("EnemyDie called!");
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
    }
}