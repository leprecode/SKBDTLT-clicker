using Assets.Scripts.Infrastructure;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Store
{
    public class WeaponModel
    {
        [SerializeField] private Dictionary<Weapon, int> _weaponPrefabs;
        [SerializeField] private Dictionary<WeaponName, bool> _boughtWeapons;
        [SerializeField] private List<List<Weapon>> _weaponsPool;

        public WeaponModel(Dictionary<Weapon, int> weaponsPrefab, Weapon startWeapon)
        {
            _weaponPrefabs = weaponsPrefab;
            ActualWeapon = startWeapon;

            _boughtWeapons = new Dictionary<WeaponName, bool>
            {
                { ActualWeapon.WeaponData.WeaponName, true }
            };


            //TODO: initialize weapons pool
        }

        [SerializeField] public Weapon ActualWeapon { get; set; }



    }

    public class WeaponPresenter
    {
        private readonly WeaponModel _weaponModel;

        public WeaponPresenter(WeaponModel weaponModel)
        {
            _weaponModel = weaponModel;
        }

        public Weapon ActualWeapon => _weaponModel.ActualWeapon;

        public void ChangeWeapon(Weapon weapon)
        { 
            _weaponModel.ActualWeapon = weapon;
        }
    }

    public class WeaponView : MonoBehaviour
    {
        [SerializeField] private Dictionary<WeaponName, StoreCell> _weaponsCell;

    }
}
