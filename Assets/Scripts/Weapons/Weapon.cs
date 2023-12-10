using Assets.Scripts.Store;
using UnityEngine;

namespace Assets.Scripts.Weapons
{
    public class Weapon : MonoBehaviour
    {
        [field: SerializeField] public WeaponData WeaponData { get; private set; }
    }
}