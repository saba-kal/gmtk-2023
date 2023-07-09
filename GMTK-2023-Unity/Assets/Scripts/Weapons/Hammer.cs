using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Hammer : AIWeapon
{
    [SerializeField] private GameObject effectPrefab;
    [SerializeField] private Animator animator;
    [SerializeField] private float radius = 3f;
    [SerializeField] private float force = 100f;
    [SerializeField] private float delay = 0.8f;

    private List<EnemyAI> enemyCharacters;
    private GameObject player;

    private void Awake()
    {
        enemyCharacters = FindObjectsByType<EnemyAI>(FindObjectsSortMode.None).Where(x => x.tag != "Hero").ToList();
        player = FindObjectOfType<CharacterPossessor>().gameObject;
    }

    public override List<GameObject> GetObjectsInRange()
    {
        RemoveDeletedEnemyCharacters();
        var enemiesToCheck = enemyCharacters.Select(x => x.gameObject).ToList();
        enemiesToCheck.Add(player.gameObject);

        var objectsInRange = new List<GameObject>();
        foreach (var enemy in enemiesToCheck)
        {
            var distanceToEnemy = Vector3.Distance(enemy.transform.position, transform.position);
            if (distanceToEnemy <= radius)
            {
                objectsInRange.Add(enemy);
            }
        }

        return objectsInRange;
    }

    protected override void Fire()
    {
        animator.SetTrigger("attack");
        StartCoroutine(FireAfterTime());
    }

    private IEnumerator FireAfterTime()
    {
        yield return new WaitForSeconds(delay);

        var effect = Instantiate(effectPrefab, transform.position, Quaternion.identity);
        Destroy(effect, 5f);
        CameraShake.Instance?.Shake();

        var objectsInRange = GetObjectsInRange();
        foreach (var potentialEnemy in objectsInRange)
        {
            var direction = (potentialEnemy.transform.position - transform.position).normalized;
            var knockBack = potentialEnemy.GetComponent<KnockBack>();
            knockBack?.KnockCharacterBack(direction * force);

            var playerHealth = potentialEnemy.GetComponent<PlayerHealth>();
            if (playerHealth == null)
            {
                Destroy(potentialEnemy, 10f);
                AkSoundEngine.PostEvent("KoboldHit", potentialEnemy);
            }
            else
            {
                playerHealth.TakeDamage(1);
            }
        }

        RemoveDeletedEnemyCharacters();
    }

    private void RemoveDeletedEnemyCharacters()
    {
        var newEnemyCharacters = new List<EnemyAI>();
        foreach (var character in enemyCharacters)
        {
            if (character != null)
            {
                newEnemyCharacters.Add(character);
            }
        }
        enemyCharacters = newEnemyCharacters;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(1, 0, 0, 0.2f);
        Gizmos.DrawSphere(transform.position, radius);
    }
}