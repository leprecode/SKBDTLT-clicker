using Assets.Scripts.BankLogic;
using Assets.Scripts.EnemiesManagment;
using Assets.Scripts.EnemyLogic;
using Assets.Scripts.Weapons;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Infrastructure
{
    public class StateMachine
    {
        private Dictionary<GameState, IState> _states;
        private IState _currentState;
        private readonly Gameplay _gameplay;
        public StateMachine(Dictionary<Weapon, int> WeaponsPrefabs,
            Transform[] AttackPoints,
            BankView BankView,
            Dictionary<Enemy, int> Enemies,
            WeaponView WeaponView,
            EnemiesManagerView EnemiesManagerView,
            out WeaponPresenter weaponPresenter,
            out BankPresenter bankPresenter,
            out WeaponModel weaponModel,
            out EnemiesPool enemiesPool,
            out EnemiesManager enemiesManager,
            out Player player)
        {


            _states = new Dictionary<GameState, IState>
            {
                [GameState.Initial] = new InitialState(WeaponsPrefabs, AttackPoints, BankView, Enemies, WeaponView, EnemiesManagerView, out weaponPresenter, 
                out bankPresenter, out weaponModel, out enemiesPool, out enemiesManager, out player),
                [GameState.Gameplay] = new GameplayState(),
                [GameState.Pause] = new PauseState(),
                [GameState.AD] = new AdState()
            };

            SetState(GameState.Initial);
        }

        public void SetState(GameState state)
        {
            if (_states.TryGetValue(state, out IState newState))
            {
                if (_currentState != null)
                    _currentState.Exit();

                _currentState = newState;
                _currentState.Enter();
            }
            else
            {
                Debug.LogError($"Состояние с именем {state} не найдено");
            }
        }

        public void Update()
        {
            if (_currentState != null && _currentState is IUpdatableState updatable)
            {
                updatable.Update();
            }
        }
    }

}
