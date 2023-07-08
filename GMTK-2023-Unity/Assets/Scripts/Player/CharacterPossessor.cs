using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CharacterPossessor : MonoBehaviour
{
    [SerializeField] private GameObject testPrefab;
    [SerializeField] private float transferDuration = 0.2f;

    private CharacterController _characterController;
    private FirstPersonController _firstPersonController;
    private EnemyAI _possessedCharacter;
    private List<EnemyAI> _enemyCharacters;

    private int _currentCharacterIndex = -1;

    private void Start()
    {
        _characterController = GetComponent<CharacterController>();
        _firstPersonController = GetComponent<FirstPersonController>();
        _enemyCharacters = FindObjectsByType<EnemyAI>(FindObjectsSortMode.None).Where(x => x.tag != "Hero").ToList();
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
        if (_enemyCharacters.Count == 0)
        {
            Debug.LogWarning("No more character left to possess.");
            return;
        }

        var newCharacterIndex = Random.Range(0, _enemyCharacters.Count);
        var tries = 0;
        const int maxTries = 10;
        while (newCharacterIndex == _currentCharacterIndex && tries < maxTries)
        {
            newCharacterIndex = Random.Range(0, _enemyCharacters.Count);
            tries++;
        }

        if (tries >= maxTries)
        {
            Debug.LogError($"Could not generate random character index after {maxTries} tries.");
            return;
        }


        _currentCharacterIndex = newCharacterIndex;
        _possessedCharacter = _enemyCharacters[newCharacterIndex];
        _enemyCharacters.RemoveAt(_currentCharacterIndex);
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
        _possessedCharacter.gameObject.SetActive(false);
    }
}
