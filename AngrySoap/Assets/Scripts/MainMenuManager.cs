using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] private GameObject informationPannel;
    [SerializeField] private AudioSource clip;
    private bool toggle;

    // Start is called before the first frame update
    void Start()
    {
        toggle = true;
        informationPannel.SetActive(false);

        DontDestroyOnLoad(clip);
    }

    public void StartGame()
    {
        //SceneManager.LoadScene("");
        Debug.Log("You pressed Play!");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void toggleInstructions()
    {
        informationPannel.SetActive(toggle);
        toggle = !toggle;
    }
}
