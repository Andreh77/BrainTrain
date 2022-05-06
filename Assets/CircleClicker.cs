using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class CircleClicker : GameMode
{
    public GameObject startUI;

    public int numberOfBalls = 10;
 
    public TMP_InputField inputField;
    public TMP_Text scoresUI;
    public List<double> scores = new List<double>();
    public GameObject ball;

    Timer timer;

    public int level = 1;
    bool paused = true;

    float height;
    float width;
    public GameObject destroyParticle;
    public GameObject soundEffect;
    public float startPitch = 0.5f;
    private void Start()
    {
        height = Camera.main.orthographicSize * 2;
        width = height * Camera.main.aspect;
        startUI.SetActive(true);
        numberOfBalls = level * 2;
        SpawnBalls();
        timer = new Timer();
        
    }

    public void CheckClick()
    {
        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

        if (hit.collider != null)
        {
            Destroy(hit.collider.gameObject);

            GameObject particle = Instantiate(destroyParticle, hit.transform.position, hit.transform.rotation);

            Destroy(particle, 2f);

            GameObject sound = Instantiate(soundEffect);
            sound.GetComponent<AudioSource>().pitch = startPitch;
            if (startPitch < 1.5) { startPitch += 0.1f; }
            
            Destroy(sound, 2f);

            numberOfBalls--;
        }
    }
    private void LateUpdate()
    {
        if(!paused && Input.GetKeyDown(KeyCode.Mouse0))
        {
            CheckClick();
        }

        if(!timer.running && Input.GetKeyDown(KeyCode.Mouse0))
        {
            startUI.SetActive(false);
            timer.Start();
            paused = false;
            CheckClick();
        }
        
        if(timer.running && numberOfBalls == 0)
        {
            timer.Stop();
            double score = timer.time;
            scores.Add(score);
            startUI.SetActive(true);
            paused = true;

            NextLevel();
        }
    }

    public void NextLevel()
    {
        scoresUI.text += "Level " + (level) + " Score: " + scores[scores.Count - 1].ToString("F4") + " seconds \n";
        level++;
        numberOfBalls = level * 2;
        timer.Reset();
        SpawnBalls();
    }

    public void SpawnBalls()
    {
        startPitch = 0.2f;
        for (int i = 0; i < numberOfBalls; i++)
        {
            GameObject GO = Instantiate(ball);
            GO.transform.position = new Vector3(Random.Range(-(width/ 2) + 1, (width/ 2) - 1), Random.Range(-(height/ 2) + 1, (height/ 2) - 1), 0);
        }
    }
}