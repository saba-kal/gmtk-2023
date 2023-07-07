using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused { get; private set; } = false;

    [SerializeField] private GameObject _pauseScreen;

    private void Start()
    {
        ResumeGame();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P))
        {
            if (GameIsPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    public void ResumeGame()
    {
        GameIsPaused = false;
        Time.timeScale = 1f;
        _pauseScreen.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void PauseGame()
    {
        GameIsPaused = true;
        Time.timeScale = 0;
        _pauseScreen.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
    }
}
