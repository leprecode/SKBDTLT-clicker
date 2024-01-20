using TMPro;
using UnityEngine;

namespace Assets.Scripts.BankLogic
{
    public class BankView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _moneyScore;


        public void UpdateUIOnAddingMoney(int money)
        {
            _moneyScore.SetText(money.ToString());
        }

        public void UpdateUIOnSpendMoney(int money)
        {
            _moneyScore.SetText(money.ToString());
        }
    }
}