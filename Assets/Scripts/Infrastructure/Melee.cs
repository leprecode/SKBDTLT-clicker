using System;
using UnityEngine;

namespace Assets.Scripts.Infrastructure
{
    [Serializable]
    public class Melee : Weapon
    {

        [SerializeField] private int _damage;

        public int Damage { get => _damage; private set => _damage = value; }

        public void Construct(int damage)
        {
            Damage = damage;
        }
    }
}