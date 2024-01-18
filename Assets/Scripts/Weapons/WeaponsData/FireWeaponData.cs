using UnityEngine;

namespace Assets.Scripts.WeaponsData
{
    [CreateAssetMenu(fileName = "FireWeaponData", menuName = "FireWeaponData")]
    public class FireWeaponData : WeaponData
    {
        [field: SerializeField] public int ShotsCount { get; }
        [field: SerializeField] public float FireRateInSecond { get; }
    }
}
