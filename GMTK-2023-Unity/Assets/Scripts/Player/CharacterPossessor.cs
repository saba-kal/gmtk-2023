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
    private PlayerHealth _playerHealth;
    private WeaponManager weaponManager;

    private void Start()
    {
        _characterController = GetComponent<CharacterController>();
        _firstPersonController = GetComponent<FirstPersonController>();
        _playerHealth = GetComponent<PlayerHealth>();
        weaponManager = GetComponent<WeaponManager>();
        PossessRandomCharacter();
    }

    public bool PossessRandomCharacter()
    {
        var enemyCharacters = FindObjectsByType<EnemyAI>(FindObjectsSortMode.None)
            .Where(x => x.tag != "Hero" && !x.Disabled)
            .ToList();
        if (enemyCharacters.Count == 0)
        {
            Debug.LogWarning("No more character left to possess.");
            return false;
        }

        weaponManager.DisableAllWeapons();

        var characterIndex = Random.Range(0, enemyCharacters.Count);
        _possessedCharacter = enemyCharacters[characterIndex];
        _possessedCharacter.Disable();

        StartCoroutine(MoveTowardsPossesedCharacter());
        return true;
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

        CharacterInfo.Instance?.SetCharacterInfo(_possessedCharacter.Name, _possessedCharacter.MaxHealth);
        _playerHealth.SetHealth(_possessedCharacter.MaxHealth);
        weaponManager.SwitchTo(_possessedCharacter.weaponType);
    }
}
