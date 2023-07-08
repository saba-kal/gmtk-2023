using UnityEngine;
using UnityEngine.Events;

public class CharacterAnimationEventListener : MonoBehaviour
{
    [SerializeField] private UnityEvent attackCompleteEvent;

    public void OnAttackComplete()
    {
        attackCompleteEvent?.Invoke();
    }
}