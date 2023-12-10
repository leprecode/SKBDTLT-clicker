using Assets.Scripts.Store;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Weapons
{
    public class WeaponView : MonoBehaviour
    {
        [SerializeField] private Dictionary<WeaponName, StoreCell> _weaponsCell;

    }
}
