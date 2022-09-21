using UnityEngine;

namespace Nazio_LT.Tools.Core
{
    [AddComponentMenu("Nazio_LT/Core/NCurveBehaviour")]
    public class NCurveBehaviour : MonoBehaviour
    {
        //For Editor
        public bool editing;
        public CurveMesh.CurveMeshType meshType;

        [SerializeField] public NCurve curve;
        public MeshFilter meshFilter;

        [ContextMenu("Gen")]
        public void Gen()
        {
            CurveMesh _mesh = CurveMesh.Factory(meshType, curve);
            meshFilter.sharedMesh = _mesh.GenerateMesh();
        }
    }
}