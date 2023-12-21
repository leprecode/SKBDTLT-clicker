using Assets.Scripts.BankLogic;
using Assets.Scripts.EnemiesManagment;
using Assets.Scripts.EnemyLogic;
using Assets.Scripts.Store;
using Assets.Scripts.Weapons;
using Assets.Scripts.WeaponsLogic;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Infrastructure
{
    public class InitialState : IState
    {
        public InitialState(
            Dictionary<Weapon, int> WeaponsPrefabs,
            Transform[] AttackPoints,
            BankView BankView,
            Dictionary<Enemy, int> Enemies,
            EnemiesManagerView EnemiesManagerView,
            StoreCellUI[] cellUIs,
            WeaponsCost weaponsCost,
            StoreView storeView,
            out WeaponPresenter weaponPresenter,
            out BankPresenter bankPresenter,
            out WeaponModel weaponModel,
            out EnemiesPool enemiesPool,
            out EnemiesManager enemiesManager,
            out Player player)
        {
            PrepareWeapons(WeaponsPrefabs, AttackPoints, out weaponModel, out weaponPresenter);
            PrepareBankSystem(BankView, out bankPresenter);

            StorePresenter StorePresenter = 
                new StorePresenter(cellUIs, weaponPresenter, weaponsCost,bankPresenter, storeView);

            PrepareEnemies(Enemies, EnemiesManagerView, out enemiesPool, out enemiesManager);
            PreparePlayer(weaponPresenter, bankPresenter, out player);
            RegisterService(player, enemiesManager);
        }

        public void Enter()
        {
            Debug.Log("Вход в InitialState");
        }

        public void Exit()
        {
            Debug.Log("Выход из InitialState");
        }

        private void PreparePlayer(WeaponPresenter weaponPresenter, BankPresenter bank, out Player player)
        {
            player = new Player(weaponPresenter, bank);
        }

        private void PrepareEnemies(Dictionary<Enemy, int> Enemies, EnemiesManagerView EnemiesManagerView,
            out EnemiesPool enemiesPool, out EnemiesManager enemiesManager)
        {
            enemiesPool = new EnemiesPool();
            enemiesPool.Initialize(Enemies);
            enemiesManager = new EnemiesManager(enemiesPool, EnemiesManagerView);
        }

        private void PrepareWeapons(Dictionary<Weapon, int> WeaponsPrefabs, Transform[] AttackPoints,
            out WeaponModel weaponModel, out WeaponPresenter weaponPresenter)
        {
            weaponModel = new WeaponModel(WeaponsPrefabs, WeaponName.Fist, AttackPoints);
            weaponPresenter = new WeaponPresenter(weaponModel);
        }

        private void PrepareBankSystem(BankView BankView, out BankPresenter bankPresenter)
        {
            Bank bank = new Bank(0);
            bankPresenter = new BankPresenter(bank, BankView);
        }

        private void RegisterService(Player player, EnemiesManager enemiesManager)
        {
            ServiceLocator.RegisterService(player);
            ServiceLocator.RegisterService(enemiesManager);
        }
    }
}



