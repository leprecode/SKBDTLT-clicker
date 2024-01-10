using Assets.Scripts.EnemyLogic;
using Assets.Scripts.WeaponsLogic;
using DG.Tweening;
using MoreMountains.Feedbacks;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Weapons
{
    public class Pistol : Weapon
    {
        private readonly Color32 RESET_COLOR = new Color32(255, 255, 255, 255);
        [SerializeField] private SpriteRenderer _spriteRenderer;
        [SerializeField] private MMF_Player _onPistolFire;
        [SerializeField] private Transform _muzzleFlash;
        private MMF_Player _onBulletHit;
        private MMF_ParticlesInstantiation _particlesInstantiationOnBulletHit;
        private WeaponModel _pool;

        public override void Construct(WeaponModel pool, MMF_Player onBulletHit)
        {
            _pool = pool;
            _onBulletHit = onBulletHit;
            _particlesInstantiationOnBulletHit = onBulletHit.GetFeedbackOfType<MMF_ParticlesInstantiation>();
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

            float lastAttackTime = 0;

            for (int i = 0; i < ShotsCount; i++) 
            {
                var timeToNextShot = SecondsToOneShot * i;
                lastAttackTime = timeToNextShot;
                StartCoroutine(Shoot(timeToNextShot, GetRandomPosition (position), enemy));
            }

            StartCoroutine(OnEndAttack(lastAttackTime));
        }

        private IEnumerator Shoot(float seconds, Vector3 pos, Enemy enemy)
        {
            yield return new WaitForSeconds(seconds);
            _onPistolFire.PlayFeedbacks();

            _particlesInstantiationOnBulletHit.TargetWorldPosition = pos;
            _onBulletHit.PlayFeedbacks();

            enemy?.TakeDamage(Damage, pos);
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
        }

        private void BackToPool()
        {
            ResetWeapon();
            gameObject.SetActive(false);
            _pool.ReturnPooledObject(this);
        }


    }
}