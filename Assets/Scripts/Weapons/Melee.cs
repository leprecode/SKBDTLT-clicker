using System;
using UnityEngine;

namespace Assets.Scripts.Weapons
{
    [Serializable]
    public class Melee : Weapon
    {
        public int Damage { get => WeaponData.Damage; }
    }
}