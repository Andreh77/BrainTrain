using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class HowManyBalls : GameMode
{
    public GameObject InstructionsScreen, guessScreen;

    public double startTime;
    public double finishTime;
    public double totalTime;

    public bool timerStarted = false;
    public int numberOfBalls = 1;

    public TMP_InputField inputField;

    public GameObject ball;
    
    private void Start()
    {
        numberOfBalls = 1;
        for (int i = 0; i < numberOfBalls; i++)
        {
            GameObject GO = Instantiate(ball);
            GO.transform.position = new Vector3(Random.Range(-3f, 3f), Random.Range(-3f, 3f), 0);
        }
    }

    private void Update()
    {
        if(timerStarted == false && Input.GetKeyDown(KeyCode.Space))
        {
            InstructionsScreen.SetActive(false);
            startTime = Time.timeSinceLevelLoadAsDouble;
            timerStarted = true;
        }
        else if(Input.GetKeyDown(KeyCode.Space))
        {
            finishTime = Time.timeSinceLevelLoadAsDouble;
            totalTime = finishTime - startTime;
            
            guessScreen.SetActive(true);
            inputField.ActivateInputField();
        }
    }

    public void CheckAnwser()
    {
        int guess = int.Parse(inputField.text);
        if (guess == numberOfBalls)
        {
            Correct();
        }
        else
        {
            Incorrect();
        }
    }

    public void Correct()
    {
        Debug.Log("It took you: " + totalTime + " to anwser correctly.");
    }

    public void Incorrect()
    {
        Debug.Log("Wrong anwser");
    }
}
