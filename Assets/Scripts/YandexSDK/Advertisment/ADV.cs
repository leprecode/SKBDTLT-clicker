using TMPro;
using UnityEngine;
using DG.Tweening;
using System.Collections;
using Assets.Scripts.Infrastructure;
using YG;

namespace Assets.Scripts.YandexSDK.Advertisment
{
    public class ADV : MonoBehaviour
    {

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

        private bool _rewardedVideoIsShowing = false;
        
        public void Construct(StateMachine fsm)
        {
            _fsm = fsm;
        } 

        public void ShowRewardedVideo()
        {
            YandexGame.RewVideoShow(1);
            _fsm.SetState(GameState.Pause);
            _audioSourceOnReward.Pause();
        }

        public void OnOpenRewardVideo()
        {
            _isRewardTime = false;
            _rewardedVideoIsShowing = true;
            ResetWarningPopup();
        }

        public void OnReward()
        {
            _isRewardTime = true;
            _rewardTimer += _rewardDuration;
            _rewardedPopup.SetActive(true);
            _rewardedTimerText.SetText(_rewardTimer.ToString());
            RewardData.DamageMultiplayer = _rewardMultiplayer;
            _audioSourceOnReward.Play();
            _fsm.SetState(GameState.Gameplay);
            _rewardedVideoIsShowing = false;
        }

        public void OnRewardError()
        {
            _fsm.SetState(GameState.Gameplay);
            _rewardedVideoIsShowing = false;
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
                if (_rewardedVideoIsShowing)
                {
                    _timeToNextShowing += 30;
                    _rewardedVideoIsShowing = false;
                    return;
                }

                _timeToNextShowing = _timeBetweenShowing;
                ShowWarningToADV();
            }

            if (_isRewardTime)
            {
                _rewardTimer -= Time.deltaTime;
                _rewardedTimerText.SetText(Mathf.Clamp(Mathf.RoundToInt(_rewardTimer),0,600).ToString());

                if (_rewardTimer < 0)
                {
                    _isRewardTime = false;
                    _rewardedPopup.SetActive(false);
                    RewardData.DamageMultiplayer = 1;
                    _audioSourceOnReward.Stop();
                    _rewardTimer = _rewardDuration;
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
            YandexGame.FullscreenShow();
        }
        
        private void ResetWarningPopup()
        {
            _popUP.alpha = 0;
            _counterToShow = 5;
            _textCounter.SetText(_counterToShow.ToString());
        }
    }
}