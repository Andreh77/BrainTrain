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
    public AudioSource overButtonSound;
    public AudioSource clickButtonSound;

    public TMP_InputField nameEntry;
    public GameManager gameManager;
    public GameObject mouseClick, ball;

    public int numberOfBalls = 1000;

    public Animation anim;


    private void Start()
    {     
        gameManager = GameManager.instance;
        // for (int i = 0; i < numberOfBalls; i++)
        // {
        //     Instantiate(ball);
        // }
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            GameObject mouseClickInst = Instantiate(mouseClick, Camera.main.ScreenToWorldPoint(Input.mousePosition) , Quaternion.identity);
            Destroy(mouseClickInst, 0.5f);
        }
    }
    public void SetupPlayer()
    {
        string name = nameEntry.text;
        gameManager.CheckIfNewPlayer(name);
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void LoadScene(string name)
    {
        SceneManager.LoadScene(name);
    }
}