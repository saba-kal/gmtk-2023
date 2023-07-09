using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    [SerializeField] private float _coolDown = 0.5f;

    private float _timeSinceLastActivation = 0f;

    protected virtual void Update()
    {
        _timeSinceLastActivation += Time.deltaTime;
    }

    public void Activate()
    {
        if (_timeSinceLastActivation < _coolDown)
        {
            return;
        }

        _timeSinceLastActivation = 0f;
        Fire();
    }

    public bool IsReady()
    {
        return _timeSinceLastActivation >= _coolDown;
    }

    public void ResetCooldown()
    {
        _timeSinceLastActivation = 0;
    }

    protected abstract void Fire();
}