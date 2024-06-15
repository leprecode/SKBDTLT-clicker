using Assets.Scripts.WeaponsLogic;
using DG.Tweening;
using UnityEngine;

namespace Assets.Scripts.Store
{
    public class StoreView : MonoBehaviour
    {
        public StoreCellUI LastAvctiveCell { get; set; }

        [SerializeField] private CanvasGroup _popUp;
        [SerializeField] private CanvasGroup _store;
        [SerializeField] private float _fadeDuration = 1.0f;
        [SerializeField] private AudioSource _audioSource;
        private StoreCellUI[] _cells;
        private Tween _popUpTween;


        public void Construct(StoreCellUI[] cells)
        {
            _cells = cells;
        }

        public void SetCellsPrices(WeaponsCost weaponsCost)
        {
            for (int i = 0; i < _cells.Length; i++)
            {
                _cells[i].SetPrice(weaponsCost.Prices[_cells[i].Name]);
            }
        }

        public void ShowPopupNotEnoughMoney()
        {
            _popUpTween?.Kill();
            _popUp.alpha = 1;
            _popUpTween = _popUp.DOFade(0, _fadeDuration).SetEase(Ease.Linear);
        }

        public void SetInactiveStatesOnLoadProgress(int weaponsCount)
        {
            for (int i = 0; i < weaponsCount && i < _cells.Length; i++)
            {
                _cells[i].SetDisableState();
            }

            LastAvctiveCell = _cells[Mathf.Min(weaponsCount - 1, _cells.Length-1)];
            LastAvctiveCell.SetActiveState();
        }

        public void UpdateActualCellUI(StoreCellUI cell)
        {
            LastAvctiveCell.SetInactiveState();
            LastAvctiveCell = cell;
            LastAvctiveCell.SetActiveState();
        }

        internal void PlayBoughtSound()
        {
            _audioSource.Play();
        }
    }
}
