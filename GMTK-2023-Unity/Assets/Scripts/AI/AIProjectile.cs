using System.Collections.Generic;
using UnityEngine;


public class AIProjectile : AIWeapon
{
    [SerializeField] private float projectileSpeed;
    [SerializeField] private float yOffset = 0f;
    [SerializeField] private float range = 20f;
    [SerializeField] private Transform projectileSpawnPoint;
    [SerializeField] private Projectile rockPrefab;

    private HeroAI hero;

    private void Start()
    {
        hero = FindObjectOfType<HeroAI>();
    }

    public override List<GameObject> GetObjectsInRange()
    {
        var distance = Vector3.Distance(hero.transform.position, projectileSpawnPoint.position);
        if (distance <= range)
        {
            return new List<GameObject> { hero.gameObject };
        }

        return new List<GameObject>();
    }

    protected override void Fire()
    {
        var direction = (hero.transform.position - projectileSpawnPoint.position).normalized;
        direction.y += yOffset;
        direction = direction.normalized;

        var projectile = Instantiate(rockPrefab);
        projectile.transform.position = projectileSpawnPoint.position;
        projectile.SetVelocity(direction * projectileSpeed);
    }
}