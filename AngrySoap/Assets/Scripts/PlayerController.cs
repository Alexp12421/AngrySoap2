using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update

    public float speed;
    private Vector2 moveInput, mouselook;

    private Vector3 rotationTarget;

    [SerializeField]
    private GameObject PlayerVisual;

    [SerializeField]
    private Transform VisualTransform;

    [SerializeField]
    private Vector3 move;

    public void OnLook(InputAction.CallbackContext context)
    {
        mouselook = context.ReadValue<Vector2>();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }

    void Start()
    {
        VisualTransform = PlayerVisual.transform;
    }

    // Update is called once per frame
    void Update()
    {
        MoveWithLook();
    }

    public void MoveWithLook(){
        Ray ray = Camera.main.ScreenPointToRay(mouselook);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            rotationTarget = new(hit.point.x, 0, hit.point.z);
        }
        var lookPos = rotationTarget - transform.position;
        lookPos.y = 0;
        var rotation = Quaternion.LookRotation(lookPos);

        if (rotation != Quaternion.identity)
        {
            VisualTransform.rotation = Quaternion.Slerp(VisualTransform.rotation, rotation, 0.15F);
        }
        
        move = new(moveInput.x, 0, moveInput.y);
        
        if (move.magnitude > 1)
        {
            move.Normalize();
        }

        transform.Translate(speed * Time.deltaTime * move, Space.World);
    }

    public Vector3 GetMove()
    {
        return move;
    }
}
