using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public const int columns = 4;
    public const int rows = 2;

    public const float Xspace = 4f;
    public const float Yspace = -5f;

    [SerializeField] private FlashCards startObject;
    [SerializeField] private Sprite[] images;
    public Timer timer;
    private int[] Randomiser(int[] locations)
    {
        int[] array = locations.Clone() as int[];
        for (int i = 0; i < array.Length; i++)
        {
            int newArray = array[i];
            int j = Random.Range(i, array.Length);
            array[i] = array[j];
            array[j] = newArray;
        }
        return array;
    }

    private void Awake() 
    {
        audioManager = FindObjectOfType<AudioManager>();
    }

    private void Start()
    {
        timer = new Timer();
        attemptsText.color = attemptGrad.Evaluate(0f);
        int[] locations = { 0, 0, 1, 1, 2, 2, 3, 3 };
        locations = Randomiser(locations);

        Vector3 startPosition = startObject.transform.position;

        for (int i = 0; i < columns; i++)
        {
            for (int j = 0; j < rows; j++)
            {
                FlashCards gameImage;
                if (i == 0 && j == 0)
                {
                    gameImage = startObject;
                }
                else 
                {
                    gameImage = Instantiate(startObject) as FlashCards;
                }

                int index = j * columns + i;
                int id = locations[index];
                gameImage.ChangeSprite(id, images[id]);

                float positionX = (Xspace * i) + startPosition.x;
                float positionY = (Yspace * j) + startPosition.y;

                gameImage.transform.position = new Vector3(positionX, positionY, startPosition.z);
            }
        }

        timer.Start();
    }

    private FlashCards firstOpen;
    private FlashCards secondOpen;
    private AudioManager audioManager;

    private int score = 0;
    private int attempts = 0;

    [SerializeField] private TextMeshProUGUI scoreText, attemptsText, timerText, scoresUI;
    [SerializeField] private Gradient attemptGrad, timerGrad;

    public bool canOpen
    {
        get { return secondOpen == null; }
    }

    private void Update() 
    {
        if (score < 4)
        {
            timerText.text = "Time: " + Mathf.Round((float) timer.Stop()).ToString() + "s";
            timerText.color = timerGrad.Evaluate((float) timer.Stop() * 0.1f);
        } 
    }

    public void imageOpened(FlashCards startObject)
    {
        if (firstOpen == null)
        {
            firstOpen = startObject;
        }
        else 
        {
            secondOpen = startObject;
            StartCoroutine(CheckGuessed());
        }
    }

    private IEnumerator CheckGuessed()
    {
        if (firstOpen.spriteId == secondOpen.spriteId)
        {
            score++;
            audioManager.Play("Score");
            // timerText.text = "Time: " + Mathf.Round((float) timer.Stop()).ToString() + "s";
            scoreText.text = "Score: " + score.ToString();
        }
        else 
        {
            yield return new WaitForSeconds(0.5f);

            firstOpen.Close();
            secondOpen.Close();
        }

        attempts++;
        attemptsText.text = "Attempts: " + attempts;
        attemptsText.color = attemptGrad.Evaluate(attempts * 0.1f);

        firstOpen = null;
        secondOpen = null;

        if (score >= 4)
        {
            GameManager gameManager = FindObjectOfType<GameManager>();
            audioManager.Play("Select");
            gameManager.TryStoreHighScore(SceneManager.GetActiveScene().name, (double) score);
            PlayerPrefs.SetString("scoreText", PlayerPrefs.GetString("scoreText") + "[Attempt " + (PlayerPrefs.GetInt("FCAttempt", 0)) + " (" + Mathf.Round((float) timer.Stop()).ToString() + "s, " + attempts +" guesses)] \n");
            scoresUI.text = PlayerPrefs.GetString("scoreText");
        } 
    }

    public void Restart()
    {
        audioManager.Play("Decline");
        PlayerPrefs.SetInt("FCAttempt", PlayerPrefs.GetInt("FCAttempt", -1) + 1);
        SceneManager.LoadScene("FlashCards");
    }
}
