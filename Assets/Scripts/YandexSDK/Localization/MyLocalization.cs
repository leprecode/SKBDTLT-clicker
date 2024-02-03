using Assets.Scripts.EnemiesManagment;
using System.Collections.Generic;
using UnityEngine;
using YG;

namespace Assets.Scripts.YandexSDK.Localization
{
    public class MyLocalization : MonoBehaviour
    {
        [SerializeField] private List<TranslatedText> _textsToTranslate;
        [SerializeField] private List<TranslatedCharacters> _charactersNamesToTranslate;
        [SerializeField] private EnemiesManager _enemiesManager;
        [SerializeField] private LocalizationView _view;

        public void Construct(EnemiesManager enemiesManager)
        {
            _enemiesManager = enemiesManager;
        }
        public void SetLanguageOnStartup()
        {
            var currentLang = YandexGame.EnvironmentData.language;


            if (currentLang == "ru" || currentLang == "be" || currentLang == "kk" || currentLang == "uk" || currentLang == "uz")
            {
                TranslateToRussian();
                _view.InitializeView("ru");
                Debug.Log("Players browser set to Russian");
            }
            else if (currentLang == "en")
            {
                TranslateToEnglish();
                _view.InitializeView("en");
                Debug.Log("Players browser set to English");
            }
            else if (currentLang == "tr")
            {
                TranslateToEnglish();
                _view.InitializeView("tr");
                Debug.Log("Players browser set to Turkish");
            }
            else
            {
                TranslateToEnglish();
                _view.InitializeView("en");
                Debug.Log("Players browser set to unknowh lang. Translate the game to english");
            }

        }

        public void TranslateToRussian()
        {
            for (int i = 0; i < _textsToTranslate.Count; i++)
            {
                _textsToTranslate[i].Text.SetText(_textsToTranslate[i].Russian);
            }

            for (int i = 0; i < _charactersNamesToTranslate.Count; i++)
            {
                _charactersNamesToTranslate[i].EnemyData.Name = _charactersNamesToTranslate[i].Russian;
            }

            _enemiesManager.UpdateEnemyNameOnTranslate();
        }

        public void TranslateToEnglish()
        {
            for (int i = 0; i < _textsToTranslate.Count; i++)
            {
                _textsToTranslate[i].Text.SetText(_textsToTranslate[i].English);
            }

            for (int i = 0; i < _charactersNamesToTranslate.Count; i++)
            {
                _charactersNamesToTranslate[i].EnemyData.Name = _charactersNamesToTranslate[i].English;
            }

            _enemiesManager.UpdateEnemyNameOnTranslate();
        }

        public void TranslateToTurkish()
        {
            for (int i = 0; i < _textsToTranslate.Count; i++)
            {
                _textsToTranslate[i].Text.SetText(_textsToTranslate[i].Turkish);
            }

            for (int i = 0; i < _charactersNamesToTranslate.Count; i++)
            {
                _charactersNamesToTranslate[i].EnemyData.Name = _charactersNamesToTranslate[i].Turkish;
            }

            _enemiesManager.UpdateEnemyNameOnTranslate();
        }
    }
}
