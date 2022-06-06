using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameBtn : GameMode
{
    public string gamename;
    public string showname;
    public GameObject parent;

    // Start is called before the first frame update
    void Start()
    {
        //GetComponent<TMPro.TextMeshProUGUI>().text = showname + " lv:" + parent.GetComponent<GameManager>().getHightLevel(gamename);
    }

    // Update is called once per frame
    void Update()
    {
        //GetComponent<TMPro.TextMeshProUGUI>().text = showname + " lv:" + parent.GetComponent<GameManager>().getHightLevel(gamename);
    }
}
