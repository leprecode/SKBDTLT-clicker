using Assets.Scripts.EnemyLogic;
using Assets.Scripts.Infrastructure;
using MoreMountains.Feedbacks;
using Sirenix.Serialization;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.EnemiesManagment
{
    [Serializable]
    public class EnemiesPool
    {
        private Queue<Enemy> _pool;
        private int _allEnemiesAtStart;
        public void Initialize(List<Enemy> enemies, MMF_Player onDamagePlayer)
        {
            _allEnemiesAtStart = enemies.Count;

            _pool = new Queue<Enemy>();

            foreach (var enemy in enemies)
            {
                var newEnemy = MonoBehaviour.Instantiate(enemy, Vector3.zero, Quaternion.identity);
                newEnemy.Construct(onDamagePlayer);
                newEnemy.gameObject.SetActive(false);
                _pool.Enqueue(newEnemy);
            }
        }

        public int GetEnemyNumber()
        {
            return _allEnemiesAtStart - _pool.Count - 1;
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

        public Enemy GetEnemyByNumber(int number)
        {
            for (int i = 0; i < number; i++)
            {
                _pool.Dequeue();
            }

            return _pool.Dequeue();
        }
    }
}