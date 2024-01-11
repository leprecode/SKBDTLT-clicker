using UnityEngine;

namespace Assets.Scripts.Infrastructure
{
    public class DamageRandomizer
    {
        private float _minRandom;
        private float _maxRandom;

        public DamageRandomizer(float minRandom, float maxRandom)
        {
            _minRandom = minRandom;
            _maxRandom = maxRandom;
        }

        public int GetRandomDamage(int damage)
        {
            var result = Mathf.RoundToInt(damage * Random.Range(_minRandom, _maxRandom));
            return result > 0 ? result : 1;
        }
    }
}
