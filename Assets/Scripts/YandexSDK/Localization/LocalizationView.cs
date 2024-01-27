using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.YandexSDK.Localization
{
    public class LocalizationView : MonoBehaviour
    {
        [SerializeField] private Sprite _turkishFlag;
        [SerializeField] private Sprite _englishFlag;
        [SerializeField] private Sprite _russianFlag;
        [SerializeField] private Localization _localization;

        [SerializeField] private Image _flagImage;

        public void OnButtonFlagClick()
        { 
            if (_flagImage.sprite == _russianFlag)
            {
                TranslateToEnglish();
            }
            else if (_flagImage.sprite == _englishFlag)
            {
                TranslateToTurkish();
            }
            else if (_flagImage.sprite == _turkishFlag)
            {
                TranslateToRussian();
            }
        }

        public void InitializeView(string currentLang)
        {

            if (currentLang == "ru")
            {
                SetRussianFlag();
            }
            else if (currentLang == "en")
            {
                SetEnglishFlag();
            }
            else if (currentLang == "tr")
            {
                SetTurkishFlag();
            }
            else
            {
                SetEnglishFlag();
            }
        }

        private void SetRussianFlag()
        {
            _flagImage.sprite = _russianFlag;
        }
        private void SetEnglishFlag()
        {
            _flagImage.sprite = _englishFlag;
            Debug.Log("SetEnglishFlag");
        }
        private void SetTurkishFlag()
        {
            _flagImage.sprite = _turkishFlag;
        }

        private void TranslateToRussian()
        {
            SetRussianFlag();
            _localization.TranslateToRussian();
        }
        private void TranslateToEnglish()
        {
            SetEnglishFlag();
            _localization.TranslateToEnglish();
        }
        private void TranslateToTurkish()
        {
            SetTurkishFlag();
            _localization.TranslateToTurkish();
        }
    }
}
