using Assets.Scripts.BankLogic;
using Assets.Scripts.WeaponsLogic;
using System;
using UnityEngine;

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

            _storeView.LastAvctiveCell = _cells[0];

            Subscribe();
            _storeView.Construct(cells);
            _storeView.SetCellsPrices(_weaponsCost);

            ActivateStartWeapon();
        }

        private void ActivateStartWeapon()
        {
            var actualWeapon = _weaponPresenter.GetActualWeapon;

            for (int i = 0; i < _cells.Length; i++)
            {
                if (_cells[i].Name == actualWeapon)
                {
                    _cells[i].SetActiveState();
                    break;
                }
            }
        }

        ~StorePresenter()
        {
            Unsubscribe();
        }

        private void OnMoneyEarning(int totalMoney)
        {
            Debug.Log("OnMoneyEarning");

            var allUnbuyedWeapons = _weaponPresenter.GetAllUnbyedWeapons();

            for (int i = 0; i < allUnbuyedWeapons.Count; i++)
            {
                if (totalMoney >= _weaponsCost.Prices[allUnbuyedWeapons[i]])
                {
                    for (int j = 0; j < _cells.Length; j++)
                    {
                        if (_cells[j].Name == allUnbuyedWeapons[i])
                        {
                            _cells[j].SetAllowToBuy();
                            Debug.Log("SetAllowToBuy");
                            return;
                        }
                    }
                }
            }
        }


        private void OnCellClick(StoreCellUI cell,WeaponName name)
        {
            if (_storeView.LastAvctiveCell.Name == name)
                return;

            if (!_weaponPresenter.IsThisWeaponIsBought(name))
            {
                var cost = _weaponsCost.Prices[name];

                if (_bankPresenter.TryBuy(cost))
                {
                    _weaponPresenter.EquipNewWeapon(name);
                    _storeView.UpdateActualCellUI(cell);
                }
                else
                {
                    _storeView.ShowPopupNotEnoughMoney();
                }
            }
            else
            {
                _storeView.UpdateActualCellUI(cell);
                _weaponPresenter.EquipWeapon(name);
            }
        }

        private void Subscribe()
        {
            for (int i = 0; i < _cells.Length; i++)
            {
                _cells[i].OnClicked += OnCellClick;
            }

            _bankPresenter.OnMoneyEarned += OnMoneyEarning;
        }

        private void Unsubscribe()
        {
            for (int i = 0; i < _cells.Length; i++)
            {
                _cells[i].OnClicked -= OnCellClick;
            }

            _bankPresenter.OnMoneyEarned -= OnMoneyEarning;
        }
    }
}
