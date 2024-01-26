using System.Collections;
using System.Runtime.InteropServices;
using UnityEngine;

namespace Assets.Scripts.CameraLogic
{
    public class CameraFitter : MonoBehaviour
    {
        private const float HALF_SIZE = 0.5f;
        [SerializeField] Camera _camera;
        [SerializeField] SpriteRenderer _background;

        private void Start()
        {
            Resize();
        }

        public void Resize()
        {
            Vector2 bgSize = _background.bounds.size;
            _camera.orthographicSize = CalculateCameraSize(bgSize);
            Debug.Log("Resize");
        }


        private float CalculateCameraSize(Vector2 bounds)
        {
            float vertical = bounds.y;
            float horizontal = bounds.x * _camera.pixelHeight / _camera.pixelWidth;

            float size = Mathf.Max(horizontal, vertical) * HALF_SIZE;
            return size;
        }
    }
}