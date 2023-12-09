using System;
using UnityEngine;

namespace Assets.Scripts.Infrastructure
{
    public class Enemy : MonoBehaviour
    {
        public Action OnDie;

        [SerializeField] private int _maxHp;
        private int _actualHp;

        private void Start()
        {
            _actualHp = _maxHp;
        }

        public void TakeDamage(int damage)
        {
            _actualHp -= damage;

            CheckHP();
        }

        private void CheckHP()
        {
            if (_actualHp <= 0)
            {
                OnDie?.Invoke();
            }
        }
    }
}