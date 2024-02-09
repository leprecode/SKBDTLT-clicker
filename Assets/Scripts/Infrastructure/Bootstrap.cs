using Assets.Scripts.BankLogic;
using Assets.Scripts.EnemiesManagment;
using Assets.Scripts.EnemyLogic;
using Assets.Scripts.Infrastructure.Save_LoadSystem;
using Assets.Scripts.Inputs;
using Assets.Scripts.SettingsMenu;
using Assets.Scripts.Store;
using Assets.Scripts.Weapons;
using Assets.Scripts.WeaponsLogic;
using Assets.Scripts.YandexSDK.Advertisment;
using Assets.Scripts.YandexSDK.Localization;
using MoreMountains.Feedbacks;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YG;

namespace Assets.Scripts.Infrastructure
{
    public class Bootstrap : SerializedMonoBehaviour
    {
        [SerializeField] private float _timeToSave;
        [SerializeField] private Transform[] _attackPoints;
        [SerializeField] private float _minDamageRandom;
        [SerializeField] private float _maxDamageRandom;
        [SerializeField] private int _startMoney;
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
        [SerializeField] private MyLocalization _localization;
        [SerializeField] private ADV _adv;
        [SerializeField] List<WeaponName> _orderedByCostWeapons;

        private WeaponModel _weaponModel;
        private WeaponPresenter _weaponPresenter;
        private InputService _inputService;
        private SaveLoadManager _saveLoadManager;
        private StorePresenter _storePresenter;

        private void Awake()
        {
            _stateMachine = new StateMachine(_startMoney,
                _minDamageRandom,
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
                _orderedByCostWeapons,
                out _storePresenter,
                out _weaponPresenter,
                out _bankPresenter,
                out _weaponModel,
                out _enemiesPool,
                out _enemiesManager,
                out _player);

            _adv.Construct(_stateMachine);
            _settings.Construct(_stateMachine);

            LaunchGameplayProgress();

            _localization.Construct(_enemiesManager);

            StartCoroutine(WaitUntilYSDKIsInitialize());

            Subscribe();

            InvokeRepeating("SaveProgress", _timeToSave, _timeToSave);
        }

        private void SaveProgress()
        {
            Debug.Log("SaveData");
            _saveLoadManager.SetMoneys(_bankPresenter.Money);
            _saveLoadManager.SetBoughtWeaponsCount(_weaponPresenter.WeaponsCount);
            _saveLoadManager.SetLastEnemyNumber(_enemiesManager.GetEnemyNumber());
            _saveLoadManager.SetLastEnemyHp(_enemiesManager.GetActualEnemyHP());
            PlayerPrefs.Save();
        }

        private void LaunchGameplayProgress()
        {
            _saveLoadManager = new SaveLoadManager();

            if (_saveLoadManager.IsFirstLaunch())
            {
                FirstLaunch();
            }
            else
            {
                var lastEnemy = _saveLoadManager.GetLastEnemyNumber();
                var lastEnemyHp = _saveLoadManager.GetLastEnemyHp();
                var moneysEarned = _saveLoadManager.GetMoneys();
                var weaponsBoughtCount = _saveLoadManager.GetBoughtWeaponsCount();
                //var rewardedTime = _saveLoadManager.GetRewardedTime();


                _enemiesManager.GetEnemyByNumber(lastEnemy, lastEnemyHp);
                _bankPresenter.SetMoneyOnLoad(moneysEarned);
                _weaponPresenter.EquipWeaponByLoad(weaponsBoughtCount);
                _storeView.SetInactiveStatesOnLoadProgress(weaponsBoughtCount);
            }
        }

        private void FirstLaunch()
        {
            _saveLoadManager.SetIsSecondLaunch();
            _enemiesManager.GetFirstEnemy();
            _storePresenter.InitialWithoutSaving();
        }

        IEnumerator WaitUntilYSDKIsInitialize()
        {
            while (!YandexGame.SDKEnabled)
            {
                Debug.Log("Waiting YSDK");

                yield return null;
            }

            Debug.Log("Waiting YSDK is ended");
            InitInput();
            _localization.SetLanguageOnStartup();
        }

        private void InitInput()
        {
            _inputService = new InputService(_player);
            _inputService.Initial();
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