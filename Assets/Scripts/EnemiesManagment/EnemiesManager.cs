﻿using Assets.Scripts.EnemyLogic;
using System;
using UnityEngine;

namespace Assets.Scripts.EnemiesManagment
{
    [Serializable]
    public class EnemiesManager
    {
        public event Action OnEnemyEnded;
        private readonly EnemiesPool _pool;
        private readonly EnemiesManagerView _view;
        private Enemy _actualEnemy;

        public EnemiesManager(EnemiesPool pool, EnemiesManagerView view)
        {
            _pool = pool;
            _view = view;

            GetFirstEnemy();
        }

        public void DisableActualEnemyInteractivity()
        {
            _actualEnemy.DisableColliders();
        }

        public void EnableActualEnemyInteractivity()
        {
            _actualEnemy.EnableColliders();
        }

        private void GetFirstEnemy()
        {
            _actualEnemy = _pool.GetEnemy();
            _actualEnemy.Initialize();
            Subscribe();
            _view.ShowNewEnemyOnScene(_actualEnemy, true);
        }

        private void OnEnemyDie()
        {
            Unsubscribe();
            PrepareDeadEnemy();
            _view.OnEnemyDie();

            _actualEnemy = _pool.GetEnemy();

            if (_actualEnemy == null)
            {
                OnEnemyEnded?.Invoke();
                _view.OnEndGame();
            }
            else
            {
                _actualEnemy.Initialize();
                Subscribe();
                _view.ShowNewEnemyOnScene(_actualEnemy);
            }
        }

        private void PrepareDeadEnemy()
        {
            if (_actualEnemy != null)
                GameObject.Destroy(_actualEnemy.gameObject);
        }

        private void Subscribe()
        {
            _actualEnemy.OnDie += OnEnemyDie;
            _actualEnemy.OnDamageTake += _view.UpdateLifeText;
        }

        private void Unsubscribe()
        {
            if (_actualEnemy != null)
            {
                _actualEnemy.OnDie -= OnEnemyDie;
                _actualEnemy.OnDamageTake -= _view.UpdateLifeText;
            }
        }
    }
}