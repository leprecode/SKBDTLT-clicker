using Assets.Scripts.EnemiesManagment;
using UnityEngine;

namespace Assets.Scripts.Infrastructure
{
    public class PauseState : IState
    {
        private readonly EnemiesManager _enemiesManager;

        public PauseState(EnemiesManager enemiesManager)
        {
            _enemiesManager = enemiesManager;
        }

        public void Enter()
        {
            Debug.Log("Вход в PauseState");
            Time.timeScale = 0;
            _enemiesManager.DisableActualEnemyInteractivity();
        }

        public void Exit()
        {
            Debug.Log("Выход из PauseState");
            Time.timeScale = 1;
            _enemiesManager.EnableActualEnemyInteractivity();
        }

    }

}
