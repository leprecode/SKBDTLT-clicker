using Assets.Scripts.Infrastructure;
using System;
using UnityEngine;

namespace Assets.Scripts.Weapons
{
    public class WeaponPresenter
    {
        private readonly WeaponModel _model;
        private readonly WeaponView _view;

        public WeaponPresenter(WeaponModel weaponModel, WeaponView view)
        {
            _model = weaponModel;
            _view = view;
        }

        public Weapon ActualWeapon => _model.ActualWeapon;

        public void ChangeWeapon(Weapon weapon)
        { 
            _model.ActualWeapon = weapon;
        }

        public int AttackByActualWeapon(Vector3 position)
        {
            var name = _model.ActualWeapon.WeaponName;
            var weapon = _model.GetWeaponFromPool(name);

           


            return 0;
        }
    }
}
