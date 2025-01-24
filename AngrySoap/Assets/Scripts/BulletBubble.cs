using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBubble : Bubble
{
    [SerializeField]
    private float bulletDuration = 2.0f;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(DestroyBullet());
    }

    public IEnumerator DestroyBullet(){
        yield return new WaitForSeconds(bulletDuration);
        Destroy(gameObject);
    }
}
