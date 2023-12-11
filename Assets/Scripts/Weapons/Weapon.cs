using Assets.Scripts.Store;
using UnityEngine;

namespace Assets.Scripts.Weapons
{
    public abstract class Weapon : MonoBehaviour
    {
        [SerializeField] private MeleeWeaponData _weaponData;
        public WeaponName WeaponName { get => _weaponData.WeaponName; }
        public int Damage { get => _weaponData.Damage; }
        public float MovementDuration { get => _weaponData.MovementDuration; }

        public abstract int Attack(Vector3 position);
    }
}