using Assets.Scripts.Infrastructure;
using MoreMountains.Feedbacks;
using System;
using System.Collections.Generic;
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
        
        private MMF_Player _onDamagePlayer;
        private MMF_FloatingText _onDamageFloatingText;

        public void Construct(MMF_Player onDamagePlayer)
        {
            _onDamagePlayer = onDamagePlayer;
            ActualHp = MaxHp;
        }

        public void Initialize()
        {
            _onDamagePlayer.GetFeedbackOfType<MMF_Scale>().AnimateScaleTarget = transform;
            _onDamageFloatingText = _onDamagePlayer.GetFeedbackOfType<MMF_FloatingText>();
            _onDamagePlayer.GetFeedbackOfType<MMF_Flicker>().BoundRenderer = GetComponent<SpriteRenderer>();
            _onDamagePlayer.Initialization();
            _onDamagePlayer.SetCanPlay(true);
        }

        public void TakeDamage(int damage)
        {
            var prevLife = ActualHp;
            ActualHp -= damage;
            var earnedMoneys = Mathf.Abs(prevLife - Mathf.Max(ActualHp,0));
            ServiceLocator.GetService<Player>().AddMoney(earnedMoneys);
            
            OnDamageTake?.Invoke(ActualHp, MaxHp);

            _onDamageFloatingText.Value = damage.ToString();
            _onDamagePlayer.PlayFeedbacks();

            CheckHP();
        }

        private void CheckHP()
        {
            if (ActualHp <= 0)
            {
                _onDamagePlayer.StopFeedbacks();
                _onDamagePlayer.SetCanPlay(false);
                _onDamagePlayer.RestoreInitialValues();

                OnDie?.Invoke();
                AllowToAttack = false;
            }
        }
    }
} 