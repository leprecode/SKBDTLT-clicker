using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.WeaponsLogic
{
    [CreateAssetMenu(fileName = "WeaponsCost")]
    public class WeaponsCost : SerializedScriptableObject
    {
        [field: SerializeField] public Dictionary<WeaponName, int> Prices { get; private set; }

    }
}
