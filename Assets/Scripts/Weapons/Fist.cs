using System;
using UnityEngine;
using DG.Tweening;
using Assets.Scripts.EnemyLogic;
using Assets.Scripts.WeaponsLogic;
using MoreMountains.Feedbacks;

namespace Assets.Scripts.Weapons
{
    [Serializable]
    public class Fist : Weapon
    {
        private readonly Color32 RESET_COLOR = new Color32(255, 255, 255, 255);

        [SerializeField] private SpriteRenderer _spriteRenderer;
        private WeaponModel _pool;
        private MMF_Player _onDamagePlayer;
        private MMF_ParticlesInstantiation _mMF_ParticlesInstantiation;
        private MMF_Player _soundSystem;
        private MMF_MMSoundManagerSound _soundFeedback;

        public override void Construct(WeaponModel pool, MMF_Player onDamagePlayer, MMF_Player soundSystem)
        {
            _pool = pool;
            _onDamagePlayer = onDamagePlayer;
            _mMF_ParticlesInstantiation = _onDamagePlayer.GetFeedbackOfType<MMF_ParticlesInstantiation>();
            _soundSystem = soundSystem;
            _soundFeedback = _soundSystem.GetFeedbackOfType<MMF_MMSoundManagerSound>();
        }

        public override void Attack(Vector3 position, Enemy enemy)
        {
            RotateToTarget(enemy);

            MoveToTarget(position, enemy);
        }

        private void MoveToTarget(Vector3 position, Enemy enemy)
        {
            transform.DOMove(GetRandomPosition(position), Speed).OnComplete(() => OnEndAttack(enemy)); ;
        }

        private Vector3 GetRandomPosition(Vector3 startPos)
        {
            return new Vector3(startPos.x + UnityEngine.Random.Range(-1, 2), startPos.y + UnityEngine.Random.Range(-1, 2), 0);
        }

        private void RotateToTarget(Enemy enemy)
        {
            Vector3 direction = enemy.transform.position - transform.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }

        private void OnEndAttack(Enemy enemy)
        {
            if (enemy != null)
            {
                enemy.TakeDamage(Damage, transform.position);
                _mMF_ParticlesInstantiation.TargetWorldPosition = transform.position;
                _onDamagePlayer.PlayFeedbacks();
                
                _soundFeedback.Sfx = WeaponClip;
                _soundSystem.PlayFeedbacks();
            }

            _spriteRenderer.DOFade(0f, FadeDuration).OnComplete(BackToPool);
        }

        private void BackToPool()
        {
            ResetWeapon();
            gameObject.SetActive(false);
            _pool.ReturnPooledObject(this);
        }

        public override void ResetWeapon()
        {
            _spriteRenderer.color = RESET_COLOR;
        }
    }
}