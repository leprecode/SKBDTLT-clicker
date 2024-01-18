using Assets.Scripts.EnemyLogic;
using Assets.Scripts.WeaponsLogic;
using DG.Tweening;
using MoreMountains.Feedbacks;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Weapons
{
    public class FlameThrower : Weapon
    {
        private readonly Color32 RESET_COLOR = new Color32(255, 255, 255, 255);
        [SerializeField] private SpriteRenderer _spriteRenderer;
        [SerializeField] private MMF_Player _onAttack;
        [SerializeField] private Transform _muzzleFlash;

        private Tween _rotationTween;
        private WeaponModel _pool;

        private MMF_Player _soundSystem;
        private MMF_MMSoundManagerSound _soundFeedback;

        public override void Construct(WeaponModel pool, MMF_Player onDamagePlayer, MMF_Player soundSystem)
        {
            _pool = pool;

            _soundSystem = soundSystem;
            _soundFeedback = _soundSystem.GetFeedbackOfType<MMF_MMSoundManagerSound>();
        }

        public override void ResetWeapon()
        {
            _spriteRenderer.color = RESET_COLOR;
            transform.eulerAngles = Vector3.zero;
            transform.position = Vector3.zero;
            transform.localScale = new Vector3(transform.localScale.x, Mathf.Abs(transform.localScale.y), transform.localScale.z);
            _muzzleFlash.localScale = new Vector3(_muzzleFlash.localScale.x, Mathf.Abs(_muzzleFlash.localScale.y), _muzzleFlash.localScale.z);
        }

        public override void Attack(Vector3 position, Enemy enemy)
        {
            transform.position = GetRandomPosition(transform.position);
            RotateToTarget(enemy);

            _rotationTween = 
                transform
                .DORotate(new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, transform.eulerAngles.z + RotationAnglePerSecond), 1f, RotateMode.Fast)
                .SetLoops(-1, LoopType.Yoyo)
                .SetEase(Ease.Linear);

            _onAttack.PlayFeedbacks();

            for (float i = 0; i < AttackDuration; i+= SecondsBetweenDamage)
            {
                StartCoroutine(SetDamage(i,enemy, position));
            }
            

            StartCoroutine(OnEndAttack(AttackDuration));
        }

        private IEnumerator SetDamage(float time,Enemy enemy, Vector3 pos)
        {
            yield return new WaitForSeconds(time);

            if (enemy != null)
            {
                _soundFeedback.Sfx = WeaponClip;
                _soundSystem.PlayFeedbacks();
                _onAttack.PlayFeedbacks();

                enemy?.TakeDamage(Damage, GetRandomPosition(pos));
            }
            else
            {
                StopAllCoroutines();
                StartCoroutine(OnEndAttack(0));
            }

        }

        private void RotateToTarget(Enemy enemy)
        {
            if (transform.position.x < enemy.transform.position.x)
            {
                transform.localScale = new Vector3(transform.localScale.x, Mathf.Abs(transform.localScale.y), transform.localScale.z);
                _muzzleFlash.localScale = new Vector3(_muzzleFlash.localScale.x, Mathf.Abs(_muzzleFlash.localScale.y), _muzzleFlash.localScale.z);
            }
            else
            {
                transform.localScale = new Vector3(transform.localScale.x, Mathf.Abs(transform.localScale.y) * -1, transform.localScale.z);
                _muzzleFlash.localScale = new Vector3(_muzzleFlash.localScale.x, Mathf.Abs(_muzzleFlash.localScale.y) * -1, _muzzleFlash.localScale.z);
            }


            Vector3 direction = enemy.transform.position - transform.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }


        private Vector3 GetRandomPosition(Vector3 startPos)
        {
            return new Vector3(startPos.x + Random.Range(-0.5f, 1), startPos.y + Random.Range(-0.5f, 1), 0);
        }

        private IEnumerator OnEndAttack(float timeOffset)
        {
            yield return new WaitForSeconds(timeOffset);
            _spriteRenderer.DOFade(0f, FadeDuration).OnComplete(BackToPool);
            _rotationTween?.Kill();
        }

        private void BackToPool()
        {
            ResetWeapon();
            gameObject.SetActive(false);
            _pool.ReturnPooledObject(this);
        }
    }
}