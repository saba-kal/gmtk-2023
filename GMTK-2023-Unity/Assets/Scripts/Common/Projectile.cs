using UnityEngine;


public class Projectile : MonoBehaviour
{
    [SerializeField] private float lifeTime = 5.0f;
    [SerializeField] private bool destroyOnHit;
    [SerializeField] private Rigidbody rigidbody;
    [SerializeField] private GameObject effectPrefab;
    [SerializeField] private GameObject heroHitEffect;
    [SerializeField] private int damage = 1;
    [SerializeField] private int radius = 1;

    private HeroAI hero;
    private bool projectileCollided = false;

    private void Start()
    {
        Destroy(gameObject, lifeTime);
        hero = FindObjectOfType<HeroAI>();
    }

    public void SetVelocity(Vector3 velocity)
    {
        rigidbody.velocity = velocity;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (projectileCollided)
        {
            return;
        }

        projectileCollided = true;
        if (destroyOnHit)
        {
            Destroy(gameObject);
            if (effectPrefab != null)
            {
                var effect = Instantiate(effectPrefab, transform.position, Quaternion.identity);
                Destroy(effect, 5f);
            }
        }

        var heroHealth = collision.gameObject.GetComponent<HeroHealth>();
        var heroIsHit = false;
        if (heroHealth != null)
        {
            heroIsHit = true;
        }
        else
        {
            var distanceToHero = Vector3.Distance(hero.transform.position, transform.position);
            if (distanceToHero <= radius)
            {
                heroIsHit = true;
            }
        }

        if (heroIsHit)
        {
            var knockBack = hero.GetComponent<KnockBack>();
            knockBack?.KnockCharacterBack(rigidbody.velocity.normalized * 0.1f);

            heroHealth = hero.GetComponent<HeroHealth>();
            heroHealth.TakeDamage(damage);

            if (heroHitEffect != null)
            {
                var effect = Instantiate(heroHitEffect, transform.position, Quaternion.identity);
                Destroy(effect, 5f);
            }
        }
    }
}