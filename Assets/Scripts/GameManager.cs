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

    public void TryStoreHighScore(string catagory, string gameName, double score)
    {
        if (firstTimePlayer)
        {
            allPlayerScores.Add(currentPlayer);
            //Debug.Log("ADDING PLAYER TO LIST");
            firstTimePlayer = false;
        }

        GameScore gScore = new GameScore(catagory, gameName, score);
        bool found = false;
        if (currentPlayer.name != "")
        {
            foreach (Player p in allPlayerScores)
            {
                if (p.name == currentPlayer.name)
                {
                    foreach (GameScore gs in p.gameScores)
                    {
                        if (gs.gameName == gameName)
                        {
                            found = true;
                            if (gs.score > score)
                            {
                                //Debug.Log("ADDING SCORE");
                                gs.score = score;
                            }
                        }
                    }

                    if (found == false)
                    {
                        currentPlayer.addGameScore(gScore);
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

    public string getHighScore()
    {
        string txt = "";
        foreach (GameScore gs in currentPlayer.gameScores)
        {
            txt += "Game: " + gs.gameName + " score:" + gs.score.ToString("#0.00") + "\n";
        }
        return txt;
    }

    //public string getHightLevel(string name) {
    //    foreach (GameScore gs in currentPlayer.gameScores)
    //    {
    //        if (gs.gameName == name) return gs.level + "";
    //    }
    //    return "0";
    //}

    public void OnApplicationQuit()
    {
        FileManager.SaveScores(allPlayerScores);
    }
}