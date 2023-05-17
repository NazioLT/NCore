using UnityEngine;

namespace Nazio_LT.Tools.Core
{
    [System.Serializable]
    public struct SecondOrderDynamicsData : ISerializationCallbackReceiver
    {
        public SecondOrderDynamicsData(float f, float z, float r)
        {
            m_frequency = f;
            m_damping = z;
            m_impulse = r;

            m_k1 = 0;
            m_k2 = 0;
            m_k3 = 0;
            m_k2Stable = 0;

            UpdateData(f, z, r);
        }

        [SerializeField, Range(0, 10f)] private float m_frequency;
        [SerializeField, Range(0, 5f)] private float m_damping;
        [SerializeField, Range(-5f, 10f)] private float m_impulse;

        private float m_k1, m_k2, m_k2Stable, m_k3;

        public void OnAfterDeserialize()
        {
            UpdateData(m_frequency, m_damping, m_impulse);
        }

        public void OnBeforeSerialize() { }

        public float K2Stable(float _t) => Mathf.Max(m_k2, _t * _t / 2 + _t * m_k1 / 2, _t * m_k1);

        private void UpdateData(float f, float z, float r)
        {
            if (f < 0) f = 0;
            if (z < 0) z = 0;
            if (r < 0) r = 0;

            float PI2F = 2 * Mathf.PI * f;
            //compute constants
            m_k1 = z / (Mathf.PI * f);
            m_k2 = 1 / (PI2F * PI2F);
            m_k3 = r * z / PI2F;
        }

        public float K1 => m_k1;
        public float K2 => m_k2;
        public float K3 => m_k3;
    }
}