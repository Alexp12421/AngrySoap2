using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerDetonateBubbles : MonoBehaviour
{
    [SerializeField]
    private float detonateCoolDown = 1.0f;

    [SerializeField]
    private bool canDetonate = true;

    [SerializeField]
    private int waterCost = 5;

    [SerializeField]
    private EnemyPoolManager enemyPoolManager;

    [SerializeField]
    private AnimatorController animatorController;

    void Start()
    {
        animatorController = GetComponentInChildren<AnimatorController>();
    }

    public void OnDetonate(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            if (canDetonate && gameObject.GetComponent<PlayerWater>().ConsumeWater(waterCost))
            {
                canDetonate = false;
                StartCoroutine(Detonate());
            }
        }
    }

    private IEnumerator Detonate()
    {
        enemyPoolManager.DetonateBubbles();
        yield return new WaitForSeconds(detonateCoolDown);
        canDetonate = true;
    }
}
