﻿using Assets.Scripts.EnemyLogic;
using Assets.Scripts.WeaponsLogic;
using DG.Tweening;
using MoreMountains.Feedbacks;
using UnityEngine;

namespace Assets.Scripts.Weapons
{
    public class Bita : Weapon
    {
        private readonly Color32 RESET_COLOR = new(255, 255, 255, 255);

        [SerializeField] private SpriteRenderer _spriteRenderer;
        private WeaponModel _pool;
        private MMF_Player _onDamagePlayer;
        private MMF_ParticlesInstantiation _mMF_ParticlesInstantiation;

        private float _rotationZPerSecond = 360;
        private float _timeToFullCycle = 0.5f;

        private Tween rotationTween;

        private MMF_Player _soundSystem;
        private MMF_MMSoundManagerSound _soundFeedback;

        private void OnDisable()
        {
            rotationTween?.Kill();
        }

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
            var isFlippedX = IsNeedFlipX(position);

            FlyToTarget(position, enemy, isFlippedX);
        }

        private bool IsNeedFlipX(Vector3 target)
        {
            if (transform.position.x < target.x)
            {
                _spriteRenderer.flipX = true;
                return false;
            }
            else
            {
                _spriteRenderer.flipX = false;
                return true;
            }
        }

        private void FlyToTarget(Vector3 position, Enemy enemy, bool isFlippedX)
        {
            transform.rotation = Quaternion.identity;

            Vector3 endRotation;

            if (isFlippedX)
            {
                endRotation = new Vector3(0, 0, _rotationZPerSecond);
            }
            else
            {
                endRotation = new Vector3(0, 0, -_rotationZPerSecond);
            }

            rotationTween = transform.DORotate(endRotation, _timeToFullCycle, RotateMode.FastBeyond360).SetLoops(-1, LoopType.Incremental).SetEase(Ease.Linear);

            Sequence movement = DOTween.Sequence();
            movement.Append(transform.DOMove(GetRandomPosition(position), Speed));
            movement.InsertCallback(Speed * 0.3f, () => Attack(enemy));
            movement.InsertCallback(Speed * 0.6f, () => Attack(enemy));
            movement.InsertCallback(Speed * 0.9f, () => Attack(enemy));
            movement.OnComplete(() => OnEndAttack(enemy));
        }

        private Vector3 GetRandomPosition(Vector3 startPos)
        {
            return new Vector3(startPos.x + Random.Range(-1, 2), startPos.y + Random.Range(-1, 2), 0);
        }

        private void Attack(Enemy enemy)
        {
            if (enemy != null)
            {
                _soundFeedback.Sfx = WeaponClip;
                _soundSystem.PlayFeedbacks();
                enemy.TakeDamage(Damage, transform.position);
                _mMF_ParticlesInstantiation.TargetWorldPosition = transform.position;
                _onDamagePlayer.PlayFeedbacks();
            }
        }

        private void OnEndAttack(Enemy enemy)
        {
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