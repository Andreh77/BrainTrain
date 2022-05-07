using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class GameSetup : MonoBehaviour
{
    public GameManager gameManager;
    public string gameName = " ";
    public void Start()
    {
        gameManager = GameManager.instance;
    }

    public void MainMenu()
    {
        gameManager.loadScene("MainMenu");
    }

    public void CheckIfHighScore(double score)
    {
        gameManager.TryStoreHighScore(SceneManager.GetActiveScene().name, score);
    }
}