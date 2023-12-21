using Assets.Scripts.WeaponsLogic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

namespace Assets.Scripts.Store
{
    public class StoreCellUI : MonoBehaviour
    {
        [SerializeField] private WeaponName _name;
        [SerializeField] private TextMeshProUGUI _debugtext;

        public delegate void OnClick(WeaponName name);
        public event OnClick OnClicked;

        public void OnCellClick()
        {
            Debug.Log(name + " CKICKED");
            OnClicked?.Invoke(_name);
        }

        //TODO: remove
        private void OnDrawGizmos()
        {
            _debugtext.SetText(_name.ToString());
        }
    }

}
