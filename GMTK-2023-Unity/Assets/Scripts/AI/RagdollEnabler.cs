using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class RagdollEnabler : MonoBehaviour
{
    public UnityAction OnRagdollEnable { get; set; }
    public UnityAction OnRagdollDisable { get; set; }

    [SerializeField] private Animator animator;
    [SerializeField] private Transform ragdollRoot;

    private NavMeshAgent navMeshAgent;
    private Rigidbody[] rigidbodies;
    private CharacterJoint[] joints;
    private Vector3 startPosition;
    private Quaternion startRotation;


    private void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        rigidbodies = GetComponentsInChildren<Rigidbody>();
        joints = GetComponentsInChildren<CharacterJoint>();
        startPosition = ragdollRoot.position;
        startRotation = ragdollRoot.rotation;
    }

    private void Start()
    {
        EnableAnimator();
    }

    public void EnableRagdoll()
    {
        animator.enabled = false;
        navMeshAgent.enabled = false;

        foreach (var rigidbody in rigidbodies)
        {
            rigidbody.detectCollisions = true;
            rigidbody.useGravity = true;
            rigidbody.isKinematic = false;
            rigidbody.velocity = Vector3.zero;
        }
        foreach (var joint in joints)
        {
            joint.enableCollision = true;
        }
        OnRagdollEnable?.Invoke();
    }

    public void EnableAnimator()
    {
        animator.enabled = true;
        navMeshAgent.enabled = true;
        navMeshAgent.updateRotation = true;

        foreach (var rigidbody in rigidbodies)
        {
            rigidbody.isKinematic = true;
            rigidbody.useGravity = false;
        }
        foreach (var joint in joints)
        {
            joint.enableCollision = false;
        }
        OnRagdollDisable?.Invoke();
    }
}