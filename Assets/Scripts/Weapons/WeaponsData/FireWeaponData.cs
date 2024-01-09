using UnityEngine;

namespace Assets.Scripts.WeaponsData
{
    [CreateAssetMenu(fileName = "FireWeaponData", menuName = "FireWeaponData")]
    public class FireWeaponData : WeaponData
    {
        [field: SerializeField] public float ShotsCount { get; }
        [field: SerializeField] public float SpreadAngle { get; }
        [field: SerializeField] public float FireRateInSecond { get; }
    }
}
