using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class MainMenuManager : MonoBehaviour
{
    public Color normal, header, background;
    public ButtonManager selectedButton;

    public TMP_InputField nameEntry;
    private AudioManager audioManager;
    public GameManager gameManager;
    public GameObject mouseClick, ball;

    public int numberOfBalls = 1000;

    public Animation anim;

    [SerializeField] private Image pause;
    private TextMeshProUGUI pauseText;


    private void Start()
    {     
        gameManager = GameManager.instance;
        // for (int i = 0; i < numberOfBalls; i++)
        // {
        //     Instantiate(ball);
        // }

        audioManager = FindObjectOfType<AudioManager>();
        pauseText = pause.GetComponentInChildren<TextMeshProUGUI>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            GameObject mouseClickInst = Instantiate(mouseClick, Camera.main.ScreenToWorldPoint(Input.mousePosition) , Quaternion.identity);
            Destroy(mouseClickInst, 0.5f);
        }

        pauseText.text = (PlayerPrefs.GetInt("musicPaused", 0) == 0) ? "Pause Music" : "Play Music";
    }
    public void SetupPlayer()
    {
        string name = nameEntry.text;
        gameManager.CheckIfNewPlayer(name);
    }

    public void MusicToggle()
    {
        if (PlayerPrefs.GetInt("musicPaused", 0) == 0)
        {
            audioManager.Play("Stop");
            PauseBGM();
            PlayerPrefs.SetInt("musicPaused", 1);
        }
        else
        {
            audioManager.Play("Start");
            PlayBGM();
            PlayerPrefs.SetInt("musicPaused", 0);
        }
    }

    public void PauseBGM()
    {
        GameObject bgmObj = GameObject.FindGameObjectWithTag("Music");
        if (bgmObj != null) bgmObj.GetComponent<AudioSource>().Pause();
    }

    public void PlayBGM()
    {
        GameObject bgmObj = GameObject.FindGameObjectWithTag("Music");
        if (bgmObj != null) bgmObj.GetComponent<AudioSource>().Play();
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void LoadScene(string name)
    {
        if (name == "FlashCards")
        {
            PlayerPrefs.SetInt("FCAttempt", 1);
            PlayerPrefs.SetString("scoreText", "");
        }
        else if (name == "ReactionTime")
        {
            PlayerPrefs.SetInt("RTAttempt", 1);
            PlayerPrefs.SetString("reflexText", "");
        } 
        SceneManager.LoadScene(name);
    }
}