using UnityEngine;

namespace Game.App.Utils
{
    public sealed class VectorRandomiser
    {
        private float _minValue;
        private float _maxValue;
        public VectorRandomiser(float minValue, float maxValue)
        {
            _minValue = minValue;
            _maxValue = maxValue;
        }
        public Vector3 GetRandomSpawnPosition()
        {
            float x = Random.Range(_minValue, _maxValue);
            float z = Random.Range(_minValue, _maxValue);
            return new Vector3(x, 1f, z);
        }
    }
}
