using Assets.Scripts.BankLogic;
using Assets.Scripts.Weapons;
using System;
using System.Numerics;

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

        public int Attack(Vector3 position)
        {
            //_weaponPresenter.AttackByActualWeapon();


            return 0;
        }
    }
}