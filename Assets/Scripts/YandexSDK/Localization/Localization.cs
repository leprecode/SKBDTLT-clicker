using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

namespace Assets.Scripts.YandexSDK.Localization
{
    public class Localization : MonoBehaviour
    {
        [DllImport("__Internal")]
        private static extern string GetLanguage();

        [SerializeField] private List<TranslatedText> _textsToTranslate;
        [SerializeField] private List<TranslatedCharacters> _charactersNamesToTranslate;

        public void SetLanguage()
        {
            try
            {
                var currentLang = GetLanguage();

                if (currentLang == "ru" || currentLang == "be" || currentLang == "kk" || currentLang == "uk" || currentLang == "uz")
                {
                    TranslateToRussian();
                    Debug.Log("Players browser set to Russian");
                }
                else if (currentLang == "en")
                {
                    TranslateToEnglish();
                    Debug.Log("Players browser set to English");
                }
                else
                {
                    TranslateToEnglish();
                    Debug.Log("Players browser set to unknowh lang. Translate the game to english");
                }
            }
            catch
            {
                Debug.LogError("Cant get the players browser language");
                TranslateToEnglish();
            }
        }

        private void TranslateToRussian()
        {
            for (int i = 0; i < _textsToTranslate.Count; i++)
            {
                _textsToTranslate[i].Text.SetText(_textsToTranslate[i].Russian);
            }

            for (int i = 0; i < _charactersNamesToTranslate.Count; i++)
            {
                _charactersNamesToTranslate[i].EnemyData.Name = _charactersNamesToTranslate[i].Russian;
            }
        }

        private void TranslateToEnglish()
        {
            for (int i = 0; i < _textsToTranslate.Count; i++)
            {
                _textsToTranslate[i].Text.SetText(_textsToTranslate[i].English);
            }

            for (int i = 0; i < _charactersNamesToTranslate.Count; i++)
            {
                _charactersNamesToTranslate[i].EnemyData.Name = _charactersNamesToTranslate[i].English;
            }
        }
    }
}
