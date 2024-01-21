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
        private const float DurationToFadeIn = 0.5f;
        [SerializeField] private CanvasGroup _popUP;
        [SerializeField] private TextMeshProUGUI _textCounter;
        [SerializeField] private float _timeBetweenShowing;
        private StateMachine _fsm;

        private float _timeToNextShowing;
        private int _counterToShow = 5;
        
        [DllImport("__Internal")]
        private static extern void ShowFullScreenAD();

        [DllImport("__Internal")]
        private static extern void ShowFullScreenADInFirstTime();

        public void Construct(StateMachine fsm)
        {
            _fsm = fsm;
        }

        public void StartInterstitialOnAwake()
        {
            ShowFullScreenADInFirstTime();
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