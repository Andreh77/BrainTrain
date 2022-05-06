using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class HowManyBalls : GameMode
{
    public GameObject startUI;

    public double startTime;
    public double finishTime;
    public double totalTime;

    public bool timerStart = false;
    public int numberOfBalls = 10;
 
    public TMP_InputField inputField;
    public TMP_Text scoresUI;
    public List<double> scores = new List<double>();
    public GameObject ball;

    public int level = 1;
    private void Start()
    {
        startUI.SetActive(true);
        numberOfBalls = level * 2;
        SpawnBalls();
    }

    public void SpawnBalls()
    {
        for (int i = 0; i < numberOfBalls; i++)
        {
            GameObject GO = Instantiate(ball);
            GO.transform.position = new Vector3(Random.Range(-3f, 3f), Random.Range(-3f, 3f), 0);
        }
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Mouse0))
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

            if (hit.collider != null)
            {
                Debug.Log("Target Position: " + hit.collider.gameObject.transform.position);
                Destroy(hit.collider.gameObject);
                numberOfBalls--;
            }
            else
            {
                //Maybe add a penalty if they miss click? // LOSE ALL LEVEL PROGRESS
            }
        }

        if(timerStart == false && Input.GetKeyDown(KeyCode.Space))
        {
            startUI.SetActive(false);
            startTime = Time.timeSinceLevelLoadAsDouble;
            timerStart = true;
        }
        
        if(numberOfBalls == 0 && timerStart == true)
        {
            finishTime = Time.timeSinceLevelLoadAsDouble;
            totalTime = finishTime - startTime;
            timerStart = false;
            scores.Add(totalTime);
            startUI.SetActive(true);
 
            //inputField.ActivateInputField();
 
            NextLevel();
        }
    }

    public void NextLevel()
    {
        scoresUI.text += "Level " + (level) + " Score: " + scores[scores.Count - 1].ToString("F4") + "seconds \n";
        level++;
        numberOfBalls = level * 2;
        finishTime = 0;
        startTime = 0;
        SpawnBalls();
    }
}
