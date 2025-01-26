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

    [SerializeField]
    private PlayerController playerController;

    private AudioManager audioManager;

    void Start()
    {
        animatorController = GetComponentInChildren<AnimatorController>();
        playerController = GetComponent<PlayerController>();
        audioManager = GetComponent<AudioManager>();
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
        animatorController.stopRunning();
        animatorController.startDetonate();
        playerController.usingAbility(true);
        StartCoroutine(DetonateAnimation());
        enemyPoolManager.DetonateBubbles();
        yield return new WaitForSeconds(detonateCoolDown);
        canDetonate = true;
    }

    private IEnumerator DetonateAnimation()
    {   
        audioManager.PlayDetonate();
        yield return new WaitForSeconds(animatorController.animationClipLengths["Anim_Detonate"]/2);
        animatorController.stopDetonate();
        playerController.usingAbility(false);
    }
}
