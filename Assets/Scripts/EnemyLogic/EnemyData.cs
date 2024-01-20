using Sirenix.OdinInspector;
using UnityEngine;

namespace Assets.Scripts.EnemyLogic
{
    [CreateAssetMenu(fileName = "E_Data", menuName = "EnemyData")]
    public class EnemyData : SerializedScriptableObject
    {
        [field: SerializeField] public int MaxHp { get; private set; }
        [field: SerializeField] public string Name { get; private set; }
        [field: SerializeField] public Sprite Background { get; private set; }

    }
}
