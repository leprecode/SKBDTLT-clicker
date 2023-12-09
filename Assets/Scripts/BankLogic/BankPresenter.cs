using System;

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
    }
}