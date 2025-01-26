using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerDash : MonoBehaviour
{
    [SerializeField]
    private float dashSpeed = 15.0f;

    [SerializeField]
    private float dashDuration = 0.5f;

    [SerializeField]
    private float dashCoolDown = 1.0f;

    [SerializeField]
    private bool canDash = true;

    [SerializeField]
    private int waterCost = 5;

    private Rigidbody rb;

    [SerializeField]
    private GameObject PlayerVisual;

    [SerializeField]
    private Transform VisualTransform;

    [SerializeField]
    private AnimatorController animatorController;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        VisualTransform = PlayerVisual.transform;
        animatorController = PlayerVisual.GetComponent<AnimatorController>();
    }

    public void OnDash(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            if (canDash && gameObject.GetComponent<PlayerWater>().ConsumeWater(waterCost))
            {
                canDash = false;
                StartCoroutine(Dash());
            }

        }
    }

    private IEnumerator Dash()
    {
        Vector3 moveDirection = gameObject.GetComponent<PlayerController>().GetMove();
        if  (moveDirection == Vector3.zero)
        {
            moveDirection = VisualTransform.forward;
        }
        Vector3 DashForce = moveDirection * dashSpeed;
        rb.AddForce(DashForce, ForceMode.Impulse);
        animatorController.startRunning();
        animatorController.startDashing();
        // animator.runtimeAnimatorController.animationClips
        Invoke(nameof(StopDash), dashDuration);
        yield return new WaitForSeconds(dashCoolDown);
        canDash = true;
    }

    private void StopDash()
    {
        rb.velocity = Vector3.zero;
        animatorController.stopDashing();
    }
}
