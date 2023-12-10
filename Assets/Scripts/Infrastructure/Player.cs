using Assets.Scripts.BankLogic;
using Assets.Scripts.Store;
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

        public Weapon ActualWeapon { get => _weaponPresenter.ActualWeapon; }

        public void AddMoney(int moneys)
        {
            _bank.AddMoney(moneys);
        }
    }
}