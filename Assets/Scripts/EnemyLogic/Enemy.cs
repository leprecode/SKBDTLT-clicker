using Assets.Scripts.Infrastructure;
using System;
using UnityEngine;

namespace Assets.Scripts.EnemyLogic
{
    public class Enemy : MonoBehaviour
    {
        public Action OnDie;
        public Action<int, int> OnDamageTake;
        public int MaxHp => _enemyData.MaxHp; 
        public string Name => _enemyData.Name;
        public Sprite Background => _enemyData.Background;
        public int ActualHp { get; private set; }

        public bool AllowToAttack { get; set; } = false;

        [SerializeField] private EnemyData _enemyData;
        [field: SerializeField] public SpriteRenderer SpriteRenderer { get; private set; }

        private void Awake()
        {
            ActualHp = MaxHp;
        }

        public void TakeDamage(int damage)
        {
            var prevLife = ActualHp;
            
            ActualHp -= damage;

            var earnedMoneys = Mathf.Abs(prevLife - Mathf.Max(ActualHp,0));

            OnDamageTake?.Invoke(ActualHp, MaxHp);
            ServiceLocator.GetService<Player>().AddMoney(earnedMoneys);
            CheckHP();
        }

        private void CheckHP()
        {
            if (ActualHp <= 0)
            {
                OnDie?.Invoke();
                AllowToAttack = false;
            }
        }
    }
}