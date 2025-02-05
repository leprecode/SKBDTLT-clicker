﻿using Assets.Scripts.BankLogic;
using Assets.Scripts.EnemiesManagment;
using Assets.Scripts.EnemyLogic;
using Assets.Scripts.Store;
using Assets.Scripts.Weapons;
using Assets.Scripts.WeaponsLogic;
using MoreMountains.Feedbacks;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Infrastructure
{
    [Serializable]
    public class StateMachine
    {
        private Dictionary<GameState, IState> _states;
        [SerializeField] private IState _currentState;

        public StateMachine(int startMoney, float minDamageRandom,
            float maxDamageRandom, Dictionary<Weapon, int> WeaponsPrefabs,
            Transform[] AttackPoints,
            BankView BankView,
            List<Enemy> Enemies,
            EnemiesManagerView EnemiesManagerView,
            StoreCellUI[] cellUIs,
            WeaponsCost weaponsCost,
            StoreView storeView,
            MMF_Player onDamagePlayer,
            MMF_Player audioSFXPlayer,
            Dictionary<WeaponName, MMF_Player> _weaponsVFXPrefabs,
            List<WeaponName> orderedByCostWeapons,
            out StorePresenter storePresenter,
            out WeaponPresenter weaponPresenter,
            out BankPresenter bankPresenter,
            out WeaponModel weaponModel,
            out EnemiesPool enemiesPool,
            out EnemiesManager enemiesManager,
            out Player player)
        {


            _states = new Dictionary<GameState, IState>
            {
                [GameState.Initial] =
                new InitialState(startMoney,
                minDamageRandom,
                maxDamageRandom,
                WeaponsPrefabs,
                AttackPoints,
                BankView,
                Enemies,
                EnemiesManagerView,
                cellUIs,
                weaponsCost,
                storeView,
                onDamagePlayer,
                audioSFXPlayer,
                _weaponsVFXPrefabs,
                orderedByCostWeapons,
                out storePresenter,
                out weaponPresenter,
                out bankPresenter,
                out weaponModel,
                out enemiesPool,
                out enemiesManager,
                out player),

                [GameState.Gameplay] = new GameplayState(),
                [GameState.Pause] = new PauseState(enemiesManager),
                [GameState.AD] = new AdState()
            };

            SetState(GameState.Gameplay);
        }

        public void SetState(GameState state)
        {
            if (_states.TryGetValue(state, out IState newState))
            {
                _currentState?.Exit();

                _currentState = newState;
                _currentState.Enter();
            }
        }
    }

}
