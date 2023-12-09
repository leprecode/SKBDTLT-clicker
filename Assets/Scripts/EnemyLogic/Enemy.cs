using System;
using UnityEngine;

namespace Assets.Scripts.EnemyLogic
{
    public class Enemy : MonoBehaviour
    {
        public Action OnDie;
        public Action<int, int> OnDamageTake;

        [field: SerializeField] public int MaxHp { get; private set; }
        public int ActualHp { get; private set; }

        private void Awake()
        {
            ActualHp = MaxHp;
        }

        public void TakeDamage(int damage)
        {
            ActualHp -= damage;
            OnDamageTake?.Invoke(ActualHp, MaxHp);
            CheckHP();
        }

        private void CheckHP()
        {
            if (ActualHp <= 0)
            {
                OnDie?.Invoke();
            }
        }
    }
}