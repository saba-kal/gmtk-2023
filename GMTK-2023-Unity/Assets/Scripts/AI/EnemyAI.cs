using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyAI : MonoBehaviour
{
    [SerializeField] private GameObject _target;

    private NavMeshAgent _agent;

    private void Start()
    {
        if (_agent == null)
        {
            _agent = GetComponent<NavMeshAgent>();
        }
    }

    private void Update()
    {
        if (_agent.enabled && _agent.isOnNavMesh)
        {
            _agent.SetDestination(_target.transform.position);
        }
    }

    public void Disable()
    {
        if (_agent == null)
        {
            _agent = GetComponent<NavMeshAgent>();
        }
        _agent.enabled = false;
    }
}