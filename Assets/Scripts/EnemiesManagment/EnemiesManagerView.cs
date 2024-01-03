using Assets.Scripts.EnemyLogic;
using DG.Tweening;
using MoreMountains.Feedbacks;
using MoreMountains.Tools;
using Sirenix.OdinInspector;
using System.Diagnostics.CodeAnalysis;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.EnemiesManagment
{
    public class EnemiesManagerView : MonoBehaviour
    {
        [FoldoutGroup("TweensSettings")]
        [FoldoutGroup("TweensSettings/Bacground")]
        [SerializeField] private float _bgFadeOutDuration;

        [FoldoutGroup("TweensSettings/Bacground")]
        [SerializeField] private float _bgFadeInDuration;

        [FoldoutGroup("TweensSettings/EnemyShowing")]
        [SerializeField] private float  _blackColorFadeOutDuration;
         
        [FoldoutGroup("TweensSettings/EnemyShowing/Movement")]
        [SerializeField] private float _movementYAmplitude;
        
        [FoldoutGroup("TweensSettings/EnemyShowing/Movement")]
        [SerializeField] private float  _movementYDuration;

        [FoldoutGroup("TweensSettings/EnemyShowing/Movement")]
        [SerializeField] private float  _movementXDuration;
        
        [FoldoutGroup("TweensSettings/EnemyShowing/Movement")]
        [SerializeField] private float _startPosYOffset = 1.0f;

        [SerializeField] private GameObject _enemiesBar;
        [SerializeField] private TextMeshProUGUI _lifeText;
        [SerializeField] private MMF_Player _barPlayer;
        [SerializeField] private MMProgressBar _progressBar;

        [SerializeField] private Image _backgroundImage;
        [SerializeField] private CanvasGroup _backgroundCanvasGroup;
        [SerializeField] private TextMeshProUGUI _characterNameText;

        #region Test
        private Enemy testEnemy = null;

        [Button("Move")]
        private void TestShow()
        {
            ShowNewEnemyOnScene(testEnemy);
        }
        #endregion

        public void UpdateLifeText(int life, int maxLife)
        {
            _lifeText.SetText(life + "/" + maxLife);
            _barPlayer.PlayFeedbacks();
            _progressBar.UpdateBar(life, 0, maxLife);
        }

        public void OnEndGame()
        {
            _enemiesBar.SetActive(false);
        }

        public void ShowNewEnemyOnScene(Enemy enemy, bool isFirstEnemy = false)
        {
            testEnemy = enemy;

            PrepareEnemy(enemy);

            if (!isFirstEnemy)
                UpdateBackgroundOnNewEnemy(enemy.Background);
            else
                UpdateBackgroundOnFirstEnemy(enemy.Background);


            UpdateLifeText(enemy.ActualHp, enemy.MaxHp);
            UpdateBarOnNewEnemy(enemy.ActualHp, enemy.MaxHp);

            Tween movementY = enemy.transform
                .DOMoveY(enemy.transform.position.y + _movementYAmplitude, _movementYDuration)
                .SetLoops(-1, LoopType.Yoyo)
                .SetEase(Ease.Linear);

            Sequence showing = DOTween.Sequence();

            showing.Append(enemy.transform.DOMoveX(0, _movementXDuration));
            showing.InsertCallback(_movementXDuration, () => movementY.Kill());
            showing.Append(enemy.SpriteRenderer.DOColor(Color.white, _blackColorFadeOutDuration)).SetEase(Ease.Linear);
            showing.OnComplete(OnComplete);
        }

        private void OnComplete()
        {
            Debug.Log("OnComplete");
        }

        private void UpdateNameOnNewEnemy(string name) => _characterNameText.SetText(name);

        private void UpdateBackgroundOnNewEnemy(Sprite newBackground)
        {
            Sequence sequence = DOTween.Sequence();

            sequence.Append(_backgroundCanvasGroup.DOFade(0, _bgFadeOutDuration)).SetEase(Ease.Linear);
            sequence.InsertCallback(_bgFadeOutDuration, () => _backgroundImage.sprite = newBackground);
            sequence.Append(_backgroundCanvasGroup.DOFade(1, _bgFadeInDuration)).SetEase(Ease.Linear);
        }

        private void PrepareEnemy(Enemy enemy)
        {
            enemy.gameObject.SetActive(true);
            enemy.transform.position = GetStartPosition(enemy.SpriteRenderer);
            enemy.SpriteRenderer.color = Color.black;
        }

        private Vector3 GetStartPosition(SpriteRenderer sprite)
        {
            float cameraHeight = Camera.main.orthographicSize;
            float cameraWidth = cameraHeight * Camera.main.aspect;

            return new Vector3(-cameraWidth - sprite.bounds.extents.x, -cameraHeight + _startPosYOffset, 0);
        }

        private void UpdateBackgroundOnFirstEnemy(Sprite newBackground)
        {
            _backgroundImage.sprite = newBackground;
            _backgroundCanvasGroup.alpha = 0f;
            _backgroundCanvasGroup.DOFade(1, _bgFadeInDuration).SetEase(Ease.Linear);
        }

        private void UpdateBarOnNewEnemy(int life, int maxLife)
        {
            _lifeText.SetText(life + "/" + maxLife);
            _progressBar.UpdateBar(life, 0, maxLife);
        }

    }
}