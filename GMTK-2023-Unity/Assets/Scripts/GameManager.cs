using UnityEngine;


public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public static bool GameIsOver { get; private set; } = false;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        GameIsOver = false;
    }

    public void SetGameOver(bool gameOver)
    {
        GameIsOver = gameOver;
    }
}