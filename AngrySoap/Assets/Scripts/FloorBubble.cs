using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorBubble : MonoBehaviour
{
    [SerializeField]
    private float bubbleDuration = 5.0f;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(DestroyBubble());
    }

    public IEnumerator DestroyBubble(){
        yield return new WaitForSeconds(bubbleDuration);
        Destroy(gameObject);
    }
}
