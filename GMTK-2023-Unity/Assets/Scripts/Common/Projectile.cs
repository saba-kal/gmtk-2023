using UnityEngine;


public class Projectile : MonoBehaviour
{
    [SerializeField] private float lifeTime = 5.0f;
    [SerializeField] private bool destroyOnHit;
    [SerializeField] private Rigidbody rigidbody;
    [SerializeField] private GameObject effectPrefab;

    private void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    public void SetVelocity(Vector3 velocity)
    {
        rigidbody.velocity = velocity;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (destroyOnHit)
        {
            Destroy(gameObject);
            if (effectPrefab != null)
            {
                var effect = Instantiate(effectPrefab, transform.position, Quaternion.identity);
                Destroy(effect, 5f);
            }
        }
    }
}