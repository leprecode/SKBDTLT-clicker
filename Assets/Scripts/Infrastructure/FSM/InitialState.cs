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
            int startMoney,
            float minDamageRandom,
            float maxDamageRandom,
            Dictionary<Weapon, int> WeaponsPrefabs,
            Transform[] AttackPoints,
            BankView BankView,
            List<Enemy> Enemies,
            EnemiesManagerView EnemiesManagerView,
            StoreCellUI[] cellUIs,
            WeaponsCost weaponsCost,
            StoreView storeView,
            MMF_Player onDamagePlayer,
            MMF_Player audioSFXPlayer,
            Dictionary<WeaponName, MMF_Player> weaponsVFXPrefabs,
            List<WeaponName> orderedByCostWeapons,
            out StorePresenter storePresenter,
            out WeaponPresenter weaponPresenter,
            out BankPresenter bankPresenter,
            out WeaponModel weaponModel,
            out EnemiesPool enemiesPool,
            out EnemiesManager enemiesManager,
            out Player player)
        {
            PrepareWeapons(orderedByCostWeapons,WeaponsPrefabs, AttackPoints, weaponsVFXPrefabs, audioSFXPlayer, out weaponModel, out weaponPresenter);
            PrepareBankSystem(BankView, startMoney, out bankPresenter);

            storePresenter =
                new StorePresenter(cellUIs, weaponPresenter, weaponsCost, bankPresenter, storeView);

            PrepareEnemies(Enemies, onDamagePlayer, EnemiesManagerView, out enemiesPool, out enemiesManager);
            PreparePlayer(weaponPresenter, bankPresenter, out player);
            RegisterService(player, enemiesManager, minDamageRandom, maxDamageRandom);


        }

        public void Enter()
        {
        }

        public void Exit()
        {
        }

        private void PreparePlayer(WeaponPresenter weaponPresenter, BankPresenter bank, out Player player)
        {
            player = new Player(weaponPresenter, bank);
        }

        private void PrepareEnemies(List<Enemy> Enemies, MMF_Player onDamagePlayer, EnemiesManagerView EnemiesManagerView,
            out EnemiesPool enemiesPool, out EnemiesManager enemiesManager)
        {
            enemiesPool = new EnemiesPool();
            enemiesPool.Initialize(Enemies, onDamagePlayer);
            enemiesManager = new EnemiesManager(enemiesPool, EnemiesManagerView);
        }

        private void PrepareWeapons(List<WeaponName> orderedByCostWeapons,Dictionary<Weapon, int> WeaponsPrefabs, Transform[] AttackPoints, Dictionary<WeaponName, MMF_Player> weaponsVFXPrefabs,
            MMF_Player audioSFXPlayer, out WeaponModel weaponModel, out WeaponPresenter weaponPresenter)
        {
            weaponModel = new WeaponModel(orderedByCostWeapons,WeaponsPrefabs, WeaponName.Fist, AttackPoints, weaponsVFXPrefabs, audioSFXPlayer);
            weaponPresenter = new WeaponPresenter(weaponModel);
        }

        private void PrepareBankSystem(BankView BankView, int startMoney, out BankPresenter bankPresenter)
        {
            Bank bank = new Bank(startMoney);
            bankPresenter = new(bank, BankView);
        }

        private void RegisterService(Player player, EnemiesManager enemiesManager, float minDamageRandom,
            float maxDamageRandom)
        {
            DamageRandomizer damageRandomizer = new(minDamageRandom, maxDamageRandom);
            ServiceLocator.RegisterService(player);
            ServiceLocator.RegisterService(damageRandomizer);
            ServiceLocator.RegisterService(enemiesManager);
        }
    }
}



