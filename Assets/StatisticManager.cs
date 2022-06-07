using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;

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
                                    SortScores(gs, p, coordination);
                                    break;
                                }
                            case "memory":
                                {
                                    SortScores(gs, p, memory);
                                    break;
                                }
                            case "reflex":
                                {
                                    SortScores(gs, p, reflex);
                                    break;
                                }
                        }
                    }
                }
                
            }
        }
    }


    bool SortScores(GameScore gs, Player p, GameObject parent)
    {
        if (coordination.transform.childCount > 0)
        {
            if (coordination.transform.GetChild(0).GetComponent<Score>().score > gs.score)
            {
                GameObject scoreHolder = CreateScoreHolder(p.name, gs.gameName, (float)gs.score, parent);
                scoreHolder.transform.SetAsFirstSibling();
                return true;
            }
            else
            {
                foreach (Transform child in coordination.transform)
                {
                    if (child.GetComponent<Score>().score > gs.score)
                    {
                        GameObject scoreHolder = CreateScoreHolder(p.name, gs.gameName, (float)gs.score, parent);
                        scoreHolder.transform.SetSiblingIndex(child.GetSiblingIndex());
                        return true;
                    }
                }

                GameObject scoreHolder2 = CreateScoreHolder(p.name, gs.gameName, (float)gs.score, parent);
                scoreHolder2.transform.SetAsLastSibling();
                return true;
            }
        }
        else
        {
            GameObject scoreHolder2 = CreateScoreHolder(p.name, gs.gameName, (float)gs.score, coordination);
            return true;
        }
    }

    GameObject CreateScoreHolder(string name, string game, float score, GameObject parent)
    {
        GameObject ch = Instantiate(scoreHolder, parent.transform);
        ch.GetComponent<Score>().name = name;
        ch.GetComponent<Score>().gameName = game;
        ch.GetComponent<Score>().score = score;
        ch.GetComponentInChildren<TMP_Text>().text = "[NAME: " + name + "] [GAME: " + game + "] [SCORE: " + score.ToString("F3") + "s]";
        return ch;    
    }
}
