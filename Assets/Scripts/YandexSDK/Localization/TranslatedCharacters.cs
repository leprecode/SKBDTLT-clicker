using Assets.Scripts.EnemyLogic;
using System;
using UnityEngine;

namespace Assets.Scripts.YandexSDK.Localization
{
    [Serializable]
    public class TranslatedCharacters
    {
        [field: SerializeField] public EnemyData EnemyData { get; private set; }
        [field: SerializeField] public string Russian { get; private set; }
        [field: SerializeField] public string English { get; private set; }
        [field: SerializeField] public string Turkish { get; private set; }
    }
}
