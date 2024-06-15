using Assets.Scripts.Infrastructure;
using Assets.Scripts.YandexSDK.Advertisment;
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
        [field: SerializeField] public SpriteRenderer[] Renderers { get; private set; }

        private MMF_Player _onDamagePlayer;
        private MMF_FloatingText _onDamageFloatingText;
        private MMF_ParticlesInstantiation _onDamageParticles;

        private Collider2D[] _colliders;

        private void Start()
        {
            DisableColliders();
        }

        public void Construct(MMF_Player onDamagePlayer)
        {
            _onDamagePlayer = onDamagePlayer;
            ActualHp = MaxHp;

            _colliders = GetComponents<Collider2D>();
        }

        public void Initialize(int savedHP = -1)
        {
            if (savedHP != -1)
            {
                ActualHp = savedHP;
            }

            _onDamagePlayer.GetFeedbackOfType<MMF_Scale>().AnimateScaleTarget = transform;
            _onDamageFloatingText = _onDamagePlayer.GetFeedbackOfType<MMF_FloatingText>();
            _onDamageParticles = _onDamagePlayer.GetFeedbackOfType<MMF_ParticlesInstantiation>();

            var flickers = _onDamagePlayer.GetFeedbacksOfType<MMF_Flicker>();
            
            for (int i = 0; i < Renderers.Length; i++)
            {
                flickers[i].BoundRenderer = Renderers[i];
            }

            _onDamagePlayer.Initialization();
            _onDamagePlayer.SetCanPlay(true);
            DisableColliders();
        }

        public void DisableColliders()
        {
            foreach (var item in _colliders)
            {
                item.enabled = false;
            }
        }
        public void EnableColliders()
        {
            foreach (var item in _colliders)
            {
                item.enabled = true;
            }
        }


        public void TakeDamage(int damage, Vector3 hitPoint)
        {
            var actualDamage = ServiceLocator.GetService<DamageRandomizer>().GetRandomDamage(damage) * RewardData.DamageMultiplayer;

            var prevLife = ActualHp;
            ActualHp -= actualDamage;
            var earnedMoneys = Mathf.Abs(prevLife - Mathf.Max(ActualHp, 0));
            ServiceLocator.GetService<Player>().AddMoney(earnedMoneys);

            OnDamageTake?.Invoke(ActualHp, MaxHp);

            _onDamageFloatingText.Value = actualDamage.ToString();
            _onDamageParticles.TargetWorldPosition = hitPoint;
            _onDamagePlayer.PlayFeedbacks();

            CheckHP();
        }

        private void CheckHP()
        {
            if (ActualHp <= 0)
            {
                AllowToAttack = false;

                if (transform != null)
                {
                    _onDamagePlayer.StopFeedbacks();
                    _onDamagePlayer.SetCanPlay(false);
                    _onDamagePlayer.RestoreInitialValues();
                }

                OnDie?.Invoke();
            }
        }
    }
}