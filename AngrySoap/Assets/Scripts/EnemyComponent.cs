using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyState
{
    Inactive,
    Chasing,
    Dying,
    Stunned,
};

public class EnemyComponent : MonoBehaviour
{
    private EnemyState _currentState;
    // Start is called before the first frame update
    void Start()
    {
        UpdateState(EnemyState.Inactive);
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void UpdateState(EnemyState newState)
    {
        _currentState = newState;
        switch (_currentState)
        {
            case EnemyState.Inactive:
                DeactivateEnemy();
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
    }

    private void ChasePlayer()
    {
        Debug.Log("ChasePlayer called!");
    }

    private void EnemyDie()
    {
        Debug.Log("EnemyDie called!");
    }

    private void StunEnemy()
    {
        Debug.Log("StunEnemy called!");
    }
}
