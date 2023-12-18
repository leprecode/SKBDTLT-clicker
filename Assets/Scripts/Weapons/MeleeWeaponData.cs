using Sirenix.OdinInspector;
using UnityEngine;

namespace Assets.Scripts.Weapons
{
    [CreateAssetMenu (fileName ="WeaponData", menuName ="WeaponData")]
    public class MeleeWeaponData : SerializedScriptableObject
    {
        [field: SerializeField] public WeaponName WeaponName{ get; }
        [field: SerializeField] public int Damage { get; }
        [field: SerializeField] public int Cost { get; }
        [field: SerializeField] public AudioClip HitSound { get; }
        [field: SerializeField] public GameObject HitVFX { get; }
        [field: SerializeField] public float Speed { get; }
        [field: SerializeField] public float FadeDuration { get; }
    }
}
