using UnityEngine;

namespace Nazio_LT.Tools.Core
{
    public static class SecondOrderDynamicsUtils
    {
        public static Vector3 Update(this SecondOrderDynamics<Vector3> dynamics, float t, Vector3 x) => SecondOrderDynamics<Vector3>.Update(t, dynamics, x);
        public static Vector2 Update(this SecondOrderDynamics<Vector2> dynamics, float t, Vector2 x) => SecondOrderDynamics<Vector2>.Update(t, dynamics, x);
        public static float Update(this SecondOrderDynamics<float> dynamics, float t, float x) => SecondOrderDynamics<float>.Update(t, dynamics, x);
    }

    [System.Serializable]
    public class SecondOrderDynamics<T>
    {
        public SecondOrderDynamics(float f, float z, float r, T initialPosition)
        {
            //Init variables
            Init(initialPosition);

            m_data = new SecondOrderDynamicsData(f, z, r);
        }

        [SerializeField] private SecondOrderDynamicsData m_data;

        private T m_xp;//Previous Input
        private T m_y, m_yd;//State variables

        public void Init(T initialPosition)
        {
            m_xp = initialPosition;
            m_y = initialPosition;
            m_yd = (T)default;
        }

        public static Vector3 Update(float t, SecondOrderDynamics<Vector3> dynamics, Vector3 x)
        {
            SecondOrderDynamicsData data = dynamics.m_data;

            //Estimated Velocity
            Vector3 xd = (x - dynamics.m_xp) / t;
            dynamics.m_xp = x;

            dynamics.m_y += t * dynamics.m_yd;//integrate position by velocity
            dynamics.m_yd += t * (x + data.K3 * xd - dynamics.m_y - data.K1 * dynamics.m_yd) / data.K2Stable(t);//integrate velocity by acceleration

            return dynamics.m_y;
        }

        public static Vector2 Update(float t, SecondOrderDynamics<Vector2> dynamics, Vector2 x)
        {
            SecondOrderDynamicsData data = dynamics.m_data;

            //Estimated Velocity
            Vector2 xd = (x - dynamics.m_xp) / t;
            dynamics.m_xp = x;

            dynamics.m_y += t * dynamics.m_yd;//integrate position by velocity
            dynamics.m_yd += t * (x + data.K3 * xd - dynamics.m_y - data.K1 * dynamics.m_yd) / data.K2Stable(t);//integrate velocity by acceleration

            return dynamics.m_y;
        }

        public static float Update(float t, SecondOrderDynamics<float> dynamics, float x)
        {
            SecondOrderDynamicsData data = dynamics.m_data;

            //Estimated Velocity
            float xd = (x - dynamics.m_xp) / t;
            dynamics.m_xp = x;

            dynamics.m_y += t * dynamics.m_yd;//integrate position by velocity
            dynamics.m_yd += t * (x + data.K3 * xd - dynamics.m_y - data.K1 * dynamics.m_yd) / data.K2Stable(t);//integrate velocity by acceleration

            return dynamics.m_y;
        }
    }

}