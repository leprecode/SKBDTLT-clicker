using System;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Infrastructure.Save_LoadSystem
{
    [Serializable]
    public class SaveLoadManager
    {
        [SerializeField] private int _rewardedTimeLast;

        public bool IsFirstLaunch() =>
            PlayerPrefs.GetInt("IsFirstLaunch", 0) == 0;

        public bool IsGameWasFinished() =>
            PlayerPrefs.GetInt("GameIsFinished", 0) == 1;

        public void SetIsSecondLaunch() =>
             PlayerPrefs.SetInt("IsFirstLaunch", 1);

        public int GetLastEnemyNumber() =>
             PlayerPrefs.GetInt("LastEnemy", 0);

        public void SetLastEnemyNumber(int number) =>
             PlayerPrefs.SetInt("LastEnemy", number);

        public int GetLastEnemyHp() =>
            PlayerPrefs.GetInt("LastEnemyHP", 1000);

        public void SetLastEnemyHp(int hp) =>
             PlayerPrefs.SetInt("LastEnemyHP", hp);

        public int GetMoneys() =>
            PlayerPrefs.GetInt("Money", 0);

        public void SetMoneys(int money) =>
             PlayerPrefs.SetInt("Money", money);

        public int GetBoughtWeaponsCount() =>
            PlayerPrefs.GetInt("Weapon", 0);

        public void SetBoughtWeaponsCount(int weaponCount) =>
             PlayerPrefs.SetInt("Weapon", weaponCount);

        public static void SaveGameIsFifnished() => PlayerPrefs.SetInt("GameIsFinished", 1);

        public void DeleteSavesOnFinishGame()
        {
            PlayerPrefs.SetInt("IsFirstLaunch", 0);
            PlayerPrefs.SetInt("Weapon", 0);
            PlayerPrefs.SetInt("Money", 0);
            PlayerPrefs.SetInt("LastEnemyHP", 0);
            PlayerPrefs.SetInt("LastEnemy", 0);
            PlayerPrefs.SetInt("GameIsFinished", 0);
        }
    }
}