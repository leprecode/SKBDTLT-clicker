using Assets.Scripts.EnemyLogic;
using System;
using UnityEngine;

namespace Assets.Scripts.EnemiesManagment
{
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
        }

        public int GetEnemyNumber() => _pool.GetEnemyNumber();

        public int GetActualEnemyHP() => _actualEnemy != null ? _actualEnemy.ActualHp : 0;

        public void UpdateEnemyNameOnTranslate()
        {
            _view.UpdateNameOnTranslating(_actualEnemy.Name);
        }

        public void DisableActualEnemyInteractivity()
        {
            _actualEnemy.DisableColliders();
        }

        public void EnableActualEnemyInteractivity()
        {
            _actualEnemy.EnableColliders();
        }

        public void GetFirstEnemy()
        {
            _actualEnemy = _pool.GetEnemy();
            _actualEnemy.Initialize();
            Subscribe();
            DisableActualEnemyInteractivity();
            _view.ShowNewEnemyOnScene(_actualEnemy, true);
        }

        public void GetEnemyByNumber(int number, int hp)
        {
            _actualEnemy = _pool.GetEnemyByNumber(number);
            _actualEnemy.Initialize(hp);
            Subscribe();
            DisableActualEnemyInteractivity();
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