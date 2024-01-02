﻿using Assets.Scripts.EnemyLogic;
using Assets.Scripts.WeaponsLogic;
using DG.Tweening;
using System;
using UnityEditor.Tilemaps;
using UnityEngine;

namespace Assets.Scripts.Weapons
{
    public class Foot : Weapon
    {
        private readonly Color32 RESET_COLOR = new Color32(255, 255, 255, 255);

        [SerializeField] private SpriteRenderer _spriteRenderer;
        private WeaponModel _pool;

        public override void Construct(WeaponModel pool)
        {
            _pool = pool;
        }

        public override void Attack(Vector3 position, Enemy enemy)
        {
            FlipY(position);

            RotateToTarget(enemy);
            MoveToTarget(position, enemy);
        }

        private void FlipY(Vector3 target)
        {
            if (transform.position.x < target.x)
                _spriteRenderer.flipY = false;
            else
                _spriteRenderer.flipY = true;
        }

        private void MoveToTarget(Vector3 position, Enemy enemy)
        {
            float dist = Vector3.Distance(transform.position, position);
            float time = dist / Speed;

            transform.DOMove(position, time).OnComplete(() => OnEndAttack(enemy)); ;
        }

        private void RotateToTarget(Enemy enemy)
        {
            Vector3 direction = enemy.transform.position - transform.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }

        private void OnEndAttack(Enemy enemy)
        {
            enemy.TakeDamage(Damage);
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