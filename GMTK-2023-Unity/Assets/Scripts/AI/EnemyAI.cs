using System.Collections;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyAI : MonoBehaviour
{
    public bool Disabled { get; private set; } = false;

    public string Name;
    public int MaxHealth;
    public WeaponType weaponType;

    [SerializeField] private Animator _animator;

    private GameObject _target;
    private NavMeshAgent _agent;
    private AIWeapon _weapon;
    private AIState _state = AIState.Persue;
    private bool _attackInProgress = false;

    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
        _weapon = GetComponent<AIWeapon>();
        _target = FindObjectOfType<HeroAI>().gameObject;
    }

    private void Update()
    {
        if (GameManager.GameIsOver)
        {
            return;
        }

        switch (_state)
        {
            case AIState.Persue:
                PersueTarget();
                break;
            case AIState.Attack:
                Attack();
                break;
        }
        UpdateAnimationParams();
    }

    public void Disable()
    {
        if (_agent == null)
        {
            _agent = GetComponent<NavMeshAgent>();
        }
        _agent.enabled = false;
        Disabled = true;
    }

    private void PersueTarget()
    {
        if (_agent.enabled && _agent.isOnNavMesh)
        {
            _agent.SetDestination(_target.transform.position);
        }

        var affectedObjects = _weapon.GetObjectsInRange();
        if (affectedObjects.Count > 0 && _weapon.IsReady())
        {
            SetState(AIState.Attack);
        }
    }

    private void Attack()
    {
        if (_agent.enabled && _agent.isOnNavMesh)
        {
            _agent.SetDestination(_target.transform.position);
        }

        if (_attackInProgress)
        {
            return;
        }

        _attackInProgress = true;
        var affectedObjects = _weapon.GetObjectsInRange();
        if (affectedObjects.Count > 0)
        {
            _animator.SetTrigger("attack");

            //Sometimes character gets stuck in attack state if animation gets interrupted.
            //This is needed to revert that after some time.
            StartCoroutine(SetStateAfterTime(AIState.Persue, 5f));
        }
    }

    public void CompleteAttack()
    {
        _weapon.Activate();
        SetState(AIState.Persue);
    }

    private void UpdateAnimationParams()
    {
        if (_agent.enabled && _agent.isOnNavMesh)
        {
            _animator.SetFloat("speed", _agent.velocity.sqrMagnitude);
        }
        else
        {
            _animator.SetFloat("speed", 0);
        }
    }

    private void SetState(AIState newState)
    {
        _state = newState;
        _attackInProgress = false;
    }

    private IEnumerator SetStateAfterTime(AIState newState, float time)
    {
        yield return new WaitForSeconds(time);
        SetState(newState);
    }
}

internal enum AIState
{
    Persue = 0,
    Attack = 1,
    KockedBack = 2
}