using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Preloader : MonoBehaviour {

    private CanvasGroup fadeGroup;
    public float loadTime;
    public float minimumLogoTime = 2f; // Minimum time of that scene

    private void Start()
    {
        // Grab the Only CanvasGroup in the scene
        fadeGroup = FindObjectOfType<CanvasGroup>();

        // Start with a white screen
        fadeGroup.alpha = 1;

        // Pre load the game
        // $$

        // Get a timestamp of complition time
        // if loadtime superfast, give it a small buffer time, so we can apreciate the logo
        if (Time.time < minimumLogoTime)
            loadTime = minimumLogoTime;
        else
            loadTime = Time.time;
    }

    private void Update()
    {
        // Fade-In
        if (Time.time < minimumLogoTime)
        {
            fadeGroup.alpha = 1 - Time.time;
        }

        // Fade-Out
        if (Time.time > minimumLogoTime && loadTime !=0)
        { 
            fadeGroup.alpha = Time.time - minimumLogoTime;
            if (fadeGroup.alpha >= 1)
            {
                Debug.Log("Change the scene");
                SceneManager.LoadScene("Menu");
            }
        }
    }
}
