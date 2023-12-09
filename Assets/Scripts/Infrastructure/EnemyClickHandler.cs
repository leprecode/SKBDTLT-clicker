using System;
using UnityEngine;

namespace Assets.Scripts.Infrastructure
{
    public class EnemyClickHandler : MonoBehaviour
    {
        private void OnMouseDown()
        {
            var damage = ServiceLocator.GetService<Player>().ActualWeapon.Damage;
            ServiceLocator.GetService<EnemiesManager>().ActualEnemy.TakeDamage(damage);

            ServiceLocator.GetService<Player>().AddMoney(damage);
        }
    }
}