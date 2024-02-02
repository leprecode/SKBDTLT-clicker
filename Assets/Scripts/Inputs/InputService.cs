using UnityEngine;
using Assets.Scripts.Infrastructure;
using YG;

namespace Assets.Scripts.Inputs
{
    public class InputService
    {
        private Player _player;
        
        public InputService(Player player)
        {
            _player = player;
        }

        public void Initial()
        {
            InstallInput();
        }

        private void InstallInput()
        {
            if (YandexGame.SDKEnabled)
            {
                if (YandexGame.EnvironmentData.isMobile || YandexGame.EnvironmentData.isTablet)
                {
                    SetMobileInput();
                    Debug.Log("! YabdexSdk islaucnhed on input startup; Platform is mobile or tablet.  Set mobile input");
                }
                else if (YandexGame.EnvironmentData.isDesktop)
                {
                    SetStandaloneInput();
                    Debug.Log("! YabdexSdk islaucnhed on input startup;  Platform is snadalone; Set standalone input");
                }
                else
                {
                    SetStandaloneInput();
                    Debug.Log("! YabdexSdk islaucnhed on input startup; Cant check platform. Set standalone input");
                }
            }
            else
            {
                SetStandaloneInput();
                Debug.Log("! YabdexSdk is not laucnhed on input startup; Cant check platform. Set standalone input");
            }
        }

        private void SetMobileInput()
        {
            GameObject input = new GameObject("Input");
            var inputHandler = input.AddComponent<TouchInputHandler>();
            inputHandler.Construct(_player);
        }

        private void SetStandaloneInput()
        {
            GameObject input = new GameObject("Input");
            var inputHandler = input.AddComponent<StandaloneInputHandler>();
            inputHandler.Construct(_player);
        }
    }
}