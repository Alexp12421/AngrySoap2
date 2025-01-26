using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroCOntroller : MonoBehaviour
{
    [SerializeField] private int elapsedTime;

    // Start is called before the first frame update
    void Start()
    {
        elapsedTime = 0;

        StartCoroutine(TimerCoroutine());
    }

    IEnumerator TimerCoroutine()
    {
        while (elapsedTime <= 113)
        {
            yield return new WaitForSeconds(1f); // Wait for 1 second
            elapsedTime++;
        }
        SceneManager.LoadScene("MainMenu");
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.anyKey)
        {
            SceneManager.LoadScene("MainMenu");
        }
    }
}
