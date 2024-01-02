using MoreMountains.Feedbacks;
using MoreMountains.Tools;
using TMPro;
using UnityEngine;

namespace Assets.Scripts.EnemiesManagment
{
    public class EnemiesManagerView : MonoBehaviour
    {
        [SerializeField] private GameObject _enemiesBar;
        [SerializeField] private TextMeshProUGUI _lifeText;
        [SerializeField] private MMF_Player _barPlayer;
        [SerializeField] private MMProgressBar _progressBar;

        public void UpdateBarOnNewEnemy(int life, int maxLife)
        {
            _lifeText.SetText(life + "/" + maxLife);
            _progressBar.UpdateBar(life, 0, maxLife);
        }

        public void UpdateLifeText(int life, int maxLife)
        {
            _lifeText.SetText(life + "/" + maxLife);
            _barPlayer.PlayFeedbacks();
            _progressBar.UpdateBar(life,0,maxLife);
        }

        public void OnEndGame()
        {
            _enemiesBar.SetActive(false);
        }
    }
}