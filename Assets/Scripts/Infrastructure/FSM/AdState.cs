using UnityEngine;

namespace Assets.Scripts.Infrastructure
{
    public class AdState : IState
    {
        public void Enter()
        {
            Debug.Log("Вход в Состояние1");
        }

        public void Exit()
        {
            Debug.Log("Выход из Состояния1");
        }


    }

}
