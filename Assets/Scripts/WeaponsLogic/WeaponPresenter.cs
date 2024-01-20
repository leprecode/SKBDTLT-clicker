using Assets.Scripts.EnemyLogic;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.WeaponsLogic
{
    [Serializable]
    public class WeaponPresenter
    {
        [SerializeField] private readonly WeaponModel _model;

        public WeaponPresenter(WeaponModel weaponModel)
        {
            _model = weaponModel;
        }

        public IReadOnlyList<WeaponName> GetAllUnbyedWeapons() => _model.GetAllUnbyedWeapons();

        public WeaponName GetActualWeapon => _model.ActualWeapon;

        public void AttackByActualWeapon(Vector3 position, Enemy enemy)
        {
            var weapon = _model.GetActualWeaponObjectFromPool();

            if (weapon is null)
                return;

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
