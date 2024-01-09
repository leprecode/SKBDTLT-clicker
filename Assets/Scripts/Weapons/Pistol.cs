using Assets.Scripts.EnemyLogic;
using Assets.Scripts.WeaponsLogic;
using DG.Tweening;
using MoreMountains.Feedbacks;
using UnityEngine;

namespace Assets.Scripts.Weapons
{
    public class Pistol : Weapon
    {
        private readonly Color32 RESET_COLOR = new Color32(255, 255, 255, 255);
        [SerializeField] private SpriteRenderer _spriteRenderer;
        private WeaponModel _pool;
        private MMF_Player _onDamagePlayer;

        public override void Construct(WeaponModel pool, MMF_Player onDamagePlayer)
        {
            _pool = pool;
            _onDamagePlayer = onDamagePlayer;
        }

        public override void Attack(Vector3 position, Enemy enemy)
        {
            var isFlippedX = IsNeedRotateY(position);
            RotateToTarget(enemy);
            transform.position = GetRandomPosition(position);
        }
        private void RotateToTarget(Enemy enemy)
        {
            Vector3 direction = enemy.transform.position - transform.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
        private bool IsNeedRotateY(Vector3 target)
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

        private Vector3 GetRandomPosition(Vector3 startPos)
        {
            return new Vector3(startPos.x + Random.Range(-1, 2), startPos.y + Random.Range(-1, 2), 0);
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

        public override void ResetWeapon()
        {
            _spriteRenderer.color = RESET_COLOR;
        }
    }
}