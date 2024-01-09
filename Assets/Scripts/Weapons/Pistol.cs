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
        [SerializeField] MMF_Player _onPistolFire;
        private MMF_Player _onBulletHit;
        private WeaponModel _pool;

        public override void Construct(WeaponModel pool, MMF_Player onPistolFire)
        {
            _pool = pool;
            _onBulletHit = onPistolFire;
        }

        public override void ResetWeapon()
        {
            _spriteRenderer.color = RESET_COLOR;
        }

        public override void Attack(Vector3 position, Enemy enemy)
        {
            var isFlippedX = IsNeedRotate(position);
            RotateToTarget(enemy);
            transform.position = GetRandomPosition(transform.position);

            for (int i = ShotsCount; i > 0; i--) 
            {
                StartCoroutine(Shoot(SecondsToOneShot*i));
            }
        }

        private IEnumerator Shoot(float seconds)
        {
            yield return new WaitForSeconds(seconds);
            _onPistolFire.PlayFeedbacks();
        }

        private void RotateToTarget(Enemy enemy)
        {
            Vector3 direction = enemy.transform.position - transform.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
        private bool IsNeedRotate(Vector3 target)
        {
            if (transform.position.x < target.x)
            {
                transform.eulerAngles = new Vector3(0,0,0);
                transform.localScale = new Vector3(transform.localScale.x, Mathf.Abs(transform.localScale.y), transform.localScale.z);
                return false;
            }
            else
            {
                transform.eulerAngles = new Vector3(0,180,0);
                transform.localScale = new Vector3(transform.localScale.x, Mathf.Abs(transform.localScale.y) * -1, transform.localScale.z);
                return true;
            }
        }

        private Vector3 GetRandomPosition(Vector3 startPos)
        {
            return new Vector3(startPos.x + Random.Range(-0.5f, 1), startPos.y + Random.Range(-0.5f, 1), 0);
        }
        private void OnEndAttack(Enemy enemy)
        {
            //_onDamagePlayer.PlayFeedbacks();
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