using System.Collections.Generic;
using UnityEngine;

public abstract class AIWeapon : Weapon
{
    public abstract List<GameObject> GetObjectsInRange();
}