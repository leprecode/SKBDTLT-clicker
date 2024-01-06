using Assets.Scripts.EnemyLogic;
using Assets.Scripts.WeaponsLogic;
using DG.Tweening;
using MoreMountains.Feedbacks;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace Assets.Scripts.Weapons
{
    public class Foot : Weapon
    {
        private readonly Color32 RESET_COLOR = new Color32(255, 255, 255, 255);

        [SerializeField] private SpriteRenderer _spriteRenderer;
        private WeaponModel _pool;
        private MMF_Player _onDamagePlayer;
        private MMF_ParticlesInstantiation _mMF_ParticlesInstantiation;

        private readonly Vector3 _rotationOnNotFlippedYAttack = new Vector3(0, 0, -60);
        private readonly Vector3 _rotationOnFlippedYAttack = new Vector3(0, 0, -120);
        private float _rotationYAmplitude = 80;
        private float _foorRotationDuration = 0.15f;

        public override void Construct(WeaponModel pool, MMF_Player onDamagePlayer)
        {
            _pool = pool;
            _onDamagePlayer = onDamagePlayer;
            _mMF_ParticlesInstantiation = _onDamagePlayer.GetFeedbackOfType<MMF_ParticlesInstantiation>();
        }

        public override void Attack(Vector3 position, Enemy enemy)
        {
           
            var isFlippedY = IsNeedFlipY(position);

            RotateToTarget(enemy, isFlippedY);
            MoveToTarget(position, enemy, isFlippedY);
        }

        private bool IsNeedFlipY(Vector3 target)
        {
            if (transform.position.x < target.x)
            {
                _spriteRenderer.flipY = false;
                return false;
            }
            else
            {
                _spriteRenderer.flipY = true;
                return true;
            }
        }

        private void MoveToTarget(Vector3 position, Enemy enemy, bool isFlippedY)
        {
            Vector3 endRotation;

            if (isFlippedY)
            {
                endRotation = new Vector3(0, 0, transform.eulerAngles.z - _rotationYAmplitude);
            }
            else
            {
                endRotation = new Vector3(0, 0, transform.eulerAngles.z + _rotationYAmplitude);
            }

            float dist = Vector3.Distance(transform.position, position);
            float time = dist / Speed;
            //TODO: optimize time
            Sequence movementSeq = DOTween.Sequence();
            movementSeq.Append(transform.DOMove(position, time));
            movementSeq.Insert(time*0.5f, transform.DORotate(endRotation, _foorRotationDuration, RotateMode.FastBeyond360));
            movementSeq.OnComplete(() => OnEndAttack(enemy));
        }

        private void RotateToTarget(Enemy enemy, bool isFlippedY)
        {
            if (isFlippedY)
            {
                transform.rotation = Quaternion.Euler(_rotationOnFlippedYAttack);
            }
            else
            {
                transform.rotation = Quaternion.Euler(_rotationOnNotFlippedYAttack);
            }
        }

        private void OnEndAttack(Enemy enemy)
        {
            enemy.TakeDamage(Damage, transform.position);
            _mMF_ParticlesInstantiation.TargetWorldPosition = transform.position;
            _onDamagePlayer.PlayFeedbacks();
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