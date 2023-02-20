using UnityEngine;

namespace Nazio_LT.Tools.Core
{
    public class SecondOrderDynamicsFollower : MonoBehaviour
    {
        [SerializeField] private Transform target;
        [SerializeField] private SecondOrderDynamics<Vector3> dynamics = new SecondOrderDynamics<Vector3>(1, 0.5f, 0, Vector3.zero);

        private void Awake()
        {
            transform.position = target.position;
            dynamics.Init(target.position);
        }

        private void Update()
        {
            transform.position = SecondOrderDynamics<Vector3>.Update(Time.deltaTime, dynamics, target.position);
        }
    }

}