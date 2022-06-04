using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine;

public class ReactionTime : MonoBehaviour
{
    [SerializeField] private Color greenColor, redColor, blueColor, yellowColor;
    [SerializeField] private Canvas pause;
    [SerializeField] private TextMeshProUGUI promptText, scoresUI, timeText;
    [SerializeField] private float startTime, endTime, maxTimeAllowed; // Set maxTImeAllowed to 0 to remove the time limit
    [HideInInspector] public bool touchingUI;

    private AudioManager audioManager;
    private GameManager gameManager;
    private SpriteRenderer background;
    private IEnumerator flicker, playerAction;

    private enum PlayerDelay { early, perfect, late }

    private enum GameState { red, green, blue, yellow}
    private string[] textLines = {"Perfect!", "Nice One!", "Flawless!", "Superb!", "Supercalifragilisticexpialidocious!", "Sweet!", "Good Job!", "Fantastic!"};

    private GameState gameState;
    private PlayerDelay playerDelay;
    private Timer timer;

    private bool showMenu;
    private float score;

    public void SetTouchTrue()
    {
        touchingUI = true;
    }
    
    public void SetTouchFalse()
    {
        touchingUI = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        audioManager = FindObjectOfType<AudioManager>();
        gameManager = FindObjectOfType<GameManager>();
        background = GetComponent<SpriteRenderer>();
        
        gameState = GameState.yellow;
        touchingUI = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !touchingUI && gameState == GameState.yellow)
        {
            if (flicker != null) StopCoroutine(flicker);
            flicker = Flicker();
            StartCoroutine(flicker);
        }
        
        pause.enabled = (showMenu) ? true : false;
        ResolveUI();
    }

    // Determines whether or not the main menu should be shown, what color the background should be and the text.
    private void ResolveUI()
    {
        switch (gameState)
        {
            case GameState.red:
                showMenu = false;
                promptText.text = "Wait For It...";
                
                background.color = redColor;
                break;
            case GameState.green:
                promptText.text = "Green light!";
                background.color = greenColor;
                break;
            case GameState.blue:
                if (playerDelay == PlayerDelay.early) promptText.text = "Too early..";
                else if (playerDelay == PlayerDelay.late) promptText.text = "Too late..";
                else
                {
                    timeText.enabled = true;
                } 
                
                background.color = blueColor;
                break;
            case GameState.yellow:
                showMenu = true;
                timeText.enabled = false;
                promptText.text = (Input.GetMouseButton(0) && !touchingUI) ? "Release when ready." : "HOLD LEFT CLICK TO BEGIN";
                
                background.color = yellowColor;
                break;
        }
    }

    // Flickers the screen from red to green after x seconds.
    IEnumerator Flicker()
    {
        audioManager.Play("Hold");
        yield return new WaitUntil(() => Input.GetMouseButtonUp(0));
        if (playerAction != null) StopCoroutine(playerAction);
        
        playerAction = PlayerAction();
        StartCoroutine(playerAction);
        
        audioManager.Play("Red");
        gameState = GameState.red;
        
        yield return new WaitForSeconds(Random.Range(startTime, endTime));
        
        timer = new Timer();
        timer.Start();
        
        audioManager.Play("Green");
        gameState = GameState.green;

        if (maxTimeAllowed > 0)
        {
            yield return new WaitForSeconds(maxTimeAllowed);
            audioManager.Play("Drum Roll");
            playerDelay = PlayerDelay.late;
            gameState = GameState.blue;

            StartCoroutine(Repeat());
        }
    }

    // Checks to see if player clicks too early, too late or right on time.
    IEnumerator PlayerAction()
    {
        yield return new WaitUntil(() => Input.GetMouseButtonDown(0));
    
        StopCoroutine(flicker);
        score = (Mathf.Ceil((float)timer.Stop() * 1000));
        playerDelay = (gameState == GameState.red) ? PlayerDelay.early : PlayerDelay.perfect;
        
        if (playerDelay == PlayerDelay.perfect)
        {
            audioManager.Play("IdkLolz"); 
            promptText.text = textLines[Random.Range(0, textLines.Length)];
        } 
        else if (playerDelay == PlayerDelay.early) audioManager.Play("Clack");
        gameState = GameState.blue;

        yield return new WaitUntil(() => Input.GetMouseButtonUp(0));
        StartCoroutine(Repeat());
    }

    // Click the mouse button again to restart.
    IEnumerator Repeat()
    {
        StopCoroutine(flicker);
        StopCoroutine(playerAction);
        timeText.text = "(" + score.ToString() + " ms)";
        yield return new WaitUntil(() => Input.GetMouseButtonDown(0));
        
        yield return new WaitUntil(() => Input.GetMouseButtonUp(0));
        StopAllCoroutines();
        audioManager.Play("Accept");
        gameState = GameState.yellow;
        SetScoreText();
        
    }

    void SetScoreText()
    {
        gameManager.TryStoreHighScore(SceneManager.GetActiveScene().name, 2);
        PlayerPrefs.SetString("reflexText", PlayerPrefs.GetString("reflexText") + "[Attempt " + (PlayerPrefs.GetInt("RTAttempt", 0)) + " (" + score.ToString() + " ms" + ")] \n");
        scoresUI.text = PlayerPrefs.GetString("reflexText");
        PlayerPrefs.SetInt("RTAttempt", PlayerPrefs.GetInt("RTAttempt", -1) + 1);

        StatsData.reactionTimeScore.Add(score);
    }
}
