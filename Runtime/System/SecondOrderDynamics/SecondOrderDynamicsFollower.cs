using UnityEngine;

namespace Nazio_LT.Tools.Core
{
    public class SecondOrderDynamicsFollower : MonoBehaviour
    {
        [SerializeField] private float frequency, damping, impulse;
        [SerializeField] private Transform target;

        SecondOrderDynamics dynamics;

        private void Awake()
        {
            transform.position = target.position;
            dynamics = new SecondOrderDynamics(frequency, damping, impulse, target.position);
        }

        private void Update()
        {
            transform.position = dynamics.Update(Time.deltaTime, target.position);
        }
    }

}