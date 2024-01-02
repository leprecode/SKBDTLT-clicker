using Assets.Scripts.EnemyLogic;
using Assets.Scripts.Infrastructure;
using System;
using UnityEngine;

namespace Assets.Scripts.EnemiesManagment
{
    [Serializable]
    public class EnemiesManager
    {
        public event Action OnEnemyEnded;
        
        private Enemy _actualEnemy;
        private readonly EnemiesPool _pool;
        private readonly EnemiesManagerView _view;

        public EnemiesManager(EnemiesPool pool, EnemiesManagerView view)
        {
            _pool = pool;
            _view = view;

            GetNewEnemy();
        }

        [field: SerializeField] public Enemy ActualEnemy { get => _actualEnemy; private set => _actualEnemy = value; }


        private void PrepareActualEnemy()
        {
            _actualEnemy.gameObject.SetActive(true);
            _view.UpdateLifeText(_actualEnemy.ActualHp, _actualEnemy.MaxHp);
        }

        private void GetNewEnemy()
        {
            Unsubscribe();
            PrepareDeadEnemy();
            _actualEnemy = _pool.GetEnemy();

            if (_actualEnemy is null)
            {
                OnEnemyEnded?.Invoke();
                _view.OnEndGame();
            }
            else
            {
                Subscribe();
                PrepareActualEnemy();
                _view.UpdateBarOnNewEnemy(_actualEnemy.ActualHp, _actualEnemy.MaxHp);
            }
        }

        private void PrepareDeadEnemy()
        {
            if (_actualEnemy != null)
                GameObject.Destroy(_actualEnemy.gameObject);
        }

        private void Subscribe()
        {
            _actualEnemy.OnDie += GetNewEnemy;
            _actualEnemy.OnDamageTake += _view.UpdateLifeText;
        }

        private void Unsubscribe()
        {
            if (_actualEnemy != null)
            {
                _actualEnemy.OnDie -= GetNewEnemy;
                _actualEnemy.OnDamageTake -= _view.UpdateLifeText;
            }
        }
    }
}