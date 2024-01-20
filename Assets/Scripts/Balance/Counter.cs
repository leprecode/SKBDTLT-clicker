using Assets.Scripts.EnemyLogic;
using Assets.Scripts.WeaponsData;
using Assets.Scripts.WeaponsLogic;
using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Balance
{
    public class Counter : MonoBehaviour
    {
        [SerializeField] private List<EnemyData> _allEnemiesInTheGame;
        [SerializeField] private WeaponsCost weaponsCost;
        [SerializeField] private List<WeaponData> _allWeaponsData;

        [SerializeField] private int ClicksPerSecond;

        [SerializeField] private TotalData totalData;
        [SerializeField] private List<DataToPassEnemy> dataToPassEnemies;

        [SerializeField] private WeaponData lastCurrentWeapon;

        [Button]
        private void Count()
        {
            List<WeaponData> allBuyedWeapons = _allWeaponsData;

            lastCurrentWeapon = allBuyedWeapons[0];

            dataToPassEnemies = new List<DataToPassEnemy>();
            totalData = new TotalData();

            for (int i = 0; i < _allEnemiesInTheGame.Count; i++)
            {
                dataToPassEnemies.Add(new DataToPassEnemy(_allEnemiesInTheGame[i].Name, _allEnemiesInTheGame[i].MaxHp));

                if (i == 0)
                {
                    dataToPassEnemies[i].AddNewWeapon(lastCurrentWeapon.WeaponName);
                }

                var currentEnemyHp = _allEnemiesInTheGame[i].MaxHp;


                while (currentEnemyHp > 0)
                {
                    CollectTotalData();
                    CollectPerEnemyData(i);
                    currentEnemyHp = MakeDamageAndEarnMoney(dataToPassEnemies[i], lastCurrentWeapon, currentEnemyHp);

                    var buyedNewWeapon = TryBuyNewWeapon(ref lastCurrentWeapon);

                    if (buyedNewWeapon)
                    {
                        dataToPassEnemies[i].AddNewWeapon(lastCurrentWeapon.WeaponName);
                    }
                }
            }
        }

        private bool TryBuyNewWeapon(ref WeaponData currentWeapon)
        {
            if (currentWeapon.WeaponName == WeaponName.Flamethrower)
                return false;

            var nextIndex = FindIndexOfNextWeapon(currentWeapon);

            if (totalData.currentMoneys >= weaponsCost.Prices[_allWeaponsData[nextIndex].WeaponName])
            {
                currentWeapon = _allWeaponsData[nextIndex];
                totalData.currentMoneys -= weaponsCost.Prices[currentWeapon.WeaponName];
                return true;
            }

            return false;
        }

        private int FindIndexOfNextWeapon(WeaponData currentWeapon)
        {
            for (int i = 0; i < _allWeaponsData.Count; i++)
            {
                if (currentWeapon.WeaponName == _allWeaponsData[i].WeaponName)
                {
                    return Mathf.Clamp(i + 1, 0, _allWeaponsData.Count);
                }
            }

            return 100;
        }

        private void CollectPerEnemyData(int i)
        {
            dataToPassEnemies[i].clicksToDestroy++;
            dataToPassEnemies[i].secondsToDestroy = dataToPassEnemies[i].clicksToDestroy / ClicksPerSecond;
            dataToPassEnemies[i].minuteToDestroy = (dataToPassEnemies[i].clicksToDestroy / ClicksPerSecond) / 60f;
        }

        private void CollectTotalData()
        {
            totalData.totalClicks++;
            totalData.totalSecondsToPassTheGame = totalData.totalClicks / ClicksPerSecond;
            totalData.totalMinutesToPassTheGame = (totalData.totalClicks / ClicksPerSecond) / 60f;
        }

        private int MakeDamageAndEarnMoney(DataToPassEnemy dataToPassEnemy, WeaponData currentWeapon, int currentEnemyHp)
        {
            var damage = MakeDamageByWeaponPerOneClick(currentWeapon);

            var prevLife = currentEnemyHp;
            currentEnemyHp -= damage;

            var realDamage = Mathf.Abs(prevLife - Mathf.Max(currentEnemyHp, 0));

            totalData.totalDamage += realDamage;
            totalData.currentMoneys += realDamage;
            totalData.totalMoneysEarned += realDamage;

            dataToPassEnemy.AddDamageByWeapon(currentWeapon.WeaponName, realDamage);

            return currentEnemyHp;
        }

        private int MakeDamageByWeaponPerOneClick(WeaponData currentWeapon)
        {
            switch (currentWeapon.WeaponName)
            {
                case WeaponName.Fist:
                case WeaponName.Foot:
                case WeaponName.Chainik:
                case WeaponName.Grenade:
                    return currentWeapon.Damage;

                case WeaponName.Bita:
                    return currentWeapon.Damage * 3;

                case WeaponName.Pistol:
                case WeaponName.Auto:
                case WeaponName.Shotgun:
                    var weapon = currentWeapon as FireWeaponData;
                    return weapon.Damage * weapon.ShotsCount;

                case WeaponName.Flamethrower:
                    var weapon2 = currentWeapon as FlamethrowerData;
                    return Mathf.RoundToInt((weapon2.AttackDuration / weapon2.SecondsBetweenDamage) * weapon2.Damage);

                default:
                    return currentWeapon.Damage;
            }
        }
    }
}