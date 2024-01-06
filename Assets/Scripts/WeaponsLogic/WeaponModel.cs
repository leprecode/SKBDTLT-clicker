using Assets.Scripts.Weapons;
using MoreMountains.Feedbacks;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.WeaponsLogic
{
    [Serializable]
    public class WeaponModel
    {
        [SerializeField] public WeaponName ActualWeapon { get; set; }

        [SerializeField] private Vector3[] _attackPoints;
        [SerializeField] private List<WeaponName> _boughtWeapons;
        [SerializeField] private List<Queue<Weapon>> _weaponsPool;
        [SerializeField] private Dictionary<WeaponName, Queue<GameObject>> _impacts;
        [SerializeField] private List<WeaponName> _allUnbuyedWeapons;


        public WeaponModel(Dictionary<Weapon, int> weaponsPrefab, WeaponName startWeapon,
            Transform[] attackPoints, Dictionary<WeaponName, MMF_Player> weaponsVFXPrefabs)
        {
            ActualWeapon = startWeapon;

            InitializeAllUnbuyedWeapon(weaponsPrefab);
            
            _boughtWeapons = new List<WeaponName>
            {
                ActualWeapon
            };
            _allUnbuyedWeapons.Remove(ActualWeapon);

            InitializeWeaponsPool(weaponsPrefab, weaponsVFXPrefabs);
            FillAttackPoints(attackPoints);
        }

        public IReadOnlyList<WeaponName> GetAllUnbyedWeapons() => _allUnbuyedWeapons.AsReadOnly();

        public void AddNewWeapon(WeaponName name)
        {
            _boughtWeapons.Add(name);
            _allUnbuyedWeapons.Remove(name);
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
        private void InitializeAllUnbuyedWeapon(Dictionary<Weapon, int> weaponsPrefab)
        {
            _allUnbuyedWeapons = new List<WeaponName>();

            foreach (var item in weaponsPrefab)
            {
                _allUnbuyedWeapons.Add(item.Key.WeaponName);

                Debug.Log("Added To Unbuyed" + item.Key.WeaponName);
            }

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

        private void InitializeWeaponsPool(Dictionary<Weapon, int> weaponsPrefab, Dictionary<WeaponName, MMF_Player> weaponsVFXPrefabs)
        {
            _weaponsPool = new List<Queue<Weapon>>();
            int queueIndex = 0;

            foreach (var weapon in weaponsPrefab)
            {
                _weaponsPool.Add(new Queue<Weapon>());

                for (int i = 0; i < weapon.Value; i++)
                {
                    var newWeapon = UnityEngine.Object.Instantiate(weapon.Key);
                    newWeapon.Construct(this, weaponsVFXPrefabs[newWeapon.WeaponName]);
                    newWeapon.gameObject.SetActive(false);
                    _weaponsPool[queueIndex].Enqueue(newWeapon);
                }

                queueIndex++;
            }
        }
    }
}
