using UnityEngine;

namespace Nazio_LT.Tools.Core
{
    public class SecondOrderDynamicsFollower : MonoBehaviour
    {
        [SerializeField] private Transform m_target;
        [SerializeField] private SecondOrderDynamics<Vector3> m_dynamics = new SecondOrderDynamics<Vector3>(1, 0.5f, 0, Vector3.zero);

        private void Awake()
        {
            transform.position = m_target.position;
            m_dynamics.Init(m_target.position);
        }

        private void Update()
        {
            transform.position = SecondOrderDynamics<Vector3>.Update(Time.deltaTime, m_dynamics, m_target.position);
        }
    }
}