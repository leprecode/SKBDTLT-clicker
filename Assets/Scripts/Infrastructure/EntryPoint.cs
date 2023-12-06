using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Infrastructure
{
    public class EntryPoint : MonoBehaviour
    {
        [SerializeField] private Enemy[] _enemies;
        [SerializeField] private Weapon[] _weapons;

        private void Awake()
        {
            
        }
    }

    public class Enemy : ScriptableObject
    { }

    public abstract class Weapon : MonoBehaviour
    { }

    public class Firearm : Weapon
    {
    }

    public class Melee : Weapon
    {
    }

    public class Factory<T>
    {
    
    }

    public class Pool<T>
    {
        
    }
}