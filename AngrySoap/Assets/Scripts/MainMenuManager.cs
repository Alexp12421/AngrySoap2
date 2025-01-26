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
    [SerializeField] private AudioManager audioManager;
    private bool toggle;

    // Start is called before the first frame update
    void Start()
    {
        toggle = true;
        informationPannel.SetActive(false);
        audioManager = GetComponent<AudioManager>();

        if(!clip.isPlaying)
        {
            clip.Play();
            DontDestroyOnLoad(clip);
        }
    }

    public void StartGame()
    {
        audioManager.PlayClick();
        SceneManager.LoadScene("Final_Map");
    }

    public void QuitGame()
    {
        audioManager.PlayClick();
        Application.Quit();
    }

    public void toggleInstructions()
    {
        audioManager.PlayClick();
        informationPannel.SetActive(toggle);
        toggle = !toggle;
    }
}
