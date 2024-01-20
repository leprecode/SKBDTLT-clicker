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

        private const int COUNT_TO_EXPAND = 5;
        private readonly Dictionary<Weapon, int> _weaponsPrefabs;
        private readonly Dictionary<WeaponName, MMF_Player> _weaponsVFXPrefabs;
        private readonly MMF_Player _audioSFXPlayer;
        [SerializeField] private Vector3[] _attackPoints;
        [SerializeField] private List<WeaponName> _boughtWeapons;
        [SerializeField] private List<Queue<Weapon>> _weaponsPool;
        [SerializeField] private Dictionary<WeaponName, Queue<GameObject>> _impacts;
        [SerializeField] private List<WeaponName> _allUnbuyedWeapons;


        public WeaponModel(Dictionary<Weapon, int> weaponsPrefab, WeaponName startWeapon,
            Transform[] attackPoints, Dictionary<WeaponName, MMF_Player> weaponsVFXPrefabs, MMF_Player audioSFXPlayer)
        {
            _weaponsPrefabs = weaponsPrefab;
            _weaponsVFXPrefabs = weaponsVFXPrefabs;
            _audioSFXPlayer = audioSFXPlayer;
            ActualWeapon = startWeapon;

            InitializeAllUnbuyedWeapon();

            _boughtWeapons = new List<WeaponName>
            {
                ActualWeapon
            };
            _allUnbuyedWeapons.Remove(ActualWeapon);

            InitializeWeaponsPool();
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

            if (_weaponsPool[(int)queueIndex].Count == 0)
            {
                return null;
            }

            return _weaponsPool[(int)queueIndex].Dequeue();
        }

        private void ExpandQueue(int indexOfQueue)
        {
            Weapon toCreate = null;

            foreach (var item in _weaponsPrefabs)
            {
                if (item.Key.WeaponName == ActualWeapon)
                {
                    toCreate = item.Key;
                }
            }

            if (toCreate == null)
            {
                return;
            }

            for (int i = 0; i < COUNT_TO_EXPAND; i++)
            {
                var newWeapon = UnityEngine.Object.Instantiate(toCreate);
                newWeapon.Construct(this, _weaponsVFXPrefabs[newWeapon.WeaponName], _audioSFXPlayer);
                newWeapon.gameObject.SetActive(false);
                _weaponsPool[indexOfQueue].Enqueue(newWeapon);
            }
        }

        public Vector3 GetRandomAttackPoint()
        {
            return _attackPoints[UnityEngine.Random.Range(0, _attackPoints.Length)];
        }

        private void InitializeAllUnbuyedWeapon()
        {
            _allUnbuyedWeapons = new List<WeaponName>();

            foreach (var item in _weaponsPrefabs)
            {
                _allUnbuyedWeapons.Add(item.Key.WeaponName);
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
            int? indexOfEmptyQueue = null;

            for (int i = 0; i < _weaponsPool.Count; i++)
            {
                if (_weaponsPool[i].TryPeek(out Weapon weapon))
                {
                    if (weapon.WeaponName == weaponName)
                    {
                        return i;
                    }
                }
                else
                {
                    indexOfEmptyQueue = i;
                }
            }

            return indexOfEmptyQueue;
        }

        private void InitializeWeaponsPool()
        {
            _weaponsPool = new List<Queue<Weapon>>();
            int queueIndex = 0;

            foreach (var weapon in _weaponsPrefabs)
            {
                _weaponsPool.Add(new Queue<Weapon>());

                for (int i = 0; i < weapon.Value; i++)
                {
                    var newWeapon = UnityEngine.Object.Instantiate(weapon.Key);
                    newWeapon.Construct(this, _weaponsVFXPrefabs.ContainsKey(newWeapon.WeaponName) ? _weaponsVFXPrefabs[newWeapon.WeaponName] : null, _audioSFXPlayer);
                    newWeapon.gameObject.SetActive(false);
                    _weaponsPool[queueIndex].Enqueue(newWeapon);
                }

                queueIndex++;
            }
        }
    }
}
