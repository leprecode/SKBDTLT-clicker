using Assets.Scripts.EnemyLogic;
using Assets.Scripts.Infrastructure;
using Sirenix.Serialization;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Weapons
{
    public class WeaponModel
    {
        [SerializeField] private Dictionary<WeaponName, bool> _boughtWeapons;
        [SerializeField] private List<List<Weapon>> _weaponsPool;
        [SerializeField] private Dictionary<WeaponName, Queue<GameObject>> _impacts;

        public WeaponModel(Dictionary<Weapon, int> weaponsPrefab, WeaponName startWeapon)
        {
            //_weaponPrefabs = weaponsPrefab; make pool
           // ActualWeapon = startWeapon;

            _boughtWeapons = new Dictionary<WeaponName, bool>
            {
                { ActualWeapon.WeaponData.WeaponName, true }
            };


            //TODO: initialize weapons pool
        }

        [SerializeField] public Weapon ActualWeapon { get; set; }



    }
}
