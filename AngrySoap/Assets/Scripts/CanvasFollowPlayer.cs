using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasFollowPlayer : MonoBehaviour
{
    void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 directionToCamera = Camera.main.transform.position - transform.position;

        directionToCamera = -directionToCamera;

        Quaternion targetRotation = Quaternion.LookRotation(directionToCamera);

        if (transform.rotation != targetRotation)
        {
            transform.rotation = targetRotation;
        }
    }
}
