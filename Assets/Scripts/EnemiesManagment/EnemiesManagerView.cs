using DG.Tweening;
using MoreMountains.Feedbacks;
using MoreMountains.Tools;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.EnemiesManagment
{
    public class EnemiesManagerView : MonoBehaviour
    {
        [SerializeField] private GameObject _enemiesBar;
        [SerializeField] private TextMeshProUGUI _lifeText;
        [SerializeField] private MMF_Player _barPlayer;
        [SerializeField] private MMProgressBar _progressBar;

        [SerializeField] private Image _backgroundImage;
        [SerializeField] private CanvasGroup _backgroundCanvasGroup;
        [SerializeField] private TextMeshProUGUI _characterNameText;
        [SerializeField] private float _bgFadeOutDuration = 1.5f;
        [SerializeField] private float _bgFadeInDuration = 1.5f;

        public void UpdateNameOnNewEnemy(string name) => _characterNameText.SetText(name);

        public void UpdateBackgroundOnNewEnemy(Sprite newBackground)
        {
            Sequence sequence = DOTween.Sequence();

            sequence.Append(_backgroundCanvasGroup.DOFade(0, _bgFadeOutDuration)).SetEase(Ease.Linear);
            sequence.InsertCallback(_bgFadeOutDuration, () => _backgroundImage.sprite = newBackground);
            sequence.Append(_backgroundCanvasGroup.DOFade(1, _bgFadeInDuration)).SetEase(Ease.Linear);
        }

        public void UpdateBackgroundOnFirstEnemy(Sprite newBackground)
        {
            _backgroundImage.sprite = newBackground;
            _backgroundCanvasGroup.alpha = 0f;
            _backgroundCanvasGroup.DOFade(1, _bgFadeInDuration).SetEase(Ease.Linear);
        }

        public void UpdateBarOnNewEnemy(int life, int maxLife)
        {
            _lifeText.SetText(life + "/" + maxLife);
            _progressBar.UpdateBar(life, 0, maxLife);
        }

        public void UpdateLifeText(int life, int maxLife)
        {
            _lifeText.SetText(life + "/" + maxLife);
            _barPlayer.PlayFeedbacks();
            _progressBar.UpdateBar(life,0,maxLife);
        }

        public void OnEndGame()
        {
            _enemiesBar.SetActive(false);
        }
    }
}