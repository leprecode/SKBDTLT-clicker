using UnityEngine;
using Assets.Scripts.Infrastructure;
using Assets.Scripts.EnemyLogic;

namespace Assets.Scripts.Inputs
{
    public class TouchInputHandler : MonoBehaviour
    {
        private Player _player;
        private Camera _cam;

        public void Construct(Player player)
        {
            _player = player;
            _cam = Camera.main;
        }

        void Update()
        {
            if (Input.touchCount > 0)
            {
                for (int i = 0; i < Input.touchCount; i++)
                {
                    Touch touch = Input.GetTouch(i);

                    if (touch.phase == TouchPhase.Began)
                    {
                        Ray ray = _cam.ScreenPointToRay(touch.position);

                        if (Physics.Raycast(ray, out RaycastHit hit))
                        {
                            if (hit.collider.TryGetComponent(out Enemy enemy))
                            {
                                HandleTouch(hit.point, enemy);
                            }

                        }
                    }
                }
            }
        }

        void HandleTouch(Vector3 pos, Enemy enemy)
        {
            _player.Attack(pos, enemy);
        }
    }
}