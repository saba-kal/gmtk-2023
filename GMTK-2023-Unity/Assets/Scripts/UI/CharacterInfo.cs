using System.Collections.Generic;
using TMPro;
using UnityEngine;


public class CharacterInfo : MonoBehaviour
{
    public static CharacterInfo Instance { get; private set; }

    [SerializeField] private GameObject healthBarContainer;
    [SerializeField] private TextMeshProUGUI characterName;

    private List<GameObject> healthIcons = new List<GameObject>();

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

        foreach (Transform child in healthBarContainer.transform)
        {
            healthIcons.Add(child.gameObject);
        }
    }

    public void SetCharacterInfo(string name, int health)
    {
        characterName.text = name;
        UpdateHealth(health);
    }

    public void UpdateHealth(int health)
    {
        for (int i = 1; i <= healthIcons.Count; i++)
        {
            healthIcons[i - 1].SetActive(i <= health);
        }
    }
}