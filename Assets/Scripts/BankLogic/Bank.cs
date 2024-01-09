using System;
using UnityEngine;

namespace Assets.Scripts.BankLogic
{
    [Serializable]
    public class Bank
    {
        [field: SerializeField] public int money { get; set; }

        public Bank(int Money)
        {
            money = Money;
        }
    }
}