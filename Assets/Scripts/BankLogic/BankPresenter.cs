using System;
using System.Net.Http.Headers;

namespace Assets.Scripts.BankLogic
{
    [Serializable]
    public class BankPresenter
    {
        private readonly Bank _bank;
        private readonly BankView _view;


        public BankPresenter(Bank bank, BankView view)
        {
            _bank = bank;
            _view = view;
        }

        public void AddMoney(int money)
        {
            _bank.money += money;
            _view.UpdateUIOnAddingMoney(_bank.money);
        }

        public bool TryBuy(int cost)
        {
            if (cost <= _bank.money)
            {
                _bank.money -= cost;
                _view.UpdateUIOnSpendMoney(_bank.money);
                return true;
            }

            return false;
        }
    }
}