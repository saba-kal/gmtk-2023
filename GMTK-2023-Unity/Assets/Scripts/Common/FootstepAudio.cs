using UnityEngine;
using UnityEngine.AI;

public class FootstepAudio : MonoBehaviour
{
    [SerializeField] private string soundName;
    [SerializeField] private float footStepInterval = 0.2f;

    private float _timeSinceLastFootstep = 0;
    private CharacterController characterController;
    private NavMeshAgent agent;

    private bool isGrounded;
    private float speed;

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
        agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        SetPhysicsParams();

        if (speed > 0.0f && isGrounded)
        {
            if (_timeSinceLastFootstep > footStepInterval)
            {
                AkSoundEngine.PostEvent(soundName, gameObject);
                _timeSinceLastFootstep = 0;
            }
            _timeSinceLastFootstep += Time.deltaTime;
        }
    }

    private void SetPhysicsParams()
    {
        if (characterController != null)
        {
            isGrounded = characterController.isGrounded;
            speed = characterController.velocity.sqrMagnitude;
        }
        else if (agent != null)
        {
            isGrounded = true;
            speed = agent.velocity.sqrMagnitude;
        }
    }
}