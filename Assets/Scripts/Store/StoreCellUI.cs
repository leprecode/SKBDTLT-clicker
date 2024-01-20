using Assets.Scripts.WeaponsLogic;
using DG.Tweening;
using System;
using System.Globalization;
using TMPro;
using UnityEngine;

namespace Assets.Scripts.Store
{
    public class StoreCellUI : MonoBehaviour
    {
        private readonly int ACTIVE_HASH_ID = Animator.StringToHash("Active");
        private readonly int ALLOW_TO_BUY_HASH_ID = Animator.StringToHash("AllowToBuy");

        public event Action<StoreCellUI, WeaponName> OnClicked;
        [field: SerializeField] public WeaponName Name { get; private set; }

        [SerializeField] private TextMeshProUGUI _priceText;
        [SerializeField] private Animator _animator;

        private float _timeToUpdatePrice = 0.3f;

        public void SetPrice(int price)
        {
            _priceText.DOCounter(0, price, _timeToUpdatePrice, true, CultureInfo.CurrentCulture);
        }

        public void OnCellClick()
        {
            OnClicked?.Invoke(this, Name);
        }

        public void SetInactiveState()
        {
            _animator.SetBool(ACTIVE_HASH_ID, false);
        }

        public void SetAllowToBuy()
        {
            _animator.SetBool(ALLOW_TO_BUY_HASH_ID, true);
        }

        public void SetActiveState()
        {
            _animator.SetBool(ACTIVE_HASH_ID, true);
        }
    }

}
