using Assets.Scripts.BankLogic;
using Assets.Scripts.WeaponsLogic;

namespace Assets.Scripts.Store
{

    public class StorePresenter
    {
        private readonly WeaponsCost _weaponsCost;
        private readonly BankPresenter _bankPresenter;
        private readonly StoreView _storeView;
        private readonly StoreCellUI[] _cells;
        private readonly WeaponPresenter _weaponPresenter;

        public StorePresenter(StoreCellUI[] cells, WeaponPresenter presenter, 
            WeaponsCost weaponsCost, BankPresenter bankPresenter, StoreView storeView)
        {
            _cells = cells;
            _weaponPresenter = presenter;
            _weaponsCost = weaponsCost;
            _bankPresenter = bankPresenter;
            _storeView = storeView;
            
            Subscribe();
        }

        ~StorePresenter()
        {
            Unsubscribe();
        }

        private void CheckIsBuyed(WeaponName name)
        {
            if (!_weaponPresenter.IsThisWeaponIsBought(name))
            {
                var cost = _weaponsCost.Prices[name];

                if (_bankPresenter.TryBuy(cost))
                {
                    _weaponPresenter.EquipNewWeapon(name);
                }
                else
                {
                    _storeView.ShowPopupNotEnoughMoney();
                }
            }
            else
            {
                _weaponPresenter.EquipWeapon(name);
            }
        }

        private void Subscribe()
        {
            for (int i = 0; i < _cells.Length; i++)
            {
                _cells[i].OnClicked += CheckIsBuyed;
            }
        }

        private void Unsubscribe()
        {
            for (int i = 0; i < _cells.Length; i++)
            {
                _cells[i].OnClicked -= CheckIsBuyed;
            }
        }
    }
}
