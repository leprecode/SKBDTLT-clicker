using UnityEngine;

namespace Assets.Scripts.WeaponsData
{

    [CreateAssetMenu(fileName = "FlamethrowerData", menuName = "FlamethrowerData")]
    public class FlamethrowerData : WeaponData
    { 
        [field: SerializeField] public float AttackDuration { get; }
        [field: SerializeField] public float SecondsBetweenDamage { get; }
        [field: SerializeField] public int RotationAnglePerSecond { get; }
    }
}
