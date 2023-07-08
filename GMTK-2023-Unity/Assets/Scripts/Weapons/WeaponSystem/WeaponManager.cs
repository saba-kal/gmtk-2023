using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    [SerializeField] List<WeaponBase> Guns = new List<WeaponBase>();
    [SerializeField] WeaponType m_WeaponType;

    public enum WeaponType
    {
        Pistol = 0,
        MachineGun = 1,
    }
    void Start()
    {
        foreach (Transform item in transform)
        {
            if (item.TryGetComponent(out WeaponBase WeaponBase))
            {
                if (!Guns.Contains(WeaponBase))
                {
                    Guns.Add(WeaponBase);
                }
                WeaponBase.gameObject.SetActive(false);
                switch (m_WeaponType)
                {
                    case WeaponType.Pistol:
                        if (WeaponBase is Pistol)
                        {
                            WeaponBase.gameObject.SetActive(true);
                        }
                        break;
                    case WeaponType.MachineGun:
                        if (WeaponBase is MachineGun)
                        {
                            WeaponBase.gameObject.SetActive(true);
                        }
                        break;
                    default:
                        break;
                }
            }

        }
    }

    

    public void GunSwitchTo(WeaponType weaponType)
    {
        foreach (WeaponBase item in Guns)
        {
            item.gameObject.SetActive(false);
            switch (weaponType)
            {
                case WeaponType.Pistol:
                    if (item is Pistol)
                    {
                        item.gameObject.SetActive(true);
                    }
                    break;
                case WeaponType.MachineGun:
                    if (item is MachineGun)
                    {
                        item.gameObject.SetActive(true);
                    }
                    break;
                default:
                    break;
            }
        }
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
          
                GunSwitchTo(WeaponType.Pistol);

        }
    }
}
