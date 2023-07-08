using UnityEngine;


public class GameOverMenu : MonoBehaviour
{
    [SerializeField] private GameObject gameOverScreen;

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
        }
    }
}