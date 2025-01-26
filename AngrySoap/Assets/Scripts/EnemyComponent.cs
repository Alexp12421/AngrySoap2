using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Mathematics;
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
    [SerializeField] private EnemyState _currentState;
    private GameObject _playerGameObject;
    private NavMeshAgent _navMeshAgent;
    private EnemyHealthComponent _enemyHealthComponent;
    [SerializeField] private Animator animator;

    [SerializeField] private LayerMask groundLayer, playerLayer;
    [SerializeField] private float attackRange = 3;
    private bool _enemyInAttackRange;

    [SerializeField] private List<GameObject> bubbleSockets = new List<GameObject>();
    [SerializeField] private GameObject stunSocket;
    [SerializeField] private int maxBubblesThreshold = 0;
    private int _overlappedBubblesCount;

    [SerializeField] private float stunDuration = 3.5f;
    private float _remainingStunDuration = 0.0f;

    private Rigidbody _rigidbody;
    [SerializeField] private GameObject enemyMesh;

    private Renderer _renderer;
    [SerializeField] private float maxMovementSpeed = 5.0f;

    [SerializeField] private GameObject overlappedBubblesObj;
    private TextMeshProUGUI _overlappedBubblesText;


    // Start is called before the first frame update
    void Start()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _enemyHealthComponent = GetComponent<EnemyHealthComponent>();
        maxBubblesThreshold = bubbleSockets.Count;
        _rigidbody = GetComponent<Rigidbody>();
        _renderer = enemyMesh.GetComponent<Renderer>();
        _overlappedBubblesText = overlappedBubblesObj.GetComponent<TextMeshProUGUI>();
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

    private void FixedUpdate()
    {
        if (_currentState == EnemyState.Stunned && _remainingStunDuration > 0.0f)
        {
            _remainingStunDuration = math.clamp(_remainingStunDuration - Time.fixedDeltaTime, 0, stunDuration);
            if (_remainingStunDuration <= 0.0f)
            {
                foreach (GameObject bubbleSocket in bubbleSockets)
                {
                    bubbleSocket.SetActive(false);
                }

                _overlappedBubblesCount = 0;
                PopulateOverlappedBubbles();
                UpdateState(EnemyState.Chasing);
            }
        }
    }

    public void UpdateState(EnemyState newState)
    {
        animator.speed = 1.0f;
        _currentState = newState;
        _navMeshAgent.speed = maxMovementSpeed;
        stunSocket.SetActive(false);
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

        PopulateOverlappedBubbles();
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
        Material[] materials = _renderer.materials;

        // Iterate through each material and set its color
        foreach (Material mat in materials)
        {
            Debug.LogWarning(mat.name);
            if (mat.name.Contains("Slime3")) // Check if the material has a "_Color" property
            {
                mat.color = GetRandomColor();
                break;
            }
        }

        gameObject.transform.LookAt(_playerGameObject.transform);
        gameObject.SetActive(true);
        _enemyHealthComponent.ResetHealth();
        foreach (GameObject bubbleSocket in bubbleSockets)
        {
            bubbleSocket.SetActive(false);
        }

        stunSocket.SetActive(false);

        _overlappedBubblesCount = 0;
        PopulateOverlappedBubbles();
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
        _navMeshAgent.isStopped = true;
        _remainingStunDuration = stunDuration;
        stunSocket.SetActive(true);
        animator.speed = 0.0f;
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
        if (_currentState == EnemyState.Chasing && other.gameObject == _playerGameObject)
        {
            Debug.LogError("Player is attacked");
            _playerGameObject.GetComponent<PlayerHealth>()?.TakeDamage(Random.Range(1, 5));
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

                _navMeshAgent.speed = maxMovementSpeed - _overlappedBubblesCount;
            }
            else if (_overlappedBubblesCount == maxBubblesThreshold)
            {
                UpdateState(EnemyState.Stunned);
            }
        }

        PopulateOverlappedBubbles();
    }

    public bool SkipEnemy()
    {
        return maxBubblesThreshold <= _overlappedBubblesCount || _currentState != EnemyState.Chasing;
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
                    _overlappedBubblesCount--;
                    bubbleSocket.SetActive(false);
                }
            }

            if (_enemyHealthComponent.GetCurrentHealth() <= 0.0f)
            {
                UpdateState(EnemyState.Dying);
            }
        }

        PopulateOverlappedBubbles();
    }

    private Color GetRandomColor()
    {
        return new Color(Random.value, Random.value, Random.value); // RGB random values between 0 and 1
    }

    private void PopulateOverlappedBubbles()
    {
        if (_overlappedBubblesText == null)
        {
            return;
        }
        switch (_currentState)
        {
            case EnemyState.Inactive:
            case EnemyState.Initialize:
            case EnemyState.Dying:
                _overlappedBubblesText.text = "";
                break;
            case EnemyState.Chasing:
                switch (_overlappedBubblesCount)
                {
                    case 1:
                        _overlappedBubblesText.text = "1";
                        _overlappedBubblesText.color = new Color(255 / 255f, 100 / 255f, 89 / 255f);
                        break;
                    case 2:
                        _overlappedBubblesText.text = "2";
                        _overlappedBubblesText.color = new Color(247 / 255f, 60 / 255f, 47 / 255f);
                        break;
                    case 3:
                        _overlappedBubblesText.text = "3";
                        _overlappedBubblesText.color = new Color(255 / 255f, 17 / 255f, 0);
                        break;
                    default:
                        _overlappedBubblesText.text = "";
                        break;
                }

                break;
            case EnemyState.Stunned:
                _overlappedBubblesText.text = "STUNNED";
                _overlappedBubblesText.color = new Color(0, 0, 0);
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
}