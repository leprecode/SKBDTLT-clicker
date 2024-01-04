using Assets.Scripts.Infrastructure;
using MoreMountains.Feedbacks;
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
        
        private MMF_Player _onDamagePlayer;

        public void Construct(MMF_Player onDamagePlayer)
        {
            _onDamagePlayer = onDamagePlayer;
            ActualHp = MaxHp;
        }

        public void Initialize()
        {
            _onDamagePlayer.GetFeedbackOfType<MMF_Scale>().AnimateScaleTarget = transform;
            _onDamagePlayer.GetFeedbackOfType<MMF_Flicker>().BoundRenderer = GetComponent<SpriteRenderer>();
            _onDamagePlayer.enabled = true;
            _onDamagePlayer.Initialization();

         //   _onDamagePlayer.SetCanPlay(true);
        }

        public void TakeDamage(int damage)
        {
            var prevLife = ActualHp;
            
            ActualHp -= damage;

            var earnedMoneys = Mathf.Abs(prevLife - Mathf.Max(ActualHp,0));

            OnDamageTake?.Invoke(ActualHp, MaxHp);
            
            ServiceLocator.GetService<Player>().AddMoney(earnedMoneys);

            _onDamagePlayer.PlayFeedbacks();

            CheckHP();
        }

        private void CheckHP()
        {
            if (ActualHp <= 0)
            {
                _onDamagePlayer.StopFeedbacks();
                _onDamagePlayer.enabled = false;
              //  _onDamagePlayer.SetCanPlay(false); 

                OnDie?.Invoke();
                AllowToAttack = false;
            }
        }
    }
} 