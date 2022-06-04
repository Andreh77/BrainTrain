using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class SpeedClicker : MonoBehaviour
{
    [SerializeField] private GameObject startUI, destroyParticle, soundEffect;
    [SerializeField] private TextMeshProUGUI scoresUI;
    [HideInInspector] public bool canPlay = true;
    
    private AudioManager audioManager;
    private float startPitch = 0.5f;
    private Timer timer;
    
    private int level = 1, clicksLeft = 0, clickLimit = 10;
    
    private float score, height, width;
    

    // Start is called before the first frame update
    void Start()
    {
        audioManager = FindObjectOfType<AudioManager>();
        height = Camera.main.orthographicSize * 2;
        
        width = height * Camera.main.aspect;
        startUI.SetActive(true);
        
        clicksLeft = clickLimit;
        timer = new Timer();    
    }

    // Update is called once per frame
    void Update()
    {
        if (!canPlay) return;
        if (Input.GetMouseButtonDown(0)) StartCoroutine(Click());

        if (!timer.running && Input.GetKeyDown(KeyCode.Mouse0))
        {
            startUI.SetActive(false);
            timer.Start();
            Click();
        }
    }

    public void MouseOverUI(BaseEventData data)
    {
        canPlay = false;
    }

    public void MouseOffUI(BaseEventData data)
    {
        canPlay = true;
    }

    IEnumerator Click()
    {
        clicksLeft--;
        GameObject sound = Instantiate(soundEffect);
        sound.GetComponent<AudioSource>().pitch = startPitch;
        if (startPitch < 1.5) { startPitch += 0.1f; }
            
        Destroy(sound, 2f);
        Destroy(Instantiate(destroyParticle, new Vector3(0, 0, 0), new Quaternion(0, 0, 0, 0)), 2);
        yield return new WaitUntil(() => Input.GetMouseButtonUp(0));

        if(timer.running && clicksLeft == 0)
        {
            startUI.SetActive(true);
            NextLevel();
        }
    }

    public void NextLevel()
    {
        timer.Stop();
        double score = timer.time;
        scoresUI.text += "[Attempt " + (level) + "(" + (float) System.Math.Round(score, 2) + " seconds)] \n";
        level++;
        StatsData.speedClickerTimeScore.Add((float) System.Math.Round(score, 2));
        clicksLeft = clickLimit;
        timer.Reset();
        startPitch = 0.2f;
    }
}
