using UnityEngine;
using UnityEngine.AI;

public class RagdollEnabler : MonoBehaviour
{
    private Rigidbody rigidbody;
    private NavMeshAgent navMeshAgent;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    private void Start()
    {
        EnableAnimator();
    }

    public void EnableRagdoll()
    {
        navMeshAgent.enabled = false;
        rigidbody.isKinematic = false;
        rigidbody.useGravity = true;
    }

    public void EnableAnimator()
    {
        navMeshAgent.enabled = true;
        rigidbody.isKinematic = true;
        rigidbody.useGravity = false;
    }
}