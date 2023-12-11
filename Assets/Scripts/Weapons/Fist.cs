using System;
using UnityEngine;
using DG.Tweening;

namespace Assets.Scripts.Weapons
{
    [Serializable]
    public class Fist : Weapon
    {
        public override int Attack(Vector3 position)
        {
            transform.DOMove(position, MovementDuration);
            //transform.DOMove()
            return Damage;
        }
    }
}