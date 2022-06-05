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
    [SerializeField] private TextMeshProUGUI flashAvg, flashPb, flashScores, rtAvg, rtPb, rtScores, circleAvg, circlePb, circleScores, speedAvg, speedPb, speedScores;
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

    private float GetAvg(List<float> list, int n = 10)
    {
        float num;
        num = (list.Count > 0) ? list[0] : 0;

        for (int i = Mathf.Max(0, list.Count - n); i < list.Count; i++)
        {
            num += list[i];
        }

        num = (float) System.Math.Round(num / list.Count, 2);
        return num;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            GameObject mouseClickInst = Instantiate(mouseClick, Camera.main.ScreenToWorldPoint(Input.mousePosition) , Quaternion.identity);
            Destroy(mouseClickInst, 0.5f);
        }

        SortUI();
    }

    private void SortUI()
    {
        flashAvg.text = "AVG: " + GetAvg(StatsData.flashCardsScore).ToString() + "s";
        flashPb.text = "PB: " + StatsData.Min(StatsData.flashCardsScore).ToString() + "s";
        flashScores.text = StatsData.GetLastItems(StatsData.flashCardsScore, 10, "s");

        rtAvg.text = "AVG: " + GetAvg(StatsData.reactionTimeScore).ToString() + " ms";
        rtPb.text = "PB: " + StatsData.Min(StatsData.reactionTimeScore).ToString() + " ms";
        rtScores.text = StatsData.GetLastItems(StatsData.reactionTimeScore, 10, " ms");

        circleAvg.text = "AVG: " + GetAvg(StatsData.circleClickerTimeScore).ToString() + "s";
        circlePb.text = "PB: " + StatsData.Min(StatsData.circleClickerTimeScore).ToString() + "s";
        circleScores.text = StatsData.GetLastItems(StatsData.circleClickerTimeScore, 10, "s");

        speedAvg.text = "AVG: " + GetAvg(StatsData.speedClickerTimeScore).ToString() + "s";
        speedPb.text = "PB: " + StatsData.Min(StatsData.speedClickerTimeScore).ToString() + "s";
        speedScores.text = StatsData.GetLastItems(StatsData.speedClickerTimeScore, 10, "s");

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