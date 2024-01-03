using Assets.Scripts.EnemyLogic;
using Assets.Scripts.Infrastructure;
using System;
using UnityEngine;
using UnityEngine.UI;

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

        [field: SerializeField] public Enemy ActualEnemy { get => _actualEnemy; private set => _actualEnemy = value; }


        private void PrepareActualEnemy()
        {
            _actualEnemy.gameObject.SetActive(true);
            _view.UpdateLifeText(_actualEnemy.ActualHp, _actualEnemy.MaxHp);
        }

        private void GetFirstEnemy()
        {
            _actualEnemy = _pool.GetEnemy();

            Subscribe();
            PrepareActualEnemy();
            _view.UpdateBarOnNewEnemy(_actualEnemy.ActualHp, _actualEnemy.MaxHp);
            _view.UpdateBackgroundOnFirstEnemy(_actualEnemy.Background);
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
                _view.UpdateBackgroundOnNewEnemy(_actualEnemy.Background);
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