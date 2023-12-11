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

        public void AttackByActualWeapon(Vector3 position)
        {
            var weapon = _model.GetActualWeaponObjectFromPool();
            weapon.gameObject.SetActive(true);
            weapon.transform.position = _model.GetRandomAttackPoint();
            weapon.Attack(position);
        }
    }
}
