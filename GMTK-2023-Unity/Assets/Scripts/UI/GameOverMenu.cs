using TMPro;
using UnityEngine;


public class GameOverMenu : MonoBehaviour
{
    [SerializeField] private GameObject gameOverScreen;
    [SerializeField] private GameObject playerWonIndicator;
    [SerializeField] private GameObject playerLostIndicator;

    [SerializeField] private TextMeshProUGUI koboldCountText;
    [SerializeField] private TextMeshProUGUI timeText;

    private float timeSinceGameStart = 0;
    private bool gameOverShown = false;

    private void Awake()
    {
        gameOverScreen.SetActive(false);
    }

    private void Update()
    {
        if (GameManager.GameIsOver && !gameOverShown)
        {
            ShowGameOver();
            gameOverShown = true;
        }
        else
        {
            timeSinceGameStart += Time.deltaTime;
        }
    }

    private void ShowGameOver()
    {
        gameOverScreen.SetActive(true);
        Cursor.lockState = CursorLockMode.None;

        playerWonIndicator.SetActive(GameManager.PlayerWon);
        playerLostIndicator.SetActive(!GameManager.PlayerWon);

        koboldCountText.text = "Kobolds left alive: " + KoboldCountTracker.KoboldCount;

        int minutes = Mathf.FloorToInt(timeSinceGameStart / 60F);
        int seconds = Mathf.FloorToInt(timeSinceGameStart - minutes * 60);
        string fromattedTime = string.Format("{0:0}:{1:00}", minutes, seconds);
        timeText.text = "Time: " + fromattedTime;
    }
}