using UnityEngine;


public class GameOverMenu : MonoBehaviour
{
    [SerializeField] private GameObject gameOverScreen;
    [SerializeField] private GameObject playerWonIndicator;
    [SerializeField] private GameObject playerLostIndicator;

    private void Awake()
    {
        gameOverScreen.SetActive(false);
    }

    private void Update()
    {
        if (GameManager.GameIsOver)
        {
            gameOverScreen.SetActive(true);
            Cursor.lockState = CursorLockMode.None;

            playerWonIndicator.SetActive(GameManager.PlayerWon);
            playerLostIndicator.SetActive(!GameManager.PlayerWon);
        }
    }
}