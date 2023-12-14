using UnityEngine;

namespace Assets.Scripts.Infrastructure
{
    public class GameplayState : IUpdatableState
    {
        public void Enter()
        {
            Debug.Log("Вход в Состояние1");
        }

        public void Exit()
        {
            Debug.Log("Выход из Состояния1");
        }

        public void Update()
        {
            throw new System.NotImplementedException();
        }
    }

}
