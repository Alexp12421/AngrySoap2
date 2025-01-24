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
    }
}