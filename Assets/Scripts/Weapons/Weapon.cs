using Assets.Scripts.Store;
using UnityEngine;

namespace Assets.Scripts.Weapons
{
    public abstract class Weapon : MonoBehaviour
    {
        [SerializeField] private WeaponData _weaponData;
        public WeaponName WeaponName { get => _weaponData.WeaponName; }

        public abstract int Attack(Vector3 position);
    }
}