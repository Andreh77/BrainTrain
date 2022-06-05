using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class GameSetup : MonoBehaviour
{
    public GameManager gameManager;
    private AudioManager audioManager;
    public string gameName = "";
    public void Start()
    {
        audioManager = FindObjectOfType<AudioManager>();
        gameManager = GameManager.instance;
    }

    public void MainMenu()
    {
        audioManager.Play("Pause");
        gameManager.loadScene("MainMenu");
    }

    public void CheckIfHighScore(double score, int level)
    {
        gameManager.TryStoreHighScore(SceneManager.GetActiveScene().name, score, level);
    }
}