using Sirenix.OdinInspector;
using UnityEngine;

namespace Assets.Scripts.Store
{
    public class WeaponData : SerializedScriptableObject
    {
        [field: SerializeField] public WeaponName WeaponName{ get; }
        [field: SerializeField] public int Damage { get; }
        [field: SerializeField] public int Cost { get; }
        [field: SerializeField] public AudioClip HitSound { get; }
        [field: SerializeField] public GameObject HitVFX { get; }
    }
}
