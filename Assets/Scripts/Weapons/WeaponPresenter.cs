using Assets.Scripts.Infrastructure;

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
    }
}
