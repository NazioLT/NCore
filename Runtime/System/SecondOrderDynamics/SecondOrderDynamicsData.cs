using UnityEngine;

namespace Nazio_LT.Tools.Core
{
    [System.Serializable]
    public struct SecondOrderDynamicsData : ISerializationCallbackReceiver
    {
        public SecondOrderDynamicsData(float _f, float _z, float _r)
        {
            frequency = _f;
            damping = _z;
            impulse = _r;

            k1 = 0;
            k2 = 0;
            k3 = 0;

            UpdateData(_f, _z, _r);
        }

        [SerializeField] private float frequency, damping, impulse;

        private float k1, k2, k3;

        private void UpdateData(float _f, float _z, float _r)
        {
            if(_f < 0) _f = 0;
            if(_z < 0) _z = 0;
            if(_r < 0) _r = 0;

            float PI2F = 2 * Mathf.PI * _f;
            //compute constants
            k1 = _z / (Mathf.PI * _f);
            k2 = 1 / (PI2F * PI2F);
            k3 = _r * _z / PI2F;
        }

        public void OnAfterDeserialize()
        {
            UpdateData(frequency, damping, impulse);
        }

        public void OnBeforeSerialize() { }

        public float K1 => k1;
        public float K2 => k2;
        public float K3 => k3;
    }

}