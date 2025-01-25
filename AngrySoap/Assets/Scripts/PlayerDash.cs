using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerDash : MonoBehaviour
{
    [SerializeField]
    private float dashSpeed = 50.0f;

    [SerializeField]
    private float dashDuration = 0.5f;

    [SerializeField]
    private float dashCoolDown = 1.0f;

    [SerializeField]
    private bool canDash = true;

    [SerializeField]
    private bool isDashing = false;

    private Rigidbody rb;

    [SerializeField]
    private GameObject PlayerVisual;

    [SerializeField]
    private Transform VisualTransform;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        VisualTransform = PlayerVisual.transform;
    }

    public void OnDash(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            if (canDash)
            {
                canDash = false;
                isDashing = true;
                StartCoroutine(Dash());
            }
        }
    }

    private IEnumerator Dash()
    {
        Vector3 DashForce = VisualTransform.forward * dashSpeed;
        rb.AddForce(DashForce, ForceMode.Impulse);
        Invoke(nameof(StopDash), dashDuration);
        yield return new WaitForSeconds(dashCoolDown);
        canDash = true;
    }

    private void StopDash()
    {
        rb.velocity = Vector3.zero;
        isDashing = false;
    }

    public bool GetIsDashing()
    {
        return isDashing;
    }
}
