using System;
using TMPro;
using UnityEngine;

namespace Assets.Scripts.YandexSDK.Localization
{
    [Serializable]
    public class TranslatedText
    {
        [field: SerializeField] public string Name { get; private set; }
        [field: SerializeField] public TextMeshProUGUI Text { get; private set; }
        [field: SerializeField] public string Russian { get; private set; }
        [field: SerializeField] public string English { get; private set; } 
    }
}
