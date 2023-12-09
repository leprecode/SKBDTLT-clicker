using Assets.Scripts.BankLogic;
using System;
using UnityEngine;

namespace Assets.Scripts.Infrastructure
{
    [Serializable]
    public class Player
    {
        private readonly BankPresenter _bank;
        [SerializeField] private Melee _actualWeapon;

        public Player(Melee startWeapon, BankPresenter bank)
        {
            ActualWeapon = startWeapon;
            _bank = bank;
        }

        public Melee ActualWeapon { get => _actualWeapon; private set => _actualWeapon = value; }

        public void AddMoney(int moneys)
        {
            _bank.AddMoney(moneys);
        }
    }
}