﻿using Assets.Scripts.Infrastructure;
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

        [SerializeField] private EnemyData _enemyData;

        private void Awake()
        {
            ActualHp = MaxHp;
        }

        public void TakeDamage(int damage)
        {
            ActualHp -= damage;
            OnDamageTake?.Invoke(ActualHp, MaxHp);
            ServiceLocator.GetService<Player>().AddMoney(damage);
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