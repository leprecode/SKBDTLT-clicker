using Assets.Scripts.Weapons;
using Assets.Scripts.WeaponsLogic;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Balance
{
    [Serializable]
    public class DataToPassEnemy
    {
        [SerializeField] private string enemyName;
        [SerializeField] private int _hp;

        [SerializeField] public int clicksToDestroy;
        [SerializeField] public float secondsToDestroy;
        [SerializeField] public float minuteToDestroy;

        [SerializeField] private List<WeaponName> _weaponsWillOpenOnThisEnemy;
        [DictionaryDrawerSettings(KeyLabel = "WeaponName", ValueLabel = "Damage")]
        [SerializeField] private SerializableDictionary<WeaponName, int> DamageTakenByWeapon = new SerializableDictionary<WeaponName, int>();

        public DataToPassEnemy(string name, int hp)
        {
            enemyName = name;
            _hp = hp;
            _weaponsWillOpenOnThisEnemy = new List<WeaponName>();
        }

        public void AddDamageByWeapon(WeaponName weapon, int damage)
        {
            if (!DamageTakenByWeapon.ContainsKey(weapon))
            {
                DamageTakenByWeapon.Add(weapon, damage);
            }
            else
            {
                DamageTakenByWeapon[weapon] += damage;
            }
        }

        public void AddNewWeapon(WeaponName weapon)
        {
            _weaponsWillOpenOnThisEnemy.Add(weapon);
        }
    }

    [Serializable]
    public class TotalData
    {
        [SerializeField] public int totalClicks;
        [SerializeField] public int totalDamage;
        [SerializeField] public int totalMoneysEarned;
        [HideInInspector] public int currentMoneys;
        [SerializeField] public float totalSecondsToPassTheGame;
        [SerializeField] public float totalMinutesToPassTheGame;
    }


    [Serializable]
    public class SerializableDictionary<TKey, TValue> : Dictionary<TKey, TValue> { }
}
