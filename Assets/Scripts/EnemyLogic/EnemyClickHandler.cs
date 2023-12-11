using Assets.Scripts.Infrastructure;
using UnityEngine;

namespace Assets.Scripts.EnemyLogic
{
    public class EnemyClickHandler : MonoBehaviour
    {
        private void OnMouseDown()
        {
            /*var damage = ServiceLocator.GetService<Player>().ActualWeapon.Damage;*/
          // ServiceLocator.GetService<EnemiesManager>().ActualEnemy.TakeDamage(damage);

            //ServiceLocator.GetService<Player>().AddMoney(damage);

            var clickPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            clickPos.z = 0;

            ServiceLocator.GetService<Player>().Attack(clickPos);
        }

    }
}