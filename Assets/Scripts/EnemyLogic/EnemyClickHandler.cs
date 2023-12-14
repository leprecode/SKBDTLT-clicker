using Assets.Scripts.Infrastructure;
using UnityEngine;

namespace Assets.Scripts.EnemyLogic
{
    public class EnemyClickHandler : MonoBehaviour
    {
        [SerializeField] private Enemy _enemy;
        private void OnMouseDown()
        {
            var clickPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            clickPos.z = 0;

            ServiceLocator.GetService<Player>().Attack(clickPos, _enemy);
        }

    }
}