using Sirenix.Serialization;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Assets.Scripts.Infrastructure
{
    [Serializable]
    public class EnemiesPool
    {
        [OdinSerialize]
        public SerializableQueue<Enemy> _pool = new SerializableQueue<Enemy>();

        public void Initialize(Dictionary<Enemy, int> enemies)
        {
            foreach (var enemy in enemies)
            {
                for (int i = 0; i < enemy.Value; i++)
                {
                    var newEnemy = MonoBehaviour.Instantiate(enemy.Key, Vector3.zero, Quaternion.identity);
                    newEnemy.gameObject.SetActive(false);
                    _pool.Enqueue(newEnemy);
                }
            }
        }

        public Enemy GetEnemy()
        {
            if (_pool.Count > 0)
            {
                return _pool.Dequeue();
            }
            else
            {
                return null;
            }
        }

        public void SetObject(Enemy enemy)
        {
            _pool.Enqueue(enemy);
        }
    }

    [Serializable]
    public class EnemiesManager
    {
        private Enemy _actualEnemy;
        private readonly EnemiesPool _pool;

        public EnemiesManager(EnemiesPool pool)
        {
            _pool = pool;

            GetNewEnemy();
        }

        [field: SerializeField] public Enemy ActualEnemy { get => _actualEnemy; private set => _actualEnemy = value; }


        private void PrepareActualEnemy()
        {
            _actualEnemy.gameObject.SetActive(true);
        }

        private void GetNewEnemy()
        {
            Unsubscribe();
            PrepareDeadEnemy();
            _actualEnemy = _pool.GetEnemy(); //TODO: handle null to End Game
            Subscribe();
            PrepareActualEnemy();
        }

        private void PrepareDeadEnemy()
        {
            if (_actualEnemy != null)
                GameObject.Destroy(_actualEnemy.gameObject);
        }

        private void Subscribe()
        {
            _actualEnemy.OnDie += GetNewEnemy;
        }

        private void Unsubscribe()
        {
            if (_actualEnemy != null)
                _actualEnemy.OnDie -= GetNewEnemy;
        }
    }

    public class EnemiesManagerView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _lifeText;

        public void UpdateLifeText(int life, int maxLife)
        {

        }
    }
}