using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public Player currentPlayer;
    private void Start()
    {
        if(instance == null)
        {
            instance = this;
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
        // else
        // {
        //     GameScore lastGameScore = null;

        //     foreach (GameScore gs in currentPlayer.gameScores)
        //     {
        //         if (gs.gameName == gameName)
        //         {
        //             lastGameScore = gs;
        //             break;
        //         }
        //     }

        //     if(lastGameScore.score < score) return; // IF PLAYERS LAST SCORE IS BETTER WE DONT WANT TO CHANGE IT

        //     lastGameScore.score = score; // UPDATE SCORE
        // }

    }

    public void RetrieveHighScore()
    {
        // if new score is a record, store it in a file
    }

    //File structure ||SCORE_ID||USER_FIRSTNAME||USER_LASTNAME||GAME_NAME||LEVEL||SCORE||
}
