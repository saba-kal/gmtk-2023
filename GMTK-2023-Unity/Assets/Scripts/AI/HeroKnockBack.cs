using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class HeroKnockBack : KnockBack
{
    [SerializeField] private float knockBackDuration = 0.1f;
    [SerializeField] private float knockForce = 40f;

    private NavMeshAgent agent;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
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
            agent.Move(direction * knockForce * Time.deltaTime);
            knockBackTime += Time.deltaTime;
            yield return null;
        }
    }
}