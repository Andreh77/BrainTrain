using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public Player currentPlayer;

    public List<Player> allPlayerScores = new List<Player>();
    public bool firstTimePlayer = false;
    private void Start()
    {
        RetrieveScores();
    }
    private void RetrieveScores()
    {
        if(instance == null)
        {
            instance = this;

            allPlayerScores = FileManager.ReadScores();
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void CheckIfNewPlayer(string name)
    {
        // FILE LOOKUP TO SEE IF PLAYER EXISTS
        currentPlayer = new Player(name);
        firstTimePlayer = CheckIfPlayerHasScores();
    }

    public void loadScene(string sceneName)
    {
        //if(Time.timeScale != 1) { Time.timeScale = 1; }
        SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
    }

    private void Update() {
        Debug.Log(StatsData.flashCardsScore.Count);
    }

    public void TryStoreHighScore(string gameName, double score)
    {
        if(currentPlayer.gameScores.Count == 0)
        {
            GameScore gScore = new GameScore(gameName, score);
            currentPlayer.addGameScore(gScore);
        }
         else
         {
            foreach (Player p in allPlayerScores)
            {
                if (p.name == currentPlayer.name)
                {
                    foreach (GameScore gs in p.gameScores)
                    {
                        if (gs.gameName == gameName)
                        {
                            if (gs.score > score)
                            {
                                gs.score = score;
                            }
                        }
                    }
                }
            }
        }
    }

    public bool CheckIfPlayerHasScores()
    {
        foreach(Player p in allPlayerScores)
        {
            if(p.name == currentPlayer.name)
            {
                foreach(GameScore gs in p.gameScores)
                {
                    currentPlayer.addGameScore(gs);
                }
                Debug.Log("Retriving old scores");
                return false;
            }
        }

        return true;
    }

    public void OnApplicationQuit()
    {
        if(currentPlayer.name != "" && currentPlayer.gameScores.Count > 0)
        {
            if(firstTimePlayer)
            {
                allPlayerScores.Add(currentPlayer);
                Debug.Log("ADDING PLAYER TO LIST");
            }

        }

        FileManager.SaveScores(allPlayerScores);
    }
}