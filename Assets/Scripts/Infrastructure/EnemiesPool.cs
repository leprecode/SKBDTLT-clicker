using Sirenix.Serialization;
using System;
using System.Collections.Generic;
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

        public Enemy GetObject()
        {
            return _pool.Dequeue();
        }

        public void SetObject(Enemy enemy)
        {
            _pool.Enqueue(enemy);
        }
    }
}