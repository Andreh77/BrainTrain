using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public void loadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void StoreHighScore()
    {
        // if new score is a record, store it in a file
    }

    public void RetrieveHighScore()
    {
        // if new score is a record, store it in a file
    }

    //File structure ||SCORE_ID||USER_FIRSTNAME||USER_LASTNAME||GAME_NAME||LEVEL||SCORE||
}
