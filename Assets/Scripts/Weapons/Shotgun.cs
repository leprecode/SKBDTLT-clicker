using Assets.Scripts.EnemyLogic;
using Assets.Scripts.WeaponsLogic;
using DG.Tweening;
using MoreMountains.Feedbacks;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Weapons
{
    public class Shotgun : Weapon
    {
        private readonly Color32 RESET_COLOR = new Color32(255, 255, 255, 255);
        [SerializeField] private SpriteRenderer _spriteRenderer;
        [SerializeField] private MMF_Player _onFire;
        [SerializeField] private float _offsetBeforeFading;
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
        }

        public override void Attack(Vector3 position, Enemy enemy)
        {
            transform.position = GetRandomPosition(transform.position);
            RotateToTarget(enemy);

            for (int i = 0; i < ShotsCount; i++)
            {
                Shoot(GetRandomPosition(position), enemy);
            }

            StartCoroutine(OnEndAttack());
        }

        private void Shoot(Vector3 pos, Enemy enemy)
        {
            _onFire.PlayFeedbacks();

            _particlesInstantiationOnBulletHit.TargetWorldPosition = pos;
            _onBulletHit.PlayFeedbacks();

            enemy?.TakeDamage(Damage, pos);
        }

        private void RotateToTarget(Enemy enemy)
        {
            if (transform.position.x < enemy.transform.position.x)
                transform.localScale = new Vector3(transform.localScale.x, Mathf.Abs(transform.localScale.y), transform.localScale.z);
            else
                transform.localScale = new Vector3(transform.localScale.x, Mathf.Abs(transform.localScale.y) * -1, transform.localScale.z);

            Vector3 direction = enemy.transform.position - transform.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }

        private Vector3 GetRandomPosition(Vector3 startPos)
        {
            return new Vector3(startPos.x + Random.Range(-0.5f, 1), startPos.y + Random.Range(-0.5f, 1), 0);
        }

        private IEnumerator OnEndAttack()
        {
            yield return new WaitForSeconds(_offsetBeforeFading);

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