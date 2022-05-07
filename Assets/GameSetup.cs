using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameSetup : MonoBehaviour
{
    public GameManager gameManager;

    public void Start()
    {
        gameManager = GameManager.instance;
    }

    public void MainMenu()
    {
        gameManager.loadScene("MainMenu");
    }
}

