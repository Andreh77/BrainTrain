using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class CircleClicker : GameMode
{
    public GameObject startUI;

    public int numberOfBalls = 10;
    int ballsLeft = 10;
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
    GameObject ballClosestToMouse = null;
    public GameObject pauseMenu;
    public List<GameObject> balls = new List<GameObject>();
    private void Start()
    {
        height = Camera.main.orthographicSize * 2;
        width = height * Camera.main.aspect;
        startUI.SetActive(true);
        //numberOfBalls = level * 2;
        ballsLeft = numberOfBalls;
        SpawnBalls();
        timer = new Timer();    
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape) && !pauseMenu.activeSelf)
        {
            Time.timeScale = 0f;
            pauseMenu.SetActive(true);
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && pauseMenu.activeSelf)
        {
            Time.timeScale = 1f;
            pauseMenu.SetActive(false);
        }

        foreach(GameObject ball in balls)
        {
            ball.GetComponent<SpriteRenderer>().color = Color.black;
            if (ballClosestToMouse == null) { ballClosestToMouse = ball;}
            
            if(Vector3.Distance(ballClosestToMouse.transform.position, Camera.main.ScreenToWorldPoint(Input.mousePosition)) > Vector3.Distance(ball.transform.position, Camera.main.ScreenToWorldPoint(Input.mousePosition)))
            {
                ballClosestToMouse = ball;
            }
        }
        //ballClosestToMouse.GetComponent<SpriteRenderer>().color = Color.white;
        Debug.DrawLine(Camera.main.ScreenToWorldPoint(Input.mousePosition), ballClosestToMouse.transform.position, Color.blue);
    }
    public void CheckClick()
    {
        
        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

        if (hit.collider != null)
        {
            Destroy(Instantiate(destroyParticle, hit.transform.position, hit.transform.rotation), 2);

            balls.Remove(hit.collider.gameObject);
            Destroy(hit.collider.gameObject);
            GameObject sound = Instantiate(soundEffect);
            sound.GetComponent<AudioSource>().pitch = startPitch;
            if (startPitch < 1.5) { startPitch += 0.1f; }
            
            Destroy(sound, 2f);

            ballsLeft--;
        }
    }
    private void LateUpdate()
    {
        if (pauseMenu.activeSelf) { return; }
        if (!paused && Input.GetKeyDown(KeyCode.Mouse0))
        {
            CheckClick();
        }

        if (!timer.running && Input.GetKeyDown(KeyCode.Mouse0))
        {
            startUI.SetActive(false);
            timer.Start();
            paused = false;
            CheckClick();
        }
        
        if(timer.running && ballsLeft == 0)
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
        scoresUI.text += "[Attempt " + (level) + "(" + scores[scores.Count - 1].ToString("F4") + " seconds)] \n";
        level++;
        GetComponent<GameSetup>().CheckIfHighScore(scores[scores.Count - 1]);
        //numberOfBalls = level * 2;
        ballsLeft = numberOfBalls;
        timer.Reset();
        SpawnBalls();
    }

    public void SpawnBalls()
    {
        startPitch = 0.2f;
        for (int i = 0; i < numberOfBalls; i++)
        {
            GameObject GO = Instantiate(ball);
            balls.Add(GO);
            GO.transform.position = new Vector3(Random.Range(-(width/ 2) + 1, (width/ 2) - 1), Random.Range(-(height/ 2) + 1, (height/ 2) - 1), 0);
        }
    }
}