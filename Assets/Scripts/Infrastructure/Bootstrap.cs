using Assets.Scripts.BankLogic;
using Assets.Scripts.EnemiesManagment;
using Assets.Scripts.EnemyLogic;
using Assets.Scripts.SettingsMenu;
using Assets.Scripts.Store;
using Assets.Scripts.Weapons;
using Assets.Scripts.WeaponsLogic;
using Assets.Scripts.YandexSDK;
using MoreMountains.Feedbacks;
using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Infrastructure
{
    public class Bootstrap : SerializedMonoBehaviour
    {
        [SerializeField] private Transform[] _attackPoints;
        [SerializeField] private float _minDamageRandom;
        [SerializeField] private float _maxDamageRandom;
        [SerializeField] private Dictionary<Weapon, int> _weaponsPrefabs = new Dictionary<Weapon, int>();
        [SerializeField] private Dictionary<WeaponName, MMF_Player> _weaponsVFXPrefabs = new Dictionary<WeaponName, MMF_Player>();
        [SerializeField] private List<Enemy> _enemies = new List<Enemy>();
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
        [SerializeField] private MMF_Player _soundSystem;

        [SerializeField] private Settings _settings;
        [SerializeField] private Localization _localization;


        private WeaponModel _weaponModel;
        private WeaponPresenter _weaponPresenter;

        private void Awake()
        {
            _localization.SetLanguage();

            _stateMachine = new StateMachine(_minDamageRandom,
                _maxDamageRandom,
                _weaponsPrefabs,
                _attackPoints,
                _bankView,
                _enemies,
                _enemiesManagerView,
                _cellUIs,
                _weaponsCost,
                _storeView,
                _onDamagePlayer,
                _soundSystem,
                _weaponsVFXPrefabs,
                out _weaponPresenter,
                out _bankPresenter,
                out _weaponModel,
                out _enemiesPool,
                out _enemiesManager,
                out _player);


            _settings.Construct(_stateMachine);
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
            _endGamePopup.SetActive(true);
        }
    }
}