using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ReactionTime : MonoBehaviour
{
    [SerializeField] private Color greenColor, redColor, blueColor, yellowColor;
    [SerializeField] private Canvas pause;
    [SerializeField] private TextMeshProUGUI promptText;
    [SerializeField] private float startTime, endTime, maxTimeAllowed;

    private AudioManager audioManager;
    private SpriteRenderer background;
    private IEnumerator flicker, playerAction;

    private enum PlayerDelay { early, perfect, late }

    private enum GameState { red, green, blue, yellow}

    private GameState gameState;
    private PlayerDelay playerDelay;

    private bool showMenu;
    public bool touchingUI;

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
        touchingUI = false;
        background = GetComponent<SpriteRenderer>();
        gameState = GameState.yellow;
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
                else promptText.text = "Perfect!";
                
                background.color = blueColor;
                break;
            case GameState.yellow:
                showMenu = true;
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
        // StartCoroutine(PlayerAction());

        if (playerAction != null) StopCoroutine(playerAction);
        playerAction = PlayerAction();
        StartCoroutine(playerAction);
        
        audioManager.Play("Red");
        gameState = GameState.red;
        
        yield return new WaitForSeconds(Random.Range(startTime, endTime));
        audioManager.Play("Green");
        gameState = GameState.green;

        yield return new WaitForSeconds(maxTimeAllowed);
        audioManager.Play("Drum Roll");
        playerDelay = PlayerDelay.late;
        gameState = GameState.blue;

        StartCoroutine(Repeat());
    }

    // Checks to see if player clicks too early, too late or right on time.
    IEnumerator PlayerAction()
    {
        yield return new WaitUntil(() => Input.GetMouseButtonDown(0));
    
        StopCoroutine(flicker);
        playerDelay = (gameState == GameState.red) ? PlayerDelay.early : PlayerDelay.perfect;
        if (playerDelay == PlayerDelay.perfect) audioManager.Play("IdkLolz"); 
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
        yield return new WaitUntil(() => Input.GetMouseButtonDown(0));
        
        yield return new WaitUntil(() => Input.GetMouseButtonUp(0));
        StopAllCoroutines();
        audioManager.Play("Accept");
        gameState = GameState.yellow;
    }
}
