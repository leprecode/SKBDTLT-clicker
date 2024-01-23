using Assets.Scripts.EnemyLogic;
using DG.Tweening;
using MoreMountains.Feedbacks;
using MoreMountains.Tools;
using Sirenix.OdinInspector;
using System.Globalization;
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
        [SerializeField] private float _blackColorFadeOutDuration;


        [FoldoutGroup("TweensSettings/EnemyShowing")]
        [SerializeField] private float _nameShoweingDuration;

        [FoldoutGroup("TweensSettings/EnemyShowing")]
        [SerializeField] private float _lifeTextUpdatingDuration;

        [FoldoutGroup("TweensSettings/EnemyShowing")]
        [SerializeField] private float _lifeTextInitialDuration;

        [FoldoutGroup("TweensSettings/EnemyShowing/Movement")]
        [SerializeField] private float _movementYAmplitude;

        [FoldoutGroup("TweensSettings/EnemyShowing/Movement")]
        [SerializeField] private float _showingMovementYDuration;

        [FoldoutGroup("TweensSettings/EnemyShowing/Movement")]
        [SerializeField] private float _intervalBeforMoveXCharacter = 0.5f;

        [FoldoutGroup("TweensSettings/EnemyShowing/Movement")]
        [SerializeField] private float _movementXDuration;

        [FoldoutGroup("TweensSettings/EnemyShowing/Movement")]
        [SerializeField] private float _startPosYOffset = 1.0f;

        [FoldoutGroup("TweensSettings/EnemyIdle")]
        [SerializeField] private float _idleMovementYDuration;

        [FoldoutGroup("TweensSettings/EnemyIdle")]
        [SerializeField] private float _idleMovementYAmplitude;

        [SerializeField] private GameObject _enemiesBar;
        [SerializeField] private TextMeshProUGUI _lifeText;
        [SerializeField] private TextMeshProUGUI _maxLifeText;
        [SerializeField] private MMF_Player _barPlayer;
        [SerializeField] private MMProgressBar _progressBar;

        [SerializeField] private Image _backgroundImage;
        [SerializeField] private CanvasGroup _backgroundCanvasGroup;
        [SerializeField] private TextMeshProUGUI _characterNameText;

        private int _lastLife;
        private Tween _lifeUpdateTween;

        public void UpdateLifeText(int life, int maxLife)
        {
            var lifeClamped = Mathf.Max(life, 0);

            _lifeUpdateTween = _lifeText.DOCounter(_lastLife, lifeClamped, _lifeTextUpdatingDuration, true, CultureInfo.CurrentCulture);

            _barPlayer.PlayFeedbacks();
            _progressBar.UpdateBar(life, 0, maxLife);

            _lastLife = lifeClamped;
        }


        public void OnEndGame()
        {
            _enemiesBar.SetActive(false);
        }

        public void ShowNewEnemyOnScene(Enemy enemy, bool isFirstEnemy = false)
        {
            PrepareEnemy(enemy);

            if (!isFirstEnemy)
                UpdateBackgroundOnNewEnemy(enemy.Background);
            else
                UpdateBackgroundOnFirstEnemy(enemy.Background);

            Tween movementY = enemy.transform
                .DOMoveY(enemy.transform.position.y + _movementYAmplitude, _showingMovementYDuration)
                .SetLoops(-1, LoopType.Yoyo)
                .SetEase(Ease.Linear);

            Sequence showing = DOTween.Sequence();
            showing.AppendInterval(_intervalBeforMoveXCharacter);
            showing.Append(enemy.transform.DOMoveX(0, _movementXDuration));
            showing.InsertCallback(_movementXDuration, () => movementY.Kill());
            showing.Append(enemy.SpriteRenderer.DOColor(Color.white, _blackColorFadeOutDuration)).SetEase(Ease.Linear);
            showing.OnComplete(() => OnCompleteShowing(enemy));
        }

        public void OnEnemyDie()
        {
            _lifeUpdateTween.OnComplete(() => _lifeText.SetText(""));
            _maxLifeText.SetText("");
            _characterNameText.SetText("");
        }

        private void OnCompleteShowing(Enemy enemy)
        {
            enemy.AllowToAttack = true;

            InitialLifeText(enemy.ActualHp, enemy.MaxHp);
            UpdateBarOnNewEnemy(enemy.ActualHp, enemy.MaxHp);

            UpdateNameOnNewEnemy(enemy.Name);

            enemy.transform.DOMoveY(enemy.transform.position.y + _idleMovementYAmplitude, _idleMovementYDuration)
                .SetEase(Ease.Linear)
                .SetLoops(-1, LoopType.Yoyo).SetLink(enemy.gameObject);

            enemy.EnableColliders();
        }

        private void InitialLifeText(int life, int maxLife)
        {
            _lifeText.DOCounter(0, life, _lifeTextInitialDuration, true, CultureInfo.CurrentCulture);
            _maxLifeText.DOCounter(0, maxLife, _lifeTextInitialDuration, true, CultureInfo.CurrentCulture);

            _lastLife = life;
        }

        private void UpdateNameOnNewEnemy(string name) => _characterNameText.DOText(name, _nameShoweingDuration);

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