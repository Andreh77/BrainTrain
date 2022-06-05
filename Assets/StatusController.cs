using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusController : GameMode
{
    public GameObject txt;
    void Start()
    {
        string str = GetComponent<GameSetup>().gameManager.getHighScore();
        txt.GetComponent<TMPro.TextMeshProUGUI>().text = str == "" ? "None" : str;
    }

    // Update is called once per frame
    void Update()
    {
        string str = GetComponent<GameSetup>().gameManager.getHighScore();
        txt.GetComponent<TMPro.TextMeshProUGUI>().text = str == "" ? "None" : str;
    }
}
