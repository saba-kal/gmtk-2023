using UnityEngine;
using UnityEngine.UI;

public class HeroHealthBar : MonoBehaviour
{
    public static HeroHealthBar Instance { get; private set; }

    [SerializeField] private Image healthBarFill;

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
    }

    public void SetHealth(float health)
    {
        healthBarFill.fillAmount = health;
    }
}