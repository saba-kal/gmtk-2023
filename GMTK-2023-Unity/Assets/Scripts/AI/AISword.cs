using System.Collections.Generic;
using UnityEngine;


public class AISword : AIWeapon
{
    [SerializeField] private Transform attackPoint;
    [SerializeField] private float attackRadius;
    [SerializeField] private int damage = 4;

    private HeroAI hero;

    private void Start()
    {
        hero = FindObjectOfType<HeroAI>();
    }

    public override List<GameObject> GetObjectsInRange()
    {
        var distanceToHero = Vector3.Distance(hero.transform.position, attackPoint.position);
        if (distanceToHero <= attackRadius)
        {
            return new List<GameObject> { hero.gameObject };
        }

        return new List<GameObject>();
    }

    protected override void Fire()
    {
        var distanceToHero = Vector3.Distance(hero.transform.position, attackPoint.position);
        if (distanceToHero <= attackRadius)
        {
            var health = hero.GetComponent<HeroHealth>();
            health?.TakeDamage(damage);
        }
    }

    private void OnDrawGizmos()
    {
        if (attackPoint == null)
        {
            return;
        }

        Gizmos.color = new Color(1.0f, 0.0f, 0.0f, 0.35f);
        Gizmos.DrawSphere(attackPoint.position, attackRadius);
    }
}