using System.Collections;
using UnityEngine;

public class AIKnockBack : KnockBack
{
    private RagdollEnabler ragdollEnabler;

    private void Awake()
    {
        ragdollEnabler = GetComponent<RagdollEnabler>();
    }

    public override void KnockCharacterBack(Vector3 force)
    {
        ragdollEnabler.EnableRagdoll();
        GetComponent<Rigidbody>().AddForce(force);
        StartCoroutine(DisableRagdollAfterTime());
    }

    private IEnumerator DisableRagdollAfterTime()
    {
        yield return new WaitForSeconds(2);
        ragdollEnabler.EnableAnimator();
    }
}