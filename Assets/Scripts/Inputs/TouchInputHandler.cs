using UnityEngine;
using Assets.Scripts.Infrastructure;
using Assets.Scripts.EnemyLogic;

namespace Assets.Scripts.Inputs
{
    public class TouchInputHandler : MonoBehaviour
    {
        private const int MAX_TOUCH_COUNT_TO_HANDLE = 4;

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
                int touchCountToHandle = Mathf.Clamp(Input.touchCount, 0, MAX_TOUCH_COUNT_TO_HANDLE);

                for (int i = 0; i < touchCountToHandle; i++)
                {
                    if (Input.GetTouch(i).phase == TouchPhase.Began)
                    {
                        Ray ray = _cam.ScreenPointToRay(Input.mousePosition);
                        var info = Physics2D.Raycast(ray.origin, ray.direction, 10);

                        if (info.collider != null)
                        {
                            if (info.collider.TryGetComponent(out Enemy enemy))
                            {
                                HandleTouch(info.point, enemy);
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