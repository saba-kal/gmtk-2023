using UnityEngine;


public class PlayerHealth : MonoBehaviour
{
    public int health = 3;

    private CharacterPossessor characterPossessor;

    private void Awake()
    {
        characterPossessor = GetComponent<CharacterPossessor>();
    }

    public void SetHealth(int newHealth)
    {
        health = newHealth;
        CharacterInfo.Instance?.UpdateHealth(newHealth);
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        health = Mathf.Max(health, 0);
        CharacterInfo.Instance?.UpdateHealth(health);
        if (health <= 0)
        {
            var possessSuccessful = characterPossessor.PossessRandomCharacter();
            if (!possessSuccessful)
            {
                GameManager.Instance.SetGameOver(true, false);
            }
        }
    }
}