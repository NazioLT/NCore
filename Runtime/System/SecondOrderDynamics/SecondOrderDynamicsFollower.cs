using UnityEngine;

namespace Nazio_LT.Tools.Core
{
    public class SecondOrderDynamicsFollower : MonoBehaviour
    {
        [SerializeField] private Transform target;
        [SerializeField] private SecondOrderDynamics dynamics = new SecondOrderDynamics(1, 0.5f, 0, Vector3.zero);

        private void Awake()
        {
            transform.position = target.position;
            dynamics.Init(target.position);
        }

        private void Update()
        {
            transform.position = dynamics.Update(Time.deltaTime, target.position);
        }
    }

}