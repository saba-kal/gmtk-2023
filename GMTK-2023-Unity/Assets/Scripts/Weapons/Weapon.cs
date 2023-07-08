using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    [SerializeField] private float _coolDown = 0.5f;

    private float _timeSinceLastActivation = 0f;

    private void Update()
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

    protected abstract void Fire();
}