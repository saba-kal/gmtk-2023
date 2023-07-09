using System.Collections;
using UnityEngine;

public abstract class PlayerWeapon : Weapon
{
    [SerializeField] private float attackDelay = 0.5f;
    [SerializeField] private GameObject weaponModel;
    [SerializeField] private Animator weaponAnimator;

    protected bool isDisabled = false;

    public void SetActive(bool active)
    {
        weaponModel.SetActive(active);
        isDisabled = !active;
    }

    protected override void Fire()
    {
        weaponAnimator.SetTrigger("attack");
        StartCoroutine(FireAfterDelay());
    }

    protected virtual IEnumerator FireAfterDelay()
    {
        yield return new WaitForSeconds(attackDelay);
        PerformAttack();
    }

    protected abstract void PerformAttack();
}