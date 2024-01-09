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

        public float SecondsToOneShot
        {
            get
            {
                if (_weaponData is FireWeaponData data)
                {
                    return 1f / data.FireRateInSecond;
                }
                else
                    return 0;
            }
        }
        public int ShotsCount
        {
            get
            {
                if (_weaponData is FireWeaponData data)
                {
                    return data.ShotsCount;
                }
                else
                    return 0;
            }
        }

        public abstract void Attack(Vector3 position, Enemy enemy);

        public abstract void Construct(WeaponModel pool, MMF_Player onDamagePlayer);

        public abstract void ResetWeapon();
    }
}