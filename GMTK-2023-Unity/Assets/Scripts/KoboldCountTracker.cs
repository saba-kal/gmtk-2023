using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class KoboldCountTracker : MonoBehaviour
{
    public static KoboldCountTracker Instance { get; private set; }
    public static int KoboldCount = 0;

    private List<EnemyAI> kobolds;

    [SerializeField] private TextMeshProUGUI countText;

    private void Awake()
    {
        KoboldCount = 0;
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        kobolds = FindObjectsOfType<EnemyAI>().ToList();
    }

    private void Update()
    {
        var liveKobolds = new List<EnemyAI>();
        foreach (var ai in kobolds)
        {
            if (ai != null && !ai.Disabled)
            {
                liveKobolds.Add(ai);
            }
        }
        kobolds = liveKobolds;

        countText.text = "Allies: " + kobolds.Count.ToString();
        KoboldCount = kobolds.Count;
    }
}
