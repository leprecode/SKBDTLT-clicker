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
		[SerializeField] private Transform[] _attackPoints;
		[SerializeField] private Dictionary<Weapon, int> _weaponsPrefabs = new Dictionary<Weapon, int>();
		[SerializeField] private Dictionary<Enemy, int> _enemies = new Dictionary<Enemy, int>();
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

			PrepareBankSystem();

			WeaponModel weaponModel = new WeaponModel(_weaponsPrefabs,WeaponName.Fist, _attackPoints);
			WeaponPresenter weaponPresenter = new WeaponPresenter(weaponModel, _weaponView);


			PreparePlayer(weaponPresenter, _bankPresenter);
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

		private void PreparePlayer(WeaponPresenter weaponPresenter, BankPresenter bank)
		{
			_player = new Player(weaponPresenter, bank);
		}

		private void PrepareEnemies()
		{
			_enemiesPool = new EnemiesPool();
			_enemiesPool.Initialize(_enemies);
			_enemiesManager = new EnemiesManager(_enemiesPool, _enemiesManagerView);
		}
	}
}