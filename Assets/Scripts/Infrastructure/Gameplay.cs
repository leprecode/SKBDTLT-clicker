using Assets.Scripts.BankLogic;
using Assets.Scripts.EnemiesManagment;
using Assets.Scripts.EnemyLogic;
using Assets.Scripts.Store;
using Assets.Scripts.Weapons;
using Assets.Scripts.WeaponsLogic;
using MoreMountains.Feedbacks;
using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Infrastructure
{
    public class Gameplay : SerializedMonoBehaviour
	{
		[SerializeField] private Transform[] _attackPoints;
		[SerializeField] private Dictionary<Weapon, int> _weaponsPrefabs = new Dictionary<Weapon, int>();
		[SerializeField] private Dictionary<WeaponName, GameObject> _weaponsVFXPrefabs = new Dictionary<WeaponName, GameObject>();
		[SerializeField] private Dictionary<Enemy, int> _enemies = new Dictionary<Enemy, int>();
		[SerializeField] private EnemiesPool _enemiesPool;
		[SerializeField] private BankView _bankView;
		[SerializeField] private BankPresenter _bankPresenter;
		[SerializeField] private EnemiesManagerView _enemiesManagerView;

		[SerializeField] private StateMachine _stateMachine;

		[SerializeField] private Player _player;
		[SerializeField] private EnemiesManager _enemiesManager;

		[SerializeField] private StoreCellUI[] _cellUIs;
		[SerializeField] private WeaponsCost _weaponsCost;
		[SerializeField] private StoreView _storeView;
		[SerializeField] private GameObject _endGamePopup;

		[SerializeField] private MMF_Player _onDamagePlayer;

        private WeaponModel _weaponModel;
        private WeaponPresenter _weaponPresenter;




        private void Awake()
		{
			_stateMachine = new StateMachine(_weaponsPrefabs, 
				_attackPoints, 
				_bankView, 
				_enemies, 
				_enemiesManagerView, 
				_cellUIs,
				_weaponsCost,
				_storeView,
                _onDamagePlayer,
                out _weaponPresenter,
                out _bankPresenter, 
				out _weaponModel, 
				out _enemiesPool, 
				out _enemiesManager, 
				out _player);


			Subscribe();
		}

		private void Subscribe()
		{
			_enemiesManager.OnEnemyEnded += EndGame;
		}

        private void Unsubscribe()
        {
			_enemiesManager.OnEnemyEnded -= EndGame;
        }

        private void OnDestroy()
        {
			Unsubscribe();
        }

        private void EndGame()
		{
			Debug.Log("Game is Ended!");
			_endGamePopup.SetActive(true);
        }
    }
}