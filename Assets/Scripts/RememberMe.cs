using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;
public class RememberMe : GameMode
{
    public GameObject startUI;

    public int numberOfBalls = 4;
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
    [Range(0.0f, 1.0f)]
    public float spread = 1;
    public float speed = 1;
    public float scale = 1;
    public GameObject destroyParticle;
    public GameObject soundEffect;
    public float startPitch = 0.5f;
    GameObject ballClosestToMouse = null;
    public List<GameObject> balls = new List<GameObject>();
    public bool canPlay = true;
    public bool canClick = false;
    public long hidetime = 0;
    public GameObject panel;
    public GameObject ballsTxt;
    public int retrytimes = 4;
    public GameObject retrylabel;


    private void Start()
    {
        height = Camera.main.orthographicSize * 2;
        width = height * Camera.main.aspect;
        startUI.SetActive(true);
        numberOfBalls += level;
        ballsLeft = numberOfBalls;
        SpawnBalls();
        timer = new Timer();
        retrylabel.GetComponent<TMPro.TextMeshProUGUI>().text = retrytimes + "";
    }

    public void MouseOverUI(BaseEventData data)
    {
        canPlay = false;
    }

    public void MouseOffUI(BaseEventData data)
    {
        canPlay = true;
    }

    private void Update()
    {
        ballsTxt.GetComponent<TMPro.TextMeshProUGUI>().text = ballsLeft + "";
        foreach (GameObject ball in balls)
        {
            ball.GetComponent<SpriteRenderer>().color = Color.black;
            if (ballClosestToMouse == null) { ballClosestToMouse = ball; }

            if (Vector3.Distance(ballClosestToMouse.transform.position, Camera.main.ScreenToWorldPoint(Input.mousePosition)) > Vector3.Distance(ball.transform.position, Camera.main.ScreenToWorldPoint(Input.mousePosition)))
            {
                ballClosestToMouse = ball;
            }
        }
        //ballClosestToMouse.GetComponent<SpriteRenderer>().color = Color.white;
        Debug.DrawLine(Camera.main.ScreenToWorldPoint(Input.mousePosition), ballClosestToMouse.transform.position, Color.blue);
        System.TimeSpan ts = System.DateTime.UtcNow - new System.DateTime(1970, 1, 1, 0, 0, 0, 0);
        long now = (long)System.Convert.ToDouble(ts.TotalMilliseconds.ToString());
        if (now - hidetime >= 5000 && !canClick && hidetime != 0) {
            canClick = true;
            //balls.ForEach(item => { 
            //    System.Console.WriteLine();
            //    item.GetComponent<Renderer>().material.color = Color.green;
            //});
            panel.SetActive(true);

        }
    }
    public void CheckClick()
    {
        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
        if (!canClick) return;
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
        else if(retrytimes <= 1)
        {
           restart();
        } else
        {
            retrytimes--;
            retrylabel.GetComponent<TMPro.TextMeshProUGUI>().text = retrytimes + "";
        }
    }
    private void LateUpdate()
    {
        if (!canPlay) return;

        if (Input.GetKeyDown(KeyCode.Mouse0))
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

        if (timer.running && ballsLeft == 0)
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
        retrytimes = 4;
        scoresUI.text += "[Attempt " + (level) + "(" + scores[scores.Count - 1].ToString("F4") + " seconds)] \n";
        level++;
        canClick = false;
        GameSetup gs = GetComponent<GameSetup>();
        try
        {
            if(gs != null)
                gs.CheckIfHighScore(scores[scores.Count - 1], level);
        } catch (System.Exception ex) {
            System.Console.WriteLine(ex.ToString());
        }

        numberOfBalls = level + numberOfBalls;
        ballsLeft = numberOfBalls;
        timer.Reset();
        SpawnBalls();
        panel.SetActive(false);
        retrylabel.GetComponent<TMPro.TextMeshProUGUI>().text = retrytimes + "";
    }

    public void SpawnBalls()
    {
        startPitch = 0.2f;
        for (int i = 0; i < numberOfBalls; i++)
        {
            GameObject GO = Instantiate(ball);
            balls.Add(GO);
            GO.transform.localScale = new Vector2(scale, scale);
            GO.GetComponent<Movement>().speed = speed;
            GO.transform.position = new Vector3(Random.Range((-(width / 2) + 1) * spread, ((width / 2) - 1)) * spread, Random.Range((-(height / 2) + 1) * spread, ((height / 2) - 1)) * spread, 0);

        }
        System.TimeSpan ts = System.DateTime.UtcNow - new System.DateTime(1970, 1, 1, 0, 0, 0, 0);
        hidetime = (long)System.Convert.ToDouble(ts.TotalMilliseconds.ToString());
    }

    public void restart() {
        ballsLeft = 0;
        level = 0;
        timer.Stop();
        scores.Clear();
        scores.Add(timer.time);
        startUI.SetActive(true);
        paused = true;
        numberOfBalls = 4;
        for (int i = balls.Count-1; i >= 0; i--) {
            GameObject item = balls[i];
            Destroy(Instantiate(destroyParticle, item.transform.position, item.transform.rotation), 2);
            balls.Remove(item);
            Destroy(item);
        }
        NextLevel();
    }
}
