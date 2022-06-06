using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class StatisticManager : MonoBehaviour
{
    public GameManager gameManager;
    public GameObject scoreHolder;
    public GameObject coordination, memory, reflex;
    int scoreCount = 0;
    private void start()
    {
        //
    }

    private void Update()
    {
        if(gameManager == null)
        {
            if(GameManager.instance != null)
            {
                gameManager = GameManager.instance;
            }
        }

        if(gameManager.allPlayerScores != null)
        {
            if (gameManager.allPlayerScores.Count > scoreCount)
            {
                scoreCount = gameManager.allPlayerScores.Count;
                Debug.Log("CREATING HIGHSCORE");
                foreach (Player p in gameManager.allPlayerScores)
                {
                    foreach (GameScore gs in p.gameScores)
                    {
                        switch (gs.catagory)
                        {
                            case "coordination":
                                {
                                    CreateScoreHolder(p.name, gs.gameName, (float)gs.score, coordination);
                                    break;
                                }
                            case "memory":
                                {
                                    CreateScoreHolder(p.name, gs.gameName, (float)gs.score, memory);
                                    break;
                                }
                            case "reflex":
                                {
                                    CreateScoreHolder(p.name, gs.gameName, (float)gs.score, reflex);
                                    break;
                                }
                        }
                    }
                }
                
            }
        }
    }

    void CreateScoreHolder(string name, string game, float score, GameObject parent)
    {
        GameObject ch = Instantiate(scoreHolder, parent.transform);
        ch.GetComponentInChildren<TMP_Text>().text = "NAME: " + name + " || GAME: " + game + "|| SCORE: " + score + "s";
    }
}
