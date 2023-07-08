using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class HeroAI : MonoBehaviour
{
    [SerializeField] private GameObject _target;

    private List<AIWeapon> weapons;
    private NavMeshAgent _agent;

    // Start is called before the first frame update
    private void Start()
    {
        if (_agent == null)
        {
            _agent = GetComponent<NavMeshAgent>();
        }
        weapons = GetComponentsInChildren<AIWeapon>().ToList();
    }

    // Update is called once per frame
    private void Update()
    {
        if (_target == null || GameManager.GameIsOver)
        {
            return;
        }

        if (_agent.enabled)
        {
            _agent.SetDestination(_target.transform.position);
        }

        foreach (var weapon in weapons)
        {
            var objectsInRange = weapon.GetObjectsInRange();
            if (objectsInRange.Count > 0)
            {
                weapon.Activate();
            }
        }
    }
}
