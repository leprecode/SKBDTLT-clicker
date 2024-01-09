using Assets.Scripts.EnemyLogic;
using Assets.Scripts.WeaponsData;
using Assets.Scripts.WeaponsLogic;
using MoreMountains.Feedbacks;
using UnityEngine;

namespace Assets.Scripts.Weapons
{
    public abstract class Weapon : MonoBehaviour
    {
        [SerializeField] private WeaponData _weaponData;
        
        public WeaponName WeaponName { get => _weaponData.WeaponName; }
        public int Damage { get => _weaponData.Damage; }
        public float Speed { get => _weaponData.Speed; }
        public float FadeDuration { get => _weaponData.FadeDuration; }

        public abstract void Attack(Vector3 position, Enemy enemy);

        public abstract void Construct(WeaponModel pool, MMF_Player onDamagePlayer);

        public abstract void ResetWeapon();
    }
}