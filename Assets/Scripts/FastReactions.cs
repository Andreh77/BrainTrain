using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;

public class FastReactions : MonoBehaviour
{
    public GameObject startUI;
    public TMP_InputField inputField;
    public TMP_Text scoresUI;
    public List<double> scores = new List<double>();

    Timer timer;
    Timer timer2;

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
    public bool canPlay = true;
    public bool clicked = false;
    public GameObject panel;
    public GameObject waitText;
    public bool showing = false;
    public long showtime = 0;

    private void Start()
    { 
        height = Camera.main.orthographicSize * 2;
        width = height * Camera.main.aspect;
        startUI.SetActive(true);

        timer = new Timer();
        timer2 = new Timer();
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
        System.Console.WriteLine("update.....");

        System.TimeSpan ts = System.DateTime.UtcNow - new System.DateTime(1970, 1, 1, 0, 0, 0, 0);
        if (!showing && System.Convert.ToDouble(ts.TotalMilliseconds.ToString()) - showtime >= speed && System.Convert.ToDouble(ts.TotalMilliseconds.ToString()) - showtime <= speed+24 && !timer2.running)
            waitForShow();
    }
    public void CheckClick()
    {
        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
        if (!paused)
        {
            //Destroy(Instantiate(destroyParticle, hit.transform.position, hit.transform.rotation), 2);
            clicked = true;
            //balls.Remove(hit.collider.gameObject);
            GameObject sound = Instantiate(soundEffect);
            sound.GetComponent<AudioSource>().pitch = startPitch;
            if (startPitch < 1.5) { startPitch += 0.1f; }
            Destroy(sound, 2f);
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
            CheckClick();
            paused = false;
            waitText.SetActive(true);
            System.TimeSpan ts = System.DateTime.UtcNow - new System.DateTime(1970,1,1,0,0,0,0);
            showtime = (long)System.Convert.ToDouble(ts.TotalMilliseconds.ToString());
            System.Random rnd = new System.Random();
            speed = rnd.Next(1000, 8000);
        }

        if (timer.running && clicked )
        {
            timer.Stop();
            timer2.Stop();
            double score = timer2.time;
            scores.Add(score);
            startUI.SetActive(true);
            paused = true;
            waitText.SetActive(false);

            NextLevel();
        }
    }

    public void NextLevel()
    {
        if(scores.Count > 0)
        {
            scoresUI.text += "[Attempt " + (level) + "(" + scores[scores.Count - 1].ToString("F4") + " seconds)] \n";
            GameManager.instance.TryStoreHighScore("reflex", "Fast Reactions", scores[scores.Count - 1]);
        }

        level++;
        
        if (!showing)
        {
            scoresUI.text = "Instructions \n- Click when the screen truns green.\n";
            scores.Clear();
        }
   
        //GetComponent<GameSetup>().CheckIfHighScore("reflex", scores[scores.Count - 1]);
        clicked = false;
        timer.Reset(); 
        timer2.Reset();
        showing = false;
        panel.SetActive(false);
    }

    public void waitForShow()
    {
        if (clicked || showing) return;
        showing = true;
        panel.SetActive(true);
        timer2.Start();
        waitText.SetActive(false);
    }

}