using Assets.Scripts.EnemyLogic;
using Assets.Scripts.Infrastructure;
using System;
using UnityEngine;

namespace Assets.Scripts.WeaponsLogic
{
    public class WeaponPresenter
    {
        private readonly WeaponModel _model;

        public WeaponPresenter(WeaponModel weaponModel)
        {
            _model = weaponModel;
        }

        public void AttackByActualWeapon(Vector3 position, Enemy enemy)
        {
            var weapon = _model.GetActualWeaponObjectFromPool();
            weapon.gameObject.SetActive(true);
            weapon.transform.position = _model.GetRandomAttackPoint();
            weapon.Attack(position, enemy);
        }

        public bool IsThisWeaponIsBought(WeaponName name)
        {
            return _model.IsThisWeaponIsBought(name);
        }

        public void EquipWeapon(WeaponName name)
        {
            _model.ActualWeapon = name;
        }
        public void EquipNewWeapon(WeaponName name)
        {
            _model.ActualWeapon = name;
            _model.AddNewWeapon(name);
        }
    }
}
