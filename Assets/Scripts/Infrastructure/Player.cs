using Assets.Scripts.BankLogic;
using Assets.Scripts.EnemyLogic;
using Assets.Scripts.WeaponsLogic;
using System;
using UnityEngine;

namespace Assets.Scripts.Infrastructure
{
    [Serializable]
    public class Player
    {
        private readonly WeaponPresenter _weaponPresenter;
        private readonly BankPresenter _bank;

        public Player(WeaponPresenter weaponPresenter, BankPresenter bank)
        {
            _weaponPresenter = weaponPresenter;
            _bank = bank;
        }

        public void AddMoney(int moneys)
        {
            _bank.AddMoney(moneys);
        }

        public void Attack(Vector3 position, Enemy enemy )
        {
            _weaponPresenter.AttackByActualWeapon(position, enemy);
        }
    }
}