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
        [SerializeField] private List<Queue<Weapon>> _weaponsPool;
        [SerializeField] private Dictionary<WeaponName, Queue<GameObject>> _impacts;

        public WeaponModel(Dictionary<Weapon, int> weaponsPrefab, WeaponName startWeapon)
        {
            //_weaponPrefabs = weaponsPrefab; make pool
            // ActualWeapon = startWeapon;

            InitializeWeaponsPool(weaponsPrefab);

           /* _boughtWeapons = new Dictionary<WeaponName, bool>
            {
                { ActualWeapon.WeaponData.WeaponName, true }
            };*/


            //TODO: initialize weapons pool
        }

        public Weapon GetWeaponFromPool(WeaponName weaponName)
        {
            var queueIndex = GetIndexOfQueue(weaponName);

            if (queueIndex != null)
                return _weaponsPool[(int)queueIndex].Dequeue();
            else
                return null;
        }

        private int? GetIndexOfQueue(WeaponName weaponName)
        {
            for (int i = 0; i < _weaponsPool.Count; i++)
            {
                if (_weaponsPool[i].Peek().WeaponName == weaponName)
                {
                    return i;
                }
            }

            return null;
        }

        private void InitializeWeaponsPool(Dictionary<Weapon, int> weaponsPrefab)
        {
            _weaponsPool = new List<Queue<Weapon>>();
            int queueIndex = 0;

            foreach (var weapon in weaponsPrefab)
            {
                _weaponsPool.Add(new Queue<Weapon>());

                for (int i = 0; i < weapon.Value; i++)
                {
                    var newWeapon = GameObject.Instantiate(weapon.Key);
                    newWeapon.gameObject.SetActive(false);
                    _weaponsPool[queueIndex].Enqueue(newWeapon);
                }

                queueIndex++;
            }
        }



        [SerializeField] public Weapon ActualWeapon { get; set; }



    }
}
