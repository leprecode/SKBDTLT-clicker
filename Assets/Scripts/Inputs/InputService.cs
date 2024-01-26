using UnityEngine;
using Assets.Scripts.Infrastructure;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using Unity.Burst.Intrinsics;

namespace Assets.Scripts.Inputs
{
    public class InputService
    {
        private Player _player;
        
        public InputService(Player player)
        {
            _player = player;
        }

        [DllImport("__Internal")]
        private static extern string GetPlatformInfo();


        public void Initial()
        {
            InstallInput();
        }

        private void InstallInput()
        {
            string platformInfo;

            try
            {
                platformInfo = GetPlatformInfo();
            }
            catch
            {
                Debug.Log("CantGet platform info. set pc input");
                SetStandaloneInput();
                return;
            }

            Debug.Log("Platform Info: " + platformInfo);


            if (platformInfo == "Windows" || platformInfo == "Mac" || platformInfo == "Linux" || platformInfo == "Unknown Platform")
            {
                Debug.Log("Game run on PC");
                SetStandaloneInput();
            }
            else if (platformInfo == "iOS" || platformInfo == "Android" )
            {
                Debug.Log("Game run on mobile");
                SetMobileInput();
            }
            else
            {
                Debug.Log("CantGet platform info. set pc input");
                SetStandaloneInput();
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