using Assets.Scripts.Infrastructure;
using DG.Tweening;
using MoreMountains.Feedbacks;
using MoreMountains.Tools;
using System.Collections;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.SettingsMenu
{
    public class Settings : MonoBehaviour
    {
        [SerializeField] private MMF_Player _onDamageAllFeedback;
        [SerializeField] private AudioMixerGroup _masterGroup;
        private MMF_FloatingText _floatingText;

        private StateMachine _stateMachine;

        public void Construct(StateMachine stateMachine)
        {
            _stateMachine = stateMachine;
        }

        private void Start()
        {
            _floatingText = _onDamageAllFeedback.GetFeedbackOfType<MMF_FloatingText>();
        }


        public void OnSettingsMenuOpen()
        {
            _stateMachine.SetState(GameState.Pause);
        }

        public void OnSettingsMenuClose()
        {
            _stateMachine.SetState(GameState.Gameplay);
        }

        public void OnTextToggle(bool value)
        { 
            _floatingText.Active = value;
        }

        public void OnSoundToggle(bool value)
        {
            if (value)
                _masterGroup.audioMixer.SetFloat("MasterVolume", 0);
            else
                _masterGroup.audioMixer.SetFloat("MasterVolume", -80);
        }

        public void ResetGame()
        {
            Time.timeScale = 1.0f;
            DOTween.KillAll();
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}