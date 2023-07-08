using System.Collections;
using UnityEngine;


public class PlayerKnockBack : KnockBack
{
    [SerializeField] private float knockBackDuration = 0.1f;
    [SerializeField] private float knockForce = 40f;

    private CharacterController characterController;

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
    }

    public override void KnockCharacterBack(Vector3 direction)
    {
        direction.y += 0.3f; //Add a bit of upward movement.
        StartCoroutine(MoveCharacterTowards(direction));
    }

    private IEnumerator MoveCharacterTowards(Vector3 direction)
    {
        var knockBackTime = 0f;
        while (knockBackTime <= knockBackDuration)
        {
            characterController.Move(direction.normalized * knockForce * Time.deltaTime);
            knockBackTime += Time.deltaTime;
            yield return null;
        }
    }
}