using Assets.Scripts.WeaponsLogic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.WeaponsData
{
    public class WeaponData : SerializedScriptableObject
    {
        [field: SerializeField] public WeaponName WeaponName { get; }
        [field: SerializeField] public AudioClip WeaponClip { get; }
        [field: SerializeField] public float FadeDuration { get; }
        [field: SerializeField] public int Damage { get; }
        [field: SerializeField] public float Speed { get; }
    }
}
