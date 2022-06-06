using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Player
{
    public string name;
    public int overallScore;
    [SerializeField] public List<GameScore> gameScores = new List<GameScore>();

    public Player(string _name)
    {
        name = _name;
    }

    public void addGameScore(GameScore _gameScore)
    {
        gameScores.Add(_gameScore);
    }
}

[System.Serializable]
public class GameScore
{
    public string gameName;
    public double score;
    public string catagory;
    public GameScore(string _catagory, string _gameName, double _score)
    {
        catagory = _catagory;
        gameName = _gameName;
        score = _score;
    }
}