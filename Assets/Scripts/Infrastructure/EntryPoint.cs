using Assets.Scripts.BankLogic;
using Assets.Scripts.EnemiesManagment;
using Assets.Scripts.EnemyLogic;
using Assets.Scripts.Weapons;
using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Infrastructure
{
	public class EntryPoint : SerializedMonoBehaviour
	{
		[SerializeField] private Dictionary<Weapon, int> _weaponsPrefabs = new Dictionary<Weapon, int>();
		[SerializeField] private Dictionary<Enemy, int> _enemies = new Dictionary<Enemy, int>();
		[SerializeField] private Weapon[] _weapons;
		[SerializeField] private EnemiesPool _enemiesPool;
		[SerializeField] private Player _player;
		[SerializeField] private EnemiesManager _enemiesManager;
		[SerializeField] private BankView _bankView;
		[SerializeField] private BankPresenter _bankPresenter;
		[SerializeField] private EnemiesManagerView _enemiesManagerView;
		[SerializeField] private WeaponView _weaponView;

		private void Awake()
		{
			PrepareEnemies();

			//Melee startWeapon = PrepareStartWeapon();
			PrepareBankSystem();

			WeaponModel weaponModel = new WeaponModel(_weaponsPrefabs,WeaponName.Fist);
			WeaponPresenter weaponPresenter = new WeaponPresenter(weaponModel, _weaponView);


	 /*       PreparePlayer(startWeapon);*/

			RegisterService();
		}

		private void PrepareBankSystem()
		{
			Bank bank = new Bank(0);
			_bankPresenter = new BankPresenter(bank, _bankView);
		}

		private void RegisterService()
		{
			ServiceLocator.RegisterService(_player);
			ServiceLocator.RegisterService(_enemiesManager);
		}

/*        private void PreparePlayer(Melee startWeapon)
		{
		   _player = new Player(startWeapon, _bankPresenter);
		}*/

		private void PrepareEnemies()
		{
			_enemiesPool = new EnemiesPool();
			_enemiesPool.Initialize(_enemies);
			_enemiesManager = new EnemiesManager(_enemiesPool, _enemiesManagerView);
		}
	}
}