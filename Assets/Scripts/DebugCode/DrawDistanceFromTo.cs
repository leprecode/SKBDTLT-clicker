using UnityEditor;
using UnityEngine;

namespace Assets.Scripts.DebugCode
{
    internal class DrawDistanceFromTo : MonoBehaviour
    {
        [SerializeField] private GameObject[] _objectsToMeasure;
        [SerializeField] private GameObject _targetObject;

        private void OnDrawGizmos()
        {
            if (_objectsToMeasure == null || _targetObject == null)
                return;

            Gizmos.color = Color.yellow;

            foreach (var obj in _objectsToMeasure)
            {
                if (obj != null)
                {
                    float distance = Vector3.Distance(obj.transform.position, _targetObject.transform.position);

                    // Рисуем линию от текущего объекта к целевому объекту
                    Gizmos.DrawLine(obj.transform.position, _targetObject.transform.position);

                    // Отображаем дистанцию в сцене
                    Handles.Label(obj.transform.position + Vector3.up, $"Distance: {distance:F2}");

                    // Отображаем сферу в точке текущего объекта
                    Gizmos.DrawWireSphere(obj.transform.position, 0.5f);
                }
            }
        }
    }
}
