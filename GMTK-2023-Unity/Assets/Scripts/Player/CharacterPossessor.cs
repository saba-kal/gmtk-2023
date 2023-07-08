using StarterAssets;
using System.Collections;
using System.Linq;
using UnityEngine;

public class CharacterPossessor : MonoBehaviour
{
    [SerializeField] private GameObject testPrefab;
    [SerializeField] private float transferDuration = 0.2f;

    private CharacterController _characterController;
    private FirstPersonController _firstPersonController;
    private EnemyAI _possessedCharacter;

    private void Start()
    {
        _characterController = GetComponent<CharacterController>();
        _firstPersonController = GetComponent<FirstPersonController>();
        PossessRandomCharacter();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            PossessRandomCharacter();
        }
    }

    private void PossessRandomCharacter()
    {

        var enemyCharacters = FindObjectsByType<EnemyAI>(FindObjectsSortMode.None).Where(x => x.tag != "Hero").ToList();
        if (enemyCharacters.Count == 0)
        {
            Debug.LogWarning("No more character left to possess.");
            return;
        }

        var characterIndex = Random.Range(0, enemyCharacters.Count);
        _possessedCharacter = enemyCharacters[characterIndex];
        _possessedCharacter.Disable();

        StartCoroutine(MoveTowardsPossesedCharacter());
    }

    private IEnumerator MoveTowardsPossesedCharacter()
    {
        _characterController.enabled = false;
        _firstPersonController.enabled = false;

        var startPosition = transform.position;
        var startRotation = transform.rotation;
        var endPosition = _possessedCharacter.transform.position;
        var endRotation = _possessedCharacter.transform.rotation;
        var transferTime = 0f;
        while (transferTime <= transferDuration)
        {
            transform.position = Vector3.Lerp(startPosition, endPosition, transferTime / transferDuration);
            transform.rotation = Quaternion.Lerp(startRotation, endRotation, transferTime / transferDuration);
            transferTime += Time.deltaTime;
            yield return null;
        }

        _characterController.enabled = true;
        _firstPersonController.enabled = true;
        Destroy(_possessedCharacter.gameObject);
    }
}
