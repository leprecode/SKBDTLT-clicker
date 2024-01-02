using TMPro;
using UnityEngine;

namespace Assets.Scripts.EnemiesManagment
{
    public class EnemiesManagerView : MonoBehaviour
    {
        [SerializeField] private GameObject _enemiesBar;
        [SerializeField] private TextMeshProUGUI _lifeText;

        public void UpdateLifeText(int life, int maxLife)
        {
            _lifeText.SetText(life + "/" + maxLife);
        }

        public void OnEndGame()
        {
            _enemiesBar.SetActive(false);
        }
    }
}