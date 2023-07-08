using System.Collections.Generic;
using UnityEngine;


public class AISword : AIWeapon
{
    [SerializeField] private Transform attackPoint;
    [SerializeField] private float attackRadius;

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
            Debug.Log($"{gameObject.name} has swung his sword and hit {hero.gameObject.name}");
            // TODO: damage the hero.
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