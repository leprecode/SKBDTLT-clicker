using Assets.Scripts.BankLogic;
using Assets.Scripts.EnemiesManagment;
using Assets.Scripts.EnemyLogic;
using Assets.Scripts.Weapons;
using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Infrastructure
{
	public class Gameplay : SerializedMonoBehaviour
	{
		[SerializeField] private Transform[] _attackPoints;
		[SerializeField] private Dictionary<Weapon, int> _weaponsPrefabs = new Dictionary<Weapon, int>();
		[SerializeField] private Dictionary<Enemy, int> _enemies = new Dictionary<Enemy, int>();
		[SerializeField] private EnemiesPool _enemiesPool;
		[SerializeField] private BankView _bankView;
		[SerializeField] private BankPresenter _bankPresenter;
		[SerializeField] private EnemiesManagerView _enemiesManagerView;
		[SerializeField] private WeaponView _weaponView;

		[SerializeField] private StateMachine _stateMachine;

		[SerializeField] private Player _player;
		[SerializeField] private EnemiesManager _enemiesManager;

        private WeaponModel _weaponModel;
        private WeaponPresenter _weaponPresenter;

        private void Awake()
		{
			_stateMachine = new StateMachine(_weaponsPrefabs, _attackPoints, _bankView, _enemies, _weaponView, _enemiesManagerView, 
				out _weaponPresenter,
                out _bankPresenter, out _weaponModel, out _enemiesPool, out _enemiesManager, out _player);
		}
	}
}