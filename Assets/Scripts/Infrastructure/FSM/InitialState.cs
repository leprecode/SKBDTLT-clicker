﻿using Assets.Scripts.BankLogic;
using Assets.Scripts.EnemiesManagment;
using Assets.Scripts.EnemyLogic;
using Assets.Scripts.Store;
using Assets.Scripts.Weapons;
using Assets.Scripts.WeaponsLogic;
using MoreMountains.Feedbacks;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Infrastructure
{
    public class InitialState : IState
    {
        public InitialState(
            float minDamageRandom,
            float maxDamageRandom,
            Dictionary<Weapon, int> WeaponsPrefabs,
            Transform[] AttackPoints,
            BankView BankView,
            Dictionary<Enemy, int> Enemies,
            EnemiesManagerView EnemiesManagerView,
            StoreCellUI[] cellUIs,
            WeaponsCost weaponsCost,
            StoreView storeView,
            MMF_Player onDamagePlayer,
            Dictionary<WeaponName, MMF_Player> weaponsVFXPrefabs,
            out WeaponPresenter weaponPresenter,
            out BankPresenter bankPresenter,
            out WeaponModel weaponModel,
            out EnemiesPool enemiesPool,
            out EnemiesManager enemiesManager,
            out Player player)
        {
            PrepareWeapons(WeaponsPrefabs, AttackPoints, weaponsVFXPrefabs, out weaponModel, out weaponPresenter);
            PrepareBankSystem(BankView, out bankPresenter);

            StorePresenter StorePresenter = 
                new StorePresenter(cellUIs, weaponPresenter, weaponsCost,bankPresenter, storeView);

            PrepareEnemies(Enemies, onDamagePlayer, EnemiesManagerView, out enemiesPool, out enemiesManager);
            PreparePlayer(weaponPresenter, bankPresenter, out player);
            RegisterService(player, enemiesManager, minDamageRandom, maxDamageRandom);
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

        private void PrepareEnemies(Dictionary<Enemy, int> Enemies, MMF_Player onDamagePlayer, EnemiesManagerView EnemiesManagerView,
            out EnemiesPool enemiesPool, out EnemiesManager enemiesManager)
        {
            enemiesPool = new EnemiesPool();
            enemiesPool.Initialize(Enemies, onDamagePlayer);
            enemiesManager = new EnemiesManager(enemiesPool, EnemiesManagerView);
        }

        private void PrepareWeapons(Dictionary<Weapon, int> WeaponsPrefabs, Transform[] AttackPoints, Dictionary<WeaponName, MMF_Player> weaponsVFXPrefabs,
            out WeaponModel weaponModel, out WeaponPresenter weaponPresenter)
        {
            weaponModel = new WeaponModel(WeaponsPrefabs, WeaponName.Fist, AttackPoints, weaponsVFXPrefabs);
            weaponPresenter = new WeaponPresenter(weaponModel);
        }

        private void PrepareBankSystem(BankView BankView, out BankPresenter bankPresenter)
        {
            Bank bank = new Bank(0);
            bankPresenter = new BankPresenter(bank, BankView);
        }

        private void RegisterService(Player player, EnemiesManager enemiesManager,float minDamageRandom,
            float maxDamageRandom)
        {
            DamageRandomizer damageRandomizer = new DamageRandomizer(minDamageRandom, maxDamageRandom);
            ServiceLocator.RegisterService(player);
            ServiceLocator.RegisterService(damageRandomizer);
            ServiceLocator.RegisterService(enemiesManager);
        }
    }
}



