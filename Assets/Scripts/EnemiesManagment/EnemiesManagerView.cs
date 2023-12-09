using TMPro;
using UnityEngine;

namespace Assets.Scripts.EnemiesManagment
{
    public class EnemiesManagerView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _lifeText;

        public void UpdateLifeText(int life, int maxLife)
        {
            _lifeText.SetText(life + "/" + maxLife);
        }
    }
}