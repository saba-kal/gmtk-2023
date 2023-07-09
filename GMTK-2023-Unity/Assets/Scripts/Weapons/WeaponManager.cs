using System.Collections.Generic;
using UnityEngine;

public enum WeaponType
{
    Melee = 0,
    Rock = 1,
    magic = 2
}

public class WeaponManager : MonoBehaviour
{
    [SerializeField] private List<PlayerWeapon> weapons = new List<PlayerWeapon>();
    [SerializeField] private WeaponType m_WeaponType;

    private void Start()
    {
        foreach (Transform item in transform)
        {
            if (item.TryGetComponent(out PlayerWeapon weaponBase))
            {
                if (!weapons.Contains(weaponBase))
                {
                    weapons.Add(weaponBase);
                }
                weaponBase.gameObject.SetActive(false);
                switch (m_WeaponType)
                {
                    case WeaponType.Rock:
                        if (weaponBase is Rock)
                        {
                            weaponBase.SetActive(true);
                        }
                        break;
                    case WeaponType.Melee:
                        if (weaponBase is Melee)
                        {
                            weaponBase.SetActive(true);
                        }
                        break;
                    case WeaponType.magic:
                        if (weaponBase is Magic)
                        {
                            weaponBase.SetActive(true);
                        }
                        break;
                    default:
                        break;
                }
            }

        }
    }


    public void SwitchTo(WeaponType weaponType)
    {
        foreach (var weaponBase in weapons)
        {
            weaponBase.SetActive(false);
            switch (weaponType)
            {
                case WeaponType.Rock:
                    if (weaponBase is Rock)
                    {
                        weaponBase.SetActive(true);
                    }
                    break;
                case WeaponType.Melee:
                    if (weaponBase is Melee)
                    {
                        weaponBase.SetActive(true);
                    }
                    break;
                case WeaponType.magic:
                    if (weaponBase is Magic)
                    {
                        weaponBase.SetActive(true);
                    }
                    break;
                default:
                    break;
            }
        }
    }

    public void DisableAllWeapons()
    {
        foreach (var weaponBase in weapons)
        {
            weaponBase.SetActive(false);
        }
    }
}
