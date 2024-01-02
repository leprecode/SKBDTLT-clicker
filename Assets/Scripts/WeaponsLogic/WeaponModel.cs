﻿using Assets.Scripts.Weapons;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.WeaponsLogic
{
    [Serializable]
    public class WeaponModel
    {
        [SerializeField] private Vector3[] _attackPoints;
        [SerializeField] private List<WeaponName> _boughtWeapons;
        [SerializeField] private List<Queue<Weapon>> _weaponsPool;
        [SerializeField] private Dictionary<WeaponName, Queue<GameObject>> _impacts;
        [SerializeField] public WeaponName ActualWeapon { get; set; }

        public WeaponModel(Dictionary<Weapon, int> weaponsPrefab, WeaponName startWeapon,
            Transform[] attackPoints)
        {
            ActualWeapon = startWeapon;

            _boughtWeapons = new List<WeaponName>
            {
                ActualWeapon
            };

            InitializeWeaponsPool(weaponsPrefab);
            FillAttackPoints(attackPoints);
        }

        public void AddNewWeapon(WeaponName name)
        {
            _boughtWeapons.Add(name);
        }

        public bool IsThisWeaponIsBought(WeaponName name)
        {
            return _boughtWeapons.Contains(name);
        }

        public void ReturnPooledObject(Weapon weapon)
        {
            var queueIndex = GetIndexOfQueue(weapon.WeaponName);
            _weaponsPool[(int)queueIndex].Enqueue(weapon);
        }

        public Weapon GetActualWeaponObjectFromPool()
        {
            var queueIndex = GetIndexOfQueue(ActualWeapon);

            //TODO: Add expandable

            if (queueIndex != null)
                return _weaponsPool[(int)queueIndex].Dequeue();
            else
                return null;
        }
        public Vector3 GetRandomAttackPoint()
        {
            return _attackPoints[UnityEngine.Random.Range(0, _attackPoints.Length)];
        }
        private void FillAttackPoints(Transform[] attackPoints)
        {
            _attackPoints = new Vector3[attackPoints.Length];

            for (int i = 0; i < attackPoints.Length; i++)
            {
                _attackPoints[i] = attackPoints[i].position;
            }
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
                    var newWeapon = UnityEngine.Object.Instantiate(weapon.Key);
                    newWeapon.Construct(this);
                    newWeapon.gameObject.SetActive(false);
                    _weaponsPool[queueIndex].Enqueue(newWeapon);
                }

                queueIndex++;
            }
        }
    }
}
