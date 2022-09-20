using UnityEngine;

namespace Nazio_LT.Tools.Core
{
    [AddComponentMenu("Nazio_LT/Core/NCurveBehaviour")]
    public class NCurveBehaviour : MonoBehaviour
    {
        //For Editor
        public bool editing;
        [SerializeField, ] public float dist;

        [SerializeField] public NCurve curve;

        private void OnDrawGizmos()
        {
            Gizmos.DrawCube(curve.ComputePointDistance(dist), Vector3.one * 0.5f);
        }
    }
}