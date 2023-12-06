using Sirenix.OdinInspector;
using Sirenix.Serialization;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Infrastructure
{
    public class EntryPoint : SerializedMonoBehaviour
    {
        [SerializeField] private Dictionary<Enemy, int> _enemies = new Dictionary<Enemy, int>(); 
        [SerializeField] private Weapon[] _weapons;
        [SerializeField] private EnemiesPool _enemiesPool;
        private void Awake()
        {
            foreach (var enemy in _enemies)
            {
                print(1);
            }

            _enemiesPool = new EnemiesPool();
            _enemiesPool.Initialize(_enemies);
        }
    }

    public class EnemyClickHandler : MonoBehaviour
    {

    }

    public abstract class Weapon : MonoBehaviour
    {

    }

    public class Firearm : Weapon
    {

    }

    public class Melee : Weapon
    {

    }
}