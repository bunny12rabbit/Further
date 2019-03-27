using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour {

    public CanvasGroup fadeGroup;
    public float fadeInSpeed = 0.33f;

    public Transform MenuPanel;
    public Transform OptionsPanel;
    public RectTransform menuContainer;

    private Vector3 desiredMenuPosition;

    public GameObject pauseMenu;

    public bool playing;
    public bool paused = false;

    private string currentScene;
    public Scorer scorer;

    public GameObject music;

    public GameObject mSlider;

    private void Start()
    {
        // Grab the Only CanvasGroup in the scene
        fadeGroup = FindObjectOfType<CanvasGroup>();

        // Find musicPlayer
        music = GameObject.Find("Music");

        // Find Music Slider
        if (mSlider == null)
        mSlider = GameObject.Find("Slider");


      //  if (!playing)
        // Start with a white screen
        fadeGroup.alpha = 1;

        //Determinating what scene is now
        currentScene = SceneManager.GetActiveScene().name;

        //Get volume from PlayerPrefs and Updating slider
        if (PlayerPrefs.HasKey("Volume"))
            mSlider.GetComponent<Slider>().value = PlayerPrefs.GetFloat("Volume");
        
    }

    private void Update()
    {
       // if (!playing)

            // Fade-in
            fadeGroup.alpha = 1 - Time.timeSinceLevelLoad;// * fadeInSpeed;

        if (paused)
            fadeGroup.alpha = 0;

        // Menu Navigation
        menuContainer.anchoredPosition3D = Vector3.Lerp(menuContainer.anchoredPosition3D, desiredMenuPosition, 0.1f);

    }

    public void PlayGame ()
    {
        Debug.Log("Play");
        NavigateTo(1);
    }

    public void OptionsMenu ()
    {
        NavigateTo(2);
    }

    public void backButton ()
    {
        PlayerPrefs.SetFloat("Volume", music.GetComponent<AudioSource>().volume);
        NavigateTo(0);
        //Debug.Log(PlayerPrefs.GetFloat("Volume").ToString());
    }

    public void QuitGame  ()
    {
        Application.Quit();
    }


    public void LoadLevel1 ()
    {
        if (playing)
        {
            Time.timeScale = 1;
            LoadLevel("Level1");
        }
        else
            LoadLevel("Level1");

    }

    public void LoadLevel2()
    {
        if (playing)
        {
            Time.timeScale = 1;
            LoadLevel("Level2");
        }
        else
            LoadLevel("Level2");
    }

    public void ResumeButton()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = Boost.boostVal;
        paused = false;
        
    }

    private void NavigateTo (int menuIndex)
    {
        switch (menuIndex)
        {
            // 0 && default case = Main Menu
            default:
            case 0:
                desiredMenuPosition = Vector3.zero;
                break;
            // 1 - Play menu
            case 1:
                desiredMenuPosition = Vector3.right * 1920;
                break;
            // 2 - Options menu
            case 2:
                desiredMenuPosition = Vector3.left * 1920;
                break;
        }
    }

    public void LoadLevel (string level)
    {
        switch (level)
        {
            // level 1 && default case = Level 1
            default:
            case "Level1":
                SceneManager.LoadScene(level);
                break;
            // Level 2
            case "Level2":
                SceneManager.LoadScene(level);
                break;
        }

    }

    public void RestartLvl ()
    {
        SceneManager.LoadScene(currentScene);
    }

    public void ClearHS ()
    {
        PlayerPrefs.DeleteKey("HighScore");
        scorer.hs.text = "0";

    }

    public void VolumeCtrl(float volume)
    {
        music.GetComponent<AudioSource>().volume = volume;
        //PlayerPrefs.SetFloat("Volume", volume);
    }
}
