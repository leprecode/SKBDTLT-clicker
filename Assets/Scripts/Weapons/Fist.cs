using System;
using UnityEngine;
using DG.Tweening;
using Assets.Scripts.EnemyLogic;

namespace Assets.Scripts.Weapons
{
    [Serializable]
    public class Fist : Weapon
    {
        [SerializeField] private SpriteRenderer _spriteRenderer;
        [SerializeField] private float _fadeDuration;
        private WeaponModel _pool;

        public override void Construct(WeaponModel pool)
        {
            _pool = pool;
        }

        public override void Attack(Vector3 position, Enemy enemy)
        {
            float dist = Vector3.Distance(transform.position, position);
            float time = dist / Speed;

            transform.DOMove(position, time).OnComplete(() => OnEndAttack(enemy)); ;
        }

        private void OnEndAttack(Enemy enemy)
        {
            enemy.TakeDamage(Damage);
            _spriteRenderer.DOFade(0f, _fadeDuration).OnComplete(BackToPool);
        }

        private void BackToPool()
        {
            gameObject.SetActive(false);
            _pool.ReturnPooledObject(this);
        }

        public override void ResetWeapon()
        {
            _spriteRenderer.color = Color.white;
        }
    }
}