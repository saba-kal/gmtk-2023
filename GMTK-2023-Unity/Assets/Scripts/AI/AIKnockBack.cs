using UnityEngine;

public class AIKnockBack : KnockBack
{
    private RagdollEnabler ragdollEnabler;
    private Rigidbody[] rigidbodies;
    private EnemyAI enemyAi;


    private void Awake()
    {
        ragdollEnabler = GetComponent<RagdollEnabler>();
        rigidbodies = GetComponentsInChildren<Rigidbody>();
        enemyAi = GetComponent<EnemyAI>();
    }

    public override void KnockCharacterBack(Vector3 force)
    {
        ragdollEnabler.EnableRagdoll();
        foreach (var rigidbody in rigidbodies)
        {
            rigidbody.AddForce(force, ForceMode.Impulse);
        }
        enemyAi.Disable();
    }
}