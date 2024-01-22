using UnityEngine;
using Assets.Scripts.Infrastructure;
using System.Runtime.InteropServices;

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
            if (SystemInfo.deviceType == DeviceType.Handheld)
            {
                Debug.Log("Game run on mobile");
                GameObject input = new GameObject("Input");
                var inputHandler = input.AddComponent<TouchInputHandler>();
                inputHandler.Construct(_player);
            }
            else if(SystemInfo.deviceType == DeviceType.Desktop)
            {
                Debug.Log("Game run on PC");
                GameObject input = new GameObject("Input");
                var inputHandler = input.AddComponent<StandaloneInputHandler>();
                inputHandler.Construct(_player);
            }
        }

        // ServiceLocator.GetService<Player>().Attack(clickPos, _enemy);
    }
}