using UnityEngine;


public class HeroHealth : MonoBehaviour
{
    public int Health = 20;

    private int startHealth = 20;

    private void Start()
    {
        startHealth = Health;
    }

    public void TakeDamage(int damage)
    {
        Health -= damage;
        Health = Mathf.Max(Health, 0);

        HeroHealthBar.Instance.SetHealth(Health / (float)startHealth);

        //TODO: UI
        if (Health <= 0)
        {
            GameManager.Instance.SetGameOver(true, true);
        }
    }
}