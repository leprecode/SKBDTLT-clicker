using System.Runtime.InteropServices;
using TMPro;
using UnityEngine;
using DG.Tweening;
using System.Collections;
using Assets.Scripts.Infrastructure;


namespace Assets.Scripts.YandexSDK.Advertisment
{
    public class ADV : MonoBehaviour
    {
        //TODO:decompose to ciew. Remove static class RewardedData. Ввести состояния для рекламы, чтобы не было нахлестов. Заменииь

        private const float DurationToFadeIn = 0.5f;
        [SerializeField] private CanvasGroup _popUP;
        [SerializeField] private TextMeshProUGUI _textCounter;
        [SerializeField] private float _timeBetweenShowing;
        [SerializeField] private GameObject _rewardedPopup;
        [SerializeField] private TextMeshProUGUI _rewardedTimerText;
        [SerializeField] private float _rewardDuration;
        [SerializeField] private int _rewardMultiplayer;
        [SerializeField] private AudioSource _audioSourceOnReward;
        
        private bool _isRewardTime;
        private float _rewardTimer;

        private StateMachine _fsm;

        private float _timeToNextShowing;
        private int _counterToShow = 5;
        
        [DllImport("__Internal")]
        private static extern void ShowFullScreenAD();

        [DllImport("__Internal")]
        private static extern void ShowRewardedVideo();

        [DllImport("__Internal")]
        private static extern void ShowFullScreenADInFirstTime();

        [DllImport("__Internal")]
        private static extern void TryInitializeYandexSDK();

        public void Construct(StateMachine fsm)
        {
            _fsm = fsm;
        }

        public void InitializeSDK()
        {
            TryInitializeYandexSDK();
        }

        public void OnRewardButtonClick()
        {
            ShowRewardedVideo();
        }

        public void TakeRewardOnRewardedVideo()
        {
            _isRewardTime = true;
            _rewardTimer += _rewardDuration;
            _rewardedPopup.SetActive(true);
            _rewardedTimerText.SetText(_rewardTimer.ToString());
            RewardData.DamageMultiplayer = _rewardMultiplayer;
            _audioSourceOnReward.Play();
        }

        public void StartInterstitialOnAwake()
        {
            try
            {
                ShowFullScreenADInFirstTime();
                _fsm.SetState(GameState.Pause);
            }
            catch
            {
                Debug.LogError("cant show adv on start the game");
            }
        }

        public void OnFullscreenADVEndOrError()
        {
            Debug.Log("OnFullscreenADEndOrError");
            ResetWarningPopup(); 
            _timeToNextShowing = _timeBetweenShowing;
            _fsm.SetState(GameState.Gameplay);
        }

        private void Start()
        {
            _timeToNextShowing = _timeBetweenShowing;
        }

        private void Update()
        {
            _timeToNextShowing -= Time.deltaTime;

            if (_timeToNextShowing < 0) 
            {
                _timeToNextShowing = _timeBetweenShowing;
                ShowWarningToADV();
            }

            if (_isRewardTime)
            {
                _rewardTimer -= Time.deltaTime;
                _rewardedTimerText.SetText(Mathf.Clamp(Mathf.RoundToInt(_rewardTimer),0,_rewardDuration).ToString());

                if (_rewardTimer < 0)
                {
                    _isRewardTime = false;
                    _rewardedPopup.SetActive(false);
                    RewardData.DamageMultiplayer = 1;
                    _audioSourceOnReward.Stop();
                }
            }
        }

        private void ShowWarningToADV()
        {
            _popUP.DOFade(1, DurationToFadeIn);
            StartCoroutine(StartCountdown());
        }

        IEnumerator StartCountdown()
        {
            while (_counterToShow > 0)
            {
                _counterToShow--;
                _textCounter.SetText(_counterToShow.ToString());
                yield return new WaitForSeconds(1f);
            }

            _fsm.SetState(GameState.Pause);
        }

        private void ResetWarningPopup()
        {
            _popUP.alpha = 0;
            _counterToShow = 5;
            _textCounter.SetText(_counterToShow.ToString());
        }
    }
}