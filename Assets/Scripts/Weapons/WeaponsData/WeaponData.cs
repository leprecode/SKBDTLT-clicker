using Assets.Scripts.WeaponsLogic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Assets.Scripts.WeaponsData
{
    public class WeaponData : SerializedScriptableObject
    {
        [field: SerializeField] public WeaponName WeaponName { get; }
        [field: SerializeField] public float FadeDuration { get; }
        [field: SerializeField] public int Damage { get; }
        [field: SerializeField] public float Speed { get; }

    }
}
