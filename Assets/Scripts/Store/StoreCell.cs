using TMPro;
using UnityEngine;

namespace Assets.Scripts.Store
{
    public class StoreCell : MonoBehaviour
    {
        [SerializeField] private WeaponName _name;
        [SerializeField] private TextMeshProUGUI _debugtext;

        public void OnCellClick()
        {
            //TODO: запрашиваем, есть ли это оружие уже. если нет, то пытаемся купить. если есть - используем
        }

        //TODO^ remove
        private void OnDrawGizmos()
        {
            _debugtext.SetText(_name.ToString());
        }
    }
}
