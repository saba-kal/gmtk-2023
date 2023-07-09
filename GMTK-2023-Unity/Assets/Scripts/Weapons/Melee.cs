using UnityEngine;

public class Melee : PlayerWeapon
{
    [SerializeField] private Transform attackPoint;
    [SerializeField] private float attackRadius;
    [SerializeField] private int damage = 2;
    [SerializeField] private GameObject effectPrefab;

    private HeroAI hero;

    private void Start()
    {
        hero = FindObjectOfType<HeroAI>();
    }

    protected override void Update()
    {
        if (PauseMenu.GameIsPaused || GameManager.GameIsOver)
        {
            return;
        }

        base.Update();

        if (isDisabled)
        {
            return;
        }

        if (Input.GetMouseButtonDown(0))
        {
            Activate();
        }
    }

    protected override void PerformAttack()
    {
        Debug.Log("Performaing attack");
        var distanceToHero = Vector3.Distance(hero.transform.position, attackPoint.position);
        Debug.Log(distanceToHero);
        if (distanceToHero <= attackRadius)
        {
            Debug.Log($"{gameObject.name} has swung his sword and hit {hero.gameObject.name}");
            var knockBack = hero.GetComponent<KnockBack>();
            knockBack?.KnockCharacterBack((hero.transform.position - attackPoint.position).normalized);

            var effect = Instantiate(effectPrefab);
            effect.transform.position = hero.transform.position;
            Destroy(effect, 5f);

            var heroHealth = hero.GetComponent<HeroHealth>();
            heroHealth.TakeDamage(damage);
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