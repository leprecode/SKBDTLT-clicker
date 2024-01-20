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
            Time.timeScale = 0;
            _enemiesManager.DisableActualEnemyInteractivity();
        }

        public void Exit()
        {
            Time.timeScale = 1;
            _enemiesManager.EnableActualEnemyInteractivity();
        }

    }

}
